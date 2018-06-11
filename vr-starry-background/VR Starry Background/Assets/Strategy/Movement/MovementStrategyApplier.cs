using System.Collections.Generic;
using UnityEngine;

// TODO: Figure out how to extend from an abstract class
public class MovementStrategyApplier : IStrategyApplier<Vector3>
{
    private readonly ICollection<int> gameObjectIds;
    private readonly IManipulator manipulator;
    private readonly IStrategy<Vector3> strategy;

    public MovementStrategyApplier(Manipulator manipulator, IStrategy<Vector3> strategy)
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

    public void Apply(float timeNow, float timeDelta)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            manipulator.SetPosition(gameObjectId, strategy.ComputeValue(gameObjectId, timeNow, timeDelta));
        }
    }

    public void ApplyFade(IStrategy<Vector3> strategyIn, float fadeOutPercent, float timeNow, float timeDelta)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            var valueOut = strategy.ComputeValue(gameObjectId, timeNow, timeDelta);
            var valueIn = strategyIn.ComputeValue(gameObjectId, timeNow, timeDelta);

            manipulator.SetPosition(gameObjectId, CrossfadeValues.FadeVector3(valueOut, valueIn, fadeOutPercent));
        }
    }
}