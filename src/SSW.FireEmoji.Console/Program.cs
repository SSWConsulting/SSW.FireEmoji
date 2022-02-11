using SSW.FireEmoji.Core.MachineLearning;
using SSW.FireEmoji.Core.Models;
using System.Text;
using Cocona;

Console.OutputEncoding = Encoding.UTF8;
Console.Write("\xfeff"); // bom = byte order mark

string ModelPath = "gitmo.mlnet";

GitmoPredictor gitmoPredictor = new GitmoPredictor();

CoconaLiteApp.Run((string commit, string? model) =>
{
    if (model != string.Empty)
    {
        ModelPath = model;
    }

    gitmoPredictor.LoadFromFilePath(ModelPath);
    var Result = gitmoPredictor.Predict(new GitComment() { Comment = commit });

    Console.WriteLine(Result.Emoji.Trim());
});
