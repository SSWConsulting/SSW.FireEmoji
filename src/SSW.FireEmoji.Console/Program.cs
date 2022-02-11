using SSW.FireEmoji.Core.MachineLearning;
using SSW.FireEmoji.Core.Models;
using System.Text;
using Cocona;

Console.OutputEncoding = Encoding.UTF8;
Console.Write("\xfeff"); // bom = byte order mark

CoconaLiteApp.Run(Predict);

static void Predict(string commit, [Option('p', Description = "Path to ML.NET model")] string modelPath = "gitmo.mlnet")
{
    GitmoPredictor predictor = new();
    predictor.LoadFromFilePath(modelPath);

    var result = predictor.Predict(new GitComment { Comment = commit });
    Console.WriteLine(result.Emoji.Trim());
}
