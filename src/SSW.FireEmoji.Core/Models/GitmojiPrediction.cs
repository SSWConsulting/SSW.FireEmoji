using Microsoft.ML.Data;

namespace SSW.FireEmoji.Core.Models;

public class GitmojiPrediction
{
    [ColumnName("PredictedLabel")]
    public string Emoji { get; set; }

    public float[] Score { get; set; }
}
