using UnityEngine;
using UnityEditor;

public class ColorHelper
{
    private ColorHelper()
    {
    }

    public static Color InvertColor(Color color)
    {
        return new Color(1.0f - color.r, 1.0f - color.g, 1.0f - color.b);
    }
}