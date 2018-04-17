using UnityEngine;
using UnityEditor;

public class ColorMatchGradientStrategy : AbstractStrategy, ITrailsStrategy
{
    IColorStrategy colorStrategy;
    bool didApply;

    public ColorMatchGradientStrategy(IManipulator manipulator, IColorStrategy colorStrat) : base(manipulator)
    {
        colorStrategy = colorStrat;
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
            Color color = colorStrategy.GetColor(gameObjectId);

            Gradient grad = new Gradient();
            GradientColorKey[] colorKeys = new GradientColorKey[] {
            new GradientColorKey(color, 0.0f),
            new GradientColorKey(ColorHelper.InvertColor(color), 0.5f) };
            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[] {
            new GradientAlphaKey(1.0f, 0.0f),
            new GradientAlphaKey(1.0f, 0.7f),
            new GradientAlphaKey(0.0f, 1.0f) };
            grad.SetKeys(colorKeys, alphaKeys);

            manipulator.SetParticleColorOverLifetimeGradient(gameObjectId, grad);
        }

        didApply = true;
    }
}