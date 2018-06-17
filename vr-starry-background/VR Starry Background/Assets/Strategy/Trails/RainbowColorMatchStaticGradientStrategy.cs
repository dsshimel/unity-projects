using UnityEngine;
using System.Collections.Generic;

public class RainbowColorMatchStaticGradientStrategy : AbstractStaticStrategy<Gradient>
{
    private static readonly int numRainbowSegments = 6;
    private static readonly float rainbowSegmentLength = 1.0f / numRainbowSegments;
    private IStrategy<Color> colorStrategy;
    private IDictionary<int, Gradient> gradientMap;

    public RainbowColorMatchStaticGradientStrategy(
        IProvider<ICollection<int>> gameObjectIdProvider,
        IStrategy<Color> colorStrategy,
        float intensityMin,
        float intensityMax) : base(gameObjectIdProvider, intensityMin, intensityMax)
    {
        this.colorStrategy = colorStrategy;

        gradientMap = new Dictionary<int, Gradient>();
        foreach (int gameObjectId in gameObjectIds)
        {
            Color color = this.colorStrategy.ComputeInitialValue(gameObjectId);

            Gradient grad = new Gradient();
            GradientColorKey[] colorKeys = new GradientColorKey[numRainbowSegments];
            for (var i = 0; i < numRainbowSegments; i++)
            {
                float hueOffset = i * rainbowSegmentLength;
                colorKeys[i] = new GradientColorKey(getRainbowColor(color, hueOffset), hueOffset);
            }
            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[]
            {
                new GradientAlphaKey(1.0f, 0.0f),
                new GradientAlphaKey(1.0f, 0.7f),
                new GradientAlphaKey(0.0f, 1.0f)
            };
            grad.SetKeys(colorKeys, alphaKeys);

            gradientMap.Add(gameObjectId, grad);
        }
    }

    private Color getRainbowColor(Color color, float hueOffset)
    {
        float h, s, v;
        Color.RGBToHSV(color, out h, out s, out v);
        h += hueOffset;
        if (h > 1.0f)
        {
            h -= 1.0f;
        }
        return Color.HSVToRGB(h, s, v);
    }

    public override Gradient ComputeStrategyValue(int gameObjectId)
    {
        return gradientMap[gameObjectId];
    }
}