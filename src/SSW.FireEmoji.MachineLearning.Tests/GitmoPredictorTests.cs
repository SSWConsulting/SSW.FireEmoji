using FluentAssertions;
using SSW.FireEmoji.Core.MachineLearning;
using SSW.FireEmoji.Core.Models;
using System;
using System.IO;
using Xunit;

namespace SSW.FireEmoji.MachineLearning.Tests;

public class GitmoPredictorTests
{
    private static readonly string _modelPath = GetModelPath();

    [Theory]
    [InlineData("Add more emojis", "✨")]
    [InlineData("Init project", "🎉")]
    [InlineData("Add button", "✨")]
    [InlineData("Add button styling", "💄")]
    [InlineData("Update build", "💚")]
    [InlineData("Fixed build", "💚")]
    [InlineData("Remove", "🔥")]
    [InlineData("Added tests for machine learning", "✅")]
    [InlineData("Moved projects around", "♻️")]
    public void ShouldClassifyCorrectly(string comment, string emoji)
    {
        GitmoPredictor predictor = new();
        predictor.LoadFromFilePath(_modelPath);

        GitmojiPrediction? prediction = predictor.Predict(new GitComment { Comment = comment });
        prediction.Emoji.Should().Be(emoji);
    }

    public static string GetModelPath()
    {
        string[] possibleModelPaths =
        {
            "Model/gitmo.mlnet",
            "../../Model/gitmo.mlnet",
            "../../../../../Model/gitmo.mlnet"
        };

        var modelPath = Array.Find(possibleModelPaths, File.Exists);
        if (modelPath == null)
        {
            throw new FileNotFoundException("ML.NET Model not found!");
        }

        return modelPath;
    }
}
