using Cocona;
using Microsoft.ML;
using SSW.FireEmoji.Core.MachineLearning;
using SSW.FireEmoji.Core.Models;
using SSW.FireEmoji.Trainer.Core;
using System.Text;

// Adding support for emojis 🚀
Console.OutputEncoding = Encoding.UTF8;
Console.Write("\xfeff"); // bom = byte order mark

var app = CoconaLiteApp.Create();
app.AddCommand("train", Train)
   .WithDescription("Train model with updated data.");

app.AddCommand("predict", Predict)
    .WithDescription("Test prediction");

await app.RunAsync();

const string DefaultModelPath = "gitmo.mlnet";
static void Train(
    [Option('d', Description = "Path to CSV")] string dataPath,
    [Option('o', Description = "Output path for ML.NET model")] string outputModel = DefaultModelPath,
    [Option('t', Description = "Time to train the model")] uint trainingTime = 90)
{
    BaseTrainer<GitComment> trainer = new GitmoTrainer();
    var data = trainer.MlContext.Data.LoadFromTextFile<GitComment>(dataPath, separatorChar: ',', hasHeader: false);

    ITransformer? model = trainer.AutoTrain(data, trainingTime);

    // TODO: Check model accuracy and fail it if it's too low.
    trainer.SaveModel(outputModel, model);
}

static void Predict(
    [Option('m', Description = "Message")] string message,
    [Option('p', Description = "Path to ML.NET model")] string modelPath = DefaultModelPath)
{
    GitmoPredictor gitmoPredictor = new();
    gitmoPredictor.LoadFromFilePath(modelPath);

    var prediction = gitmoPredictor.Predict(new GitComment { Comment = message });
    Console.WriteLine("Prediction for " + message);
    Console.WriteLine(prediction.Emoji + " " + message);
}
