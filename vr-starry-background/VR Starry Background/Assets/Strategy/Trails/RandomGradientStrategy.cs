using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class RandomGradientStrategy : AbstractStrategy, ITrailsStrategy
{
    private IDictionary<int, Gradient> gradientMap;
    private bool didApply;

    public RandomGradientStrategy(IManipulator manipulator) : base(manipulator)
    {
        gradientMap = new Dictionary<int, Gradient>();
        foreach (int gameObjectId in gameObjectIds)
        {
            Color color = new Color(
                Random.Range(0, 1.0f),
                Random.Range(0, 1.0f),
                Random.Range(0, 1.0f));

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

        didApply = false;
    }

    public override void ApplyStrategy()
    {
        if (didApply)
        {
            return;
        }

        foreach (int gameObjectId in gameObjectIds)
        {
            manipulator.SetParticleColorOverLifetimeGradient(gameObjectId, gradientMap[gameObjectId]);
        }

        didApply = true;
    }
}