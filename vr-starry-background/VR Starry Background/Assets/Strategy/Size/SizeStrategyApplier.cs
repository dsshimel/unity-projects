using UnityEngine;
using System.Collections.Generic;

public class SizeStrategyApplier : IStrategyApplier<Vector3>
{
    private ICollection<int> gameObjectIds;
    private IManipulator manipulator;

    public SizeStrategyApplier(Manipulator manipulator)
    {
        this.manipulator = manipulator;
        gameObjectIds = manipulator.Value;
    }

    void IStrategyApplier<Vector3>.Apply(IStrategy<Vector3> strategy, float timeNow, float timeDelta)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            manipulator.SetLocalScale(gameObjectId, strategy.ComputeValue(gameObjectId, timeNow, timeDelta));
        }
    }

    void IStrategyApplier<Vector3>.ApplyFade(
        IStrategy<Vector3> strategyOut, IStrategy<Vector3> strategyIn, float fadeOutPercent, float timeNow, float timeDelta)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            var valueOut = strategyOut.ComputeValue(gameObjectId, timeNow, timeDelta);
            var valueIn = strategyIn.ComputeValue(gameObjectId, timeNow, timeDelta);

            manipulator.SetLocalScale(gameObjectId, CrossfadeValues.FadeVector3(valueOut, valueIn, fadeOutPercent));
        }
    }
}