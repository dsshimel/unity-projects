using UnityEngine;
using UnityEditor;

public class ColorAndSizeMatchGradientStrategy : AbstractStaticStrategy, ITrailsStrategy
{
    IColorStrategy colorStrategy;
    ISizeStrategy sizeStrategy;

    public ColorAndSizeMatchGradientStrategy(IManipulator manipulator, IColorStrategy colorStrat, ISizeStrategy sizeStrat) : base(manipulator)
    {
        colorStrategy = colorStrat;
        sizeStrategy = sizeStrat;
    }

    protected override void ApplyStrategyInternal(int gameObjectId)
    {
        manipulator.SetParticleRadius(gameObjectId, sizeStrategy.GetSize(gameObjectId).magnitude);

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
}