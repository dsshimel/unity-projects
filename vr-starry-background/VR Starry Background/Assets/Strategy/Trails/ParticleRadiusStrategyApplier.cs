using System.Collections.Generic;

public class ParticleRadiusStrategyApplier : IStrategyApplier<float>
{
    private readonly ICollection<int> gameObjectIds;
    private readonly IManipulator manipulator;
    private readonly IStrategy<float> strategy;

    public ParticleRadiusStrategyApplier(
        Manipulator manipulator,
        IStrategy<float> strategy)
    {
        this.manipulator = manipulator;
        gameObjectIds = manipulator.Value;
        this.strategy = strategy;
    }

    public IStrategy<float> Strategy
    {
        get
        {
            return strategy;
        }
    }

    public void Apply(float timeNow, float timeBefore)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            manipulator.SetParticleRadius(gameObjectId, strategy.ComputeValue(gameObjectId, timeNow, timeBefore));
        }
    }

    void IStrategyApplier<float>.ApplyFade(IStrategy<float> strategyIn, float fadeOutPercent, float timeNow, float timeDelta)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            var valueOut = strategy.ComputeValue(gameObjectId, timeNow, timeDelta);
            var valueIn = strategyIn.ComputeValue(gameObjectId, timeNow, timeDelta);
            manipulator.SetParticleRadius(gameObjectId, CrossfadeValues.FadeFloat(valueOut, valueIn, fadeOutPercent));
        }
    }
}