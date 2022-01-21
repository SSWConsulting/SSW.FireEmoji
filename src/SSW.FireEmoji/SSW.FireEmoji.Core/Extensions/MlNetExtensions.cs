using Microsoft.ML.Data;

namespace Microsoft.ML;

public static class MlNetExtensions
{
    public static List<string> GetCategories(this DataViewSchema schema)
    {
        // Based on https://github.com/dotnet/docs/issues/14265
        var column = schema.GetColumnOrNull("Score");
        if (column == null)
        {
            return new List<string>();
        }

        var slotNames = new VBuffer<ReadOnlyMemory<char>>();
        column.Value.GetSlotNames(ref slotNames);

        // NOTE: Do not sort the list as many features depends on correct order.
        return slotNames
            .DenseValues()
            .Select(x => x.ToString())
            .ToList();
    }

    public static Dictionary<string, float>? GetCategoriesWithScore(this DataViewSchema schema, float[] scores)
    {
        // Based on https://github.com/dotnet/docs/issues/14265
        Dictionary<string, float> result = new();

        int num = 0;
        var categories = GetCategories(schema);
        foreach (var denseValue in categories)
        {
            result.Add(denseValue, scores[num++]);
        }

        return result
            .OrderByDescending(c => c.Value)
            .ToDictionary(i => i.Key, i => i.Value);
    }

    public static Dictionary<string, float>? GetCategoriesWithScore(List<string> categories, float[] scores)
    {
        int num = 0;
        Dictionary<string, float> result = new();
        foreach (string category in categories)
        {
            result.Add(category, scores[num++]);
        }

        return result
            .OrderByDescending(c => c.Value)
            .ToDictionary(i => i.Key, i => i.Value);
    }
}
