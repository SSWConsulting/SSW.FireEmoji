using Microsoft.ML;
using Microsoft.ML.AutoML;
using Microsoft.ML.Data;

namespace SSW.FireEmoji.Trainer.Core;

public abstract class BaseTrainer<TInput>
    where TInput : class
{
    protected IDataView? _trainingDataView;

    protected BaseTrainer(MLContext? mlContext = null)
    {
        MlContext = mlContext ?? new MLContext();
    }

    public MLContext MlContext { get; }

    public ITransformer AutoTrain(IEnumerable<TInput> trainingData, uint maxTimeInSec)
        => AutoTrain(MlContext.Data.LoadFromEnumerable(trainingData), maxTimeInSec);

    public ITransformer AutoTrain(IDataView? trainingData, uint maxTimeInSec)
    {
        _trainingDataView = trainingData;

        var experimentSettings = new MulticlassExperimentSettings
        {
            MaxExperimentTimeInSeconds = maxTimeInSec,
            OptimizingMetric = MulticlassClassificationMetric.MacroAccuracy
        };

        var experiment = MlContext.Auto()
            .CreateMulticlassClassificationExperiment(experimentSettings);

        // We mostly only need column definitions for different emoji trainers. (and even that can be generic if all have same column definitions)
        ColumnInformation columnInfo = GetColumnInformation();

        ExperimentResult<MulticlassClassificationMetrics>? result = experiment.Execute(_trainingDataView, columnInfo);

        // TODO: Return class that returns model as well as training stats.
        return result.BestRun.Model;
    }


    public void SaveModel(string modelSavePath, ITransformer model)
    {
        // Save training model to disk.
        MlContext.Model.Save(model, _trainingDataView!.Schema, modelSavePath);
    }

    public void SaveModel(Stream stream, ITransformer model)
    {
        // Save training model to disk.
        MlContext.Model.Save(model, _trainingDataView!.Schema, stream);
    }

    /// <summary>
    /// Column definition is usually one of the few things are different between various classifications
    /// as number of input columns can be different.
    /// 
    /// Also, some ML model benefits certain columns to be treated as hashes opposed to text featurize or we need to skip a couple of columns.
    /// </summary>
    /// <returns>Returns column information.</returns>
    protected abstract ColumnInformation GetColumnInformation();
}
