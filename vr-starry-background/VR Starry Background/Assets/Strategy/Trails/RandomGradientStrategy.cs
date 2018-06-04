using UnityEngine;
using System.Collections.Generic;

public class RandomGradientStrategy : AbstractStaticStrategy<Gradient>, ITrailsStrategy
{
    private IDictionary<int, Gradient> gradientMap;

    public override CometProperty Property
    {
        get
        {
            return CometProperty.PARTICLE_COLOR_OVER_LIFETIME_GRADIENT;
        }
    }

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
    }

    protected override void ApplyStrategyInternal(int gameObjectId)
    {
        manipulator.SetParticleColorOverLifetimeGradient(
            gameObjectId, ComputeStrategyValue(gameObjectId));
    }

    public override Gradient ComputeStrategyValue(int gameObjectId)
    {
        return gradientMap[gameObjectId];
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