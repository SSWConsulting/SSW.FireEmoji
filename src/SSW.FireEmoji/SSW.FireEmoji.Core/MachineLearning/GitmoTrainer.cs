using Microsoft.ML;
using Microsoft.ML.AutoML;
using Microsoft.ML.Data;
using SSW.FireEmoji.Core.Models;

namespace SSW.FireEmoji.Core.MachineLearning;

public class GitmoTrainer
{
    private IDataView? _trainingDataView;

    public GitmoTrainer(MLContext? mlContext = null)
    {
        MlContext = mlContext ?? new MLContext();
    }

    public MLContext MlContext { get; }

    public ITransformer AutoTrain(IEnumerable<GitComment> trainingData, uint maxTimeInSec)
        => AutoTrain(MlContext.Data.LoadFromEnumerable(trainingData), maxTimeInSec);

    public ITransformer AutoTrain(IDataView? trainingData, uint maxTimeInSec)
    {
        _trainingDataView = trainingData;

        var experimentSettings = new MulticlassExperimentSettings
        {
            MaxExperimentTimeInSeconds = maxTimeInSec,
            OptimizingMetric = MulticlassClassificationMetric.MacroAccuracy
        };

        var experiment = MlContext.Auto().CreateMulticlassClassificationExperiment(experimentSettings);

        ColumnInformation columnInfo = new() { LabelColumnName = "col0" };
        columnInfo.TextColumnNames.Add("col1");

        ExperimentResult<MulticlassClassificationMetrics>? result = experiment.Execute(_trainingDataView, columnInfo);
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
}