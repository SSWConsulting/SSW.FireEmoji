using Microsoft.ML;
using Microsoft.ML.AutoML;
using SSW.FireEmoji.Core.Models;

namespace SSW.FireEmoji.Trainer.Core;

public class GitmoTrainer : BaseTrainer<GitComment>
{
    public GitmoTrainer(MLContext? mlContext = null)
        : base(mlContext) { }

    protected override ColumnInformation GetColumnInformation()
    {
        ColumnInformation columnInfo = new() { LabelColumnName = "col0" };
        columnInfo.TextColumnNames.Add("col1");

        return columnInfo;
    }
}