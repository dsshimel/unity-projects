using System.Collections.Generic;
using UnityEngine;

// TODO: Figure out how to extend from an abstract class
public class MovementStrategyApplier : IStrategyApplier<Vector3>
{
    private ICollection<int> gameObjectIds;
    private IManipulator manipulator;

    public MovementStrategyApplier(IManipulator manipulator)
    {
        this.manipulator = manipulator;
        gameObjectIds = manipulator.GameObjectIds;
    }

    public void Apply(IStrategy<Vector3> strategy, float timeNow, float timeBefore)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            manipulator.SetPosition(gameObjectId, strategy.ComputeValue(gameObjectId, timeNow, timeBefore));
        }
    }

    public void ApplyFade(IStrategy<Vector3> strategyOut, IStrategy<Vector3> strategyIn, float fadeOutPercent, float timeNow, float timeBefore)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            var valueOut = strategyOut.ComputeValue(gameObjectId, timeNow, timeBefore);
            var valueIn = strategyIn.ComputeValue(gameObjectId, timeNow, timeBefore);

            manipulator.SetPosition(gameObjectId, CrossfadeValues.FadeVector3(valueOut, valueIn, fadeOutPercent));
        }
    }
}