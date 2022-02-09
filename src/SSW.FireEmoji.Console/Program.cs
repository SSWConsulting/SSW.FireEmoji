// See https://aka.ms/new-console-template for more information

using SSW.FireEmoji.Core.MachineLearning;
using SSW.FireEmoji.Core.Models;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.Write("\xfeff"); // bom = byte order mark

string ModelPath = "gitmo.mlnet";

GitmoPredictor gitmoPredictor = new GitmoPredictor();

gitmoPredictor.LoadFromFilePath(ModelPath);

var commitMsg = args.AsQueryable().FirstOrDefault();

if (commitMsg == null)
{
    throw new Exception("No Message parsed");
}

var res = gitmoPredictor.Predict(new GitComment() { Comment = commitMsg });

Console.WriteLine(res.Emoji.Trim());