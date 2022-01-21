// See https://aka.ms/new-console-template for more information
using Microsoft.ML;
using SSW.FireEmoji.Core.MachineLearning;
using SSW.FireEmoji.Core.Models;

Console.WriteLine("Hello, World!");

string modelName = "gitmo.mlnet";

bool forceRebuild = false;
ITransformer? model = null;
if (!File.Exists(modelName) || forceRebuild)
{
    GitmoTrainer trainer = new();
    var data = trainer.MlContext.Data.LoadFromTextFile<GitComment>(@"Data\CommitMessages.csv", separatorChar: ',', hasHeader: false);

    model = trainer.AutoTrain(data, 90);
    trainer.SaveModel(modelName, model);
}

GitmoPredictor gitmoPredictor = new();
if (model != null)
{
    gitmoPredictor.SetModel(model);
}
else
{
    gitmoPredictor.LoadFromFilePath(modelName);
}

Predict("Init project");

void Predict(string commit)
{
    var prediction = gitmoPredictor.Predict(new GitComment {  Comment = commit });
    Console.WriteLine("Prediction for " + commit);
    Console.WriteLine(prediction.Emoji + " " + commit);
}
