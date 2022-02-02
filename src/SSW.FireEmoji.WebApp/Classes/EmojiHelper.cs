using System.Drawing;

namespace SSW.FireEmoji.WebApp.Classes;

public static class EmojiHelper
{
    private static Dictionary<string, Color> _emojiBackgrounds = new()
    {
        {
            "🔥", Color.FromArgb(204, 65, 65)
        },
        {
            "✨", Color.Gold
        },
        {
            "🐛", Color.Chartreuse
        }
    };

    public static string GetCssColor(Emoji? emoji)
    {
        if (_emojiBackgrounds.TryGetValue(emoji?.Character ?? string.Empty, out var background))
        {
            return $"background: rgb({background.R},{background.G},{background.B});";
        }

        return "background: #444444";
    }
}