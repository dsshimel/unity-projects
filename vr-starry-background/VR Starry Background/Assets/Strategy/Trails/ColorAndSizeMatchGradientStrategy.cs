using UnityEngine;

public class ColorAndSizeMatchGradientStrategy : AbstractStaticStrategy<Gradient>, ITrailsStrategy
{
    IColorStrategy colorStrategy;
    ISizeStrategy sizeStrategy;

    public override CometProperty Property
    {
        get
        {
            // What to do here since this strategy manipulates two properties?
            // Could extract another strategy for the particle radius.
            return CometProperty.PARTICLE_COLOR_OVER_LIFETIME_GRADIENT;
        }
    }

    public ColorAndSizeMatchGradientStrategy(IManipulator manipulator, IColorStrategy colorStrat, ISizeStrategy sizeStrat) : base(manipulator)
    {
        colorStrategy = colorStrat;
        sizeStrategy = sizeStrat;
    }

    protected override void ApplyStrategyInternal(int gameObjectId)
    {
        manipulator.SetParticleRadius(gameObjectId, sizeStrategy.ComputeValue(gameObjectId, 0, 0).magnitude);
        manipulator.SetParticleColorOverLifetimeGradient(gameObjectId, ComputeStrategyValue(gameObjectId));
    }

    public override Gradient ComputeStrategyValue(int gameObjectId)
    {
        Color color = colorStrategy.ComputeValue(gameObjectId, 0, 0);

        Gradient grad = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[] {
        new GradientColorKey(color, 0.0f),
        new GradientColorKey(ColorHelper.InvertColor(color), 0.5f) };
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[] {
        new GradientAlphaKey(1.0f, 0.0f),
        new GradientAlphaKey(1.0f, 0.7f),
        new GradientAlphaKey(0.0f, 1.0f) };
        grad.SetKeys(colorKeys, alphaKeys);

        return grad;
    }

    public override Gradient CrossFadeValues(int gameObjectId, float timeNow, float timeBefore, IStrategy<Gradient> thatStrategy, float percentThis)
    {
        throw new System.NotImplementedException();
    }

    protected override void ApplyStrategyWithCrossfadeInternal(int gameObjectId, IStrategy<Gradient> thatStrategy, float percentThis)
    {
        throw new System.NotImplementedException();
    }
}