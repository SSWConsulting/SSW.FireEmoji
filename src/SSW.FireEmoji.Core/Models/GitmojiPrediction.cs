using Microsoft.ML.Data;

namespace SSW.FireEmoji.Core.Models;

public interface IPrediction
{
    float[] Score { get; }
}

public class GitmojiPrediction : IPrediction
{
    [ColumnName("PredictedLabel")]
    public string Emoji { get; set; }

    public float[] Score { get; set; }
}
