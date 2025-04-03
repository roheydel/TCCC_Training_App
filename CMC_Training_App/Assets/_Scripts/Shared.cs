using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public static class Shared
{
    public static void UpdateButtonColor(Button button, Color color)
    {
        if (button != null)
        {
            var cb = button!.colors;
            cb.normalColor = color;
            cb.selectedColor = color;
            cb.highlightedColor = color;
            button.colors = cb;
        }
    }

    public static string SerializeToJson(object data)
    {
        return JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
    }
}