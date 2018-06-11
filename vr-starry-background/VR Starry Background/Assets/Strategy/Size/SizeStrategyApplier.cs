using UnityEngine;
using System.Collections.Generic;

public class SizeStrategyApplier : IStrategyApplier<Vector3>
{
    private readonly ICollection<int> gameObjectIds;
    private readonly IManipulator manipulator;
    private readonly IStrategy<Vector3> strategy;

    public SizeStrategyApplier(Manipulator manipulator, IStrategy<Vector3> strategy)
    {
        this.manipulator = manipulator;
        gameObjectIds = manipulator.Value;
        this.strategy = strategy;
    }

    public IStrategy<Vector3> Strategy
    {
        get
        {
            return strategy;
        }
    }

    void IStrategyApplier<Vector3>.Apply(float timeNow, float timeDelta)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            manipulator.SetLocalScale(gameObjectId, strategy.ComputeValue(gameObjectId, timeNow, timeDelta));
        }
    }

    void IStrategyApplier<Vector3>.ApplyFade(IStrategy<Vector3> strategyIn, float fadeOutPercent, float timeNow, float timeDelta)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            var valueOut = strategy.ComputeValue(gameObjectId, timeNow, timeDelta);
            var valueIn = strategyIn.ComputeValue(gameObjectId, timeNow, timeDelta);

            manipulator.SetLocalScale(gameObjectId, CrossfadeValues.FadeVector3(valueOut, valueIn, fadeOutPercent));
        }
    }
}