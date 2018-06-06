using System.Collections.Generic;
using UnityEngine;

// TODO: Figure out how to extend from an abstract class
public class MovementStrategyApplier : IStrategyApplier<Vector3, IMovementStrategy>
{
    private ICollection<int> gameObjectIds;
    private IManipulator manipulator;

    public MovementStrategyApplier(IManipulator manipulator)
    {
        this.manipulator = manipulator;
        gameObjectIds = manipulator.GetGameObjectIds();
    }

    void IStrategyApplier<Vector3, IMovementStrategy>.Apply(IMovementStrategy strategy, float timeNow, float timeBefore)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            manipulator.SetPosition(gameObjectId, strategy.ComputeValue(gameObjectId, timeNow, timeBefore));
        }
    }

    void IStrategyApplier<Vector3, IMovementStrategy>.ApplyFade(IMovementStrategy strategyOut, IMovementStrategy strategyIn, float fadeOutPercent, float timeNow, float timeBefore)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            var valueOut = strategyOut.ComputeValue(gameObjectId, timeNow, timeBefore);
            var valueIn = strategyIn.ComputeValue(gameObjectId, timeNow, timeBefore);

            manipulator.SetPosition(gameObjectId, CrossfadeValues.FadeVector3(valueOut, valueIn, fadeOutPercent));
        }
    }
}