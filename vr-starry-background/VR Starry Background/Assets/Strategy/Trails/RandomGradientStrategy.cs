using UnityEngine;
using System.Collections.Generic;

public class RandomGradientStrategy : AbstractStaticStrategy<Gradient>
{
    private IDictionary<int, Gradient> gradientMap;

    public RandomGradientStrategy(IManipulator manipulator) : base(manipulator)
    {
        gradientMap = new Dictionary<int, Gradient>();
        foreach (int gameObjectId in gameObjectIds)
        {
            Color color = ColorHelper.RandomColor();

            Gradient grad = new Gradient();
            GradientColorKey[] colorKeys = new GradientColorKey[] {
            new GradientColorKey(color, 0.0f),
            new GradientColorKey(ColorHelper.InvertColor(color), 0.5f) };
            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[] {
            new GradientAlphaKey(1.0f, 0.0f),
            new GradientAlphaKey(1.0f, 0.7f),
            new GradientAlphaKey(0.0f, 1.0f) };
            grad.SetKeys(colorKeys, alphaKeys);
            gradientMap.Add(gameObjectId, grad);
        }
    }

    public override Gradient ComputeStrategyValue(int gameObjectId)
    {
        return gradientMap[gameObjectId];
    }
}