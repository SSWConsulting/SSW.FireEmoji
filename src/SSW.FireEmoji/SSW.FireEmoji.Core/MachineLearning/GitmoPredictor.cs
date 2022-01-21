using SSW.FireEmoji.Core.Models;

namespace SSW.FireEmoji.Core.MachineLearning;

public interface IGitmoPredictor
{
    List<string> GetCategories();
    GitmojiPrediction Predict(GitComment input);
    Dictionary<string, float> PredictionList(GitComment input);
}

public class GitmoPredictor : BasePredictor<GitComment, GitmojiPrediction>, IGitmoPredictor
{
    // NOTE: No implementation required.
}
