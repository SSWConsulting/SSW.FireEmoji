using Microsoft.Extensions.ML;
using Microsoft.ML;
using SSW.FireEmoji.Core.Models;

namespace SSW.FireEmoji.Core.MachineLearning;

public interface IPredictor
{
    void LoadFromStream(Stream stream);
    void LoadFromFilePath(string modelPath);
    void SetModel(ITransformer mlModel);

    List<string> GetCategories();
}

public interface IPredictor<TInput, TOutput> : IPredictor
    where TInput : class
    where TOutput : class, IPrediction, new()
{
    void SetPredictionPool(PredictionEnginePool<TInput, TOutput> predictionPool);

    TOutput Predict(TInput input);
    Dictionary<string, float> PredictionList(TInput input);
}

public class BasePredictor<TInput, TOutput> : IPredictor<TInput, TOutput>
    where TInput : class
    where TOutput : class, IPrediction, new()
{
    private readonly MLContext _mlContext;

    private ITransformer? _mlModel;
    private List<string>? _categories;
    private PredictionEnginePool<TInput, TOutput>? _predictionEnginePool;

    internal BasePredictor()
    {
        _mlContext = new MLContext();
    }

    /// <summary>
    /// Used for Blazor WASM and Azure Functions where we don't have direct acces to the model.
    /// </summary>
    public void LoadFromStream(Stream stream)
    {
        // Load model from file.
        SetModel(_mlContext.Model.Load(stream, out var _));
    }

    /// <summary>
    /// Used for Console applications where we can access the file directly.
    /// </summary>
    public void LoadFromFilePath(string modelPath)
    {
        // Load model from file.
        using var stream = new FileStream(modelPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        SetModel(_mlContext.Model.Load(stream, out var _));
    }

    /// <summary>
    /// Use this when using ASP.NET Core.
    /// </summary>
    public void SetPredictionPool(PredictionEnginePool<TInput, TOutput> predictionPool)
    {
        _predictionEnginePool = predictionPool;
    }

    public void SetModel(ITransformer mlModel)
    {
        // Reset category cache.
        _categories = null;
        _mlModel = mlModel;
    }

    public TOutput Predict(TInput input)
    {
        if (_predictionEnginePool != null)
        {
            // Used for scalable applications.
            return _predictionEnginePool.Predict(input);
        }

        // Used for console applications where multi-threading might not be a problem.
        PredictionEngine<TInput, TOutput>? predictionEngine = _mlContext.Model.CreatePredictionEngine<TInput, TOutput>(_mlModel);
        return predictionEngine.Predict(input);
    }

    public Dictionary<string, float> PredictionList(TInput input)
    {
        TOutput prediction = Predict(input);
        if (prediction == null)
        {
            return new Dictionary<string, float>();
        }

        int num = 0;
        List<string> categories = GetCategories();
        Dictionary<string, float> result = new();
        foreach (string category in categories)
        {
            result.Add(category, prediction.Score[num++]);
        }

        return result;
    }

    public List<string> GetCategories()
    {
        if (_categories != null)
        {
            return _categories;
        }

        var schema = GetOutputSchema();
        _categories = schema.GetCategories();

        List<string> sortedCategories = new(_categories);
        sortedCategories.Sort();
        return sortedCategories;
    }

    public DataViewSchema GetOutputSchema()
    {
        PredictionEngine<TInput, TOutput> predEngine = _predictionEnginePool != null
            ? _predictionEnginePool.GetPredictionEngine()
            : _mlContext.Model.CreatePredictionEngine<TInput, TOutput>(_mlModel);

        return predEngine.OutputSchema;
    }
}
