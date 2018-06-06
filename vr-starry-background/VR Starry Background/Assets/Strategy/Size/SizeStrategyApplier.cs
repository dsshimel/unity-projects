using UnityEngine;
using System.Collections.Generic;

public class SizeStrategyApplier : IStrategyApplier<Vector3, ISizeStrategy>
{
    private ICollection<int> gameObjectIds;
    private IManipulator manipulator;

    public SizeStrategyApplier(IManipulator manipulator)
    {
        this.manipulator = manipulator;
        gameObjectIds = manipulator.GameObjectIds;
    }

    void IStrategyApplier<Vector3, ISizeStrategy>.Apply(ISizeStrategy strategy, float timeNow, float timeBefore)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            manipulator.SetLocalScale(gameObjectId, strategy.ComputeValue(gameObjectId, timeNow, timeBefore));
        }
    }

    void IStrategyApplier<Vector3, ISizeStrategy>.ApplyFade(ISizeStrategy strategyOut, ISizeStrategy strategyIn, float fadeOutPercent, float timeNow, float timeBefore)
    {
        throw new System.NotImplementedException();
    }
}