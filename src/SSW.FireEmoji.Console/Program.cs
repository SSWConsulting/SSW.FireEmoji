using SSW.FireEmoji.Core.MachineLearning;
using SSW.FireEmoji.Core.Models;
using System.Text;
using Cocona;

Console.OutputEncoding = Encoding.UTF8;
Console.Write("\xfeff"); // bom = byte order mark

string ModelPath = "gitmo.mlnet";

static void Predict(string commit, [Option('p', Description = "Path to ML.NET model")] string model = "gitmo.mlnet")
{
    GitmoPredictor predictor = new();
    predictor.LoadFromFilePath(model);

    Console.WriteLine(Result.Emoji.Trim());
});
