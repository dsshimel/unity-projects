using UnityEngine;
using System.Collections.Generic;

public class SizeStrategyApplier : IStrategyApplier<Vector3>
{
    private ICollection<int> gameObjectIds;
    private IManipulator manipulator;

    public SizeStrategyApplier(IManipulator manipulator)
    {
        this.manipulator = manipulator;
        gameObjectIds = manipulator.GameObjectIds;
    }

    void IStrategyApplier<Vector3>.Apply(IStrategy<Vector3> strategy, float timeNow, float timeBefore)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            manipulator.SetLocalScale(gameObjectId, strategy.ComputeValue(gameObjectId, timeNow, timeBefore));
        }
    }

    void IStrategyApplier<Vector3>.ApplyFade(IStrategy<Vector3> strategyOut, IStrategy<Vector3> strategyIn, float fadeOutPercent, float timeNow, float timeBefore)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            var valueOut = strategyOut.ComputeValue(gameObjectId, timeNow, timeBefore);
            var valueIn = strategyIn.ComputeValue(gameObjectId, timeNow, timeBefore);

            manipulator.SetLocalScale(gameObjectId, CrossfadeValues.FadeVector3(valueOut, valueIn, fadeOutPercent));
        }
    }
}