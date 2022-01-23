using Microsoft.ML.Data;

namespace SSW.FireEmoji.Core.Models;

public class GitComment
{
    [LoadColumn(0)]
    [ColumnName("col0")]
    public string? Emoji { get; set; }

    [LoadColumn(1)]
    [ColumnName("col1")]
    public string? Comment { get; set; }
}
