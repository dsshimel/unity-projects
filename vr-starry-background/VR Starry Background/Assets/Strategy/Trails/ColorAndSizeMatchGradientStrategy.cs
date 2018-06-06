using System.Collections.Generic;
using UnityEngine;

public class ColorAndSizeMatchGradientStrategy : AbstractStaticStrategy<Gradient>, ITrailsStrategy
{
    private IColorStrategy colorStrategy;
    private IDictionary<int, Gradient> gradientMap;

    public ColorAndSizeMatchGradientStrategy(IManipulator manipulator, IColorStrategy colorStrat) : base(manipulator)
    {
        colorStrategy = colorStrat;

        gradientMap = new Dictionary<int, Gradient>();
        foreach (int gameObjectId in gameObjectIds)
        {
            Color color = colorStrategy.ComputeValue(gameObjectId, 0, 0);

            Gradient grad = new Gradient();
            GradientColorKey[] colorKeys = new GradientColorKey[]
            {
                new GradientColorKey(color, 0.0f),
                new GradientColorKey(ColorHelper.InvertColor(color), 0.5f)
            };
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

    public override Gradient ComputeStrategyValue(int gameObjectId)
    {
        return gradientMap[gameObjectId];
    }
}