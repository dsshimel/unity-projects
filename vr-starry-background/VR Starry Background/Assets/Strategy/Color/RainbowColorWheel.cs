using UnityEngine;
using System.Collections.Generic;

public class RainbowColorWheel : AbstractStrategy<Color>
{
    private readonly IDictionary<int, Color> colorMap;

    public readonly float duration;

    public RainbowColorWheel(
        IProvider<ICollection<int>> gameObjectIdProvider,
        float duration) : base(gameObjectIdProvider)
    {
        this.duration = duration;

        float hueSpacing = 1.0f / gameObjectIds.Count;
        float hueOffset = Random.Range(0, 1);
        
        colorMap = new Dictionary<int, Color>();
        var cometNumber = 0;
        foreach (int gameObjectId in gameObjectIds)
        {
            var hue = hueOffset + cometNumber * hueSpacing;
            if (hue > 1.0f)
            {
                hue -= 1.0f;
            }
            var color = Color.HSVToRGB(hue, /* saturation= */ 1.0f, /* value= */ 1.0f);
            cometNumber++;
            colorMap.Add(gameObjectId, color);
        }
    }

    public override Color ComputeValue(int gameObjectId, float timeNow, float timeDelta)
    {
        var color = colorMap[gameObjectId];
        var colorStep = intensity * timeDelta / duration;
        float hue, saturation, value;
        Color.RGBToHSV(color, out hue, out saturation, out value);
        hue += colorStep;
        if (hue > 1.0f)
        {
            hue -= 1;
        }

        saturation = intensity;

        color = Color.HSVToRGB(hue, saturation, value);
        colorMap[gameObjectId] = color;
        return color;
    }
}