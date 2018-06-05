using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ColorStrategyApplier : IStrategyApplier<Color, IColorStrategy>
{
    // This list should be immutable but Unity doesn't support a 
    // high-enough version of .NET to use System.Collections.Immutable.
    protected ICollection<int> gameObjectIds;
    protected IManipulator manipulator;

    public ColorStrategyApplier(IManipulator manipulator)
    {
        this.manipulator = manipulator;
        gameObjectIds = manipulator.GetGameObjectIds();
    }

    void IStrategyApplier<Color, IColorStrategy>.Apply(IColorStrategy strategy, float timeNow, float timeBefore)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            manipulator.SetMaterialColor(gameObjectId, strategy.ComputeValue(gameObjectId, timeNow, timeBefore));
        }
    }

    void IStrategyApplier<Color, IColorStrategy>.ApplyFade(IColorStrategy strategyOut, IColorStrategy strategyIn, float fadeOutPercent, float timeNow, float timeBefore)
    {
        throw new System.NotImplementedException();
    }
}