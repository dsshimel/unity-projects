﻿using UnityEngine;
using System.Collections.Generic;

public class ColorStrategyApplier : IStrategyApplier<Color, IColorStrategy>
{
    // This list should be immutable but Unity doesn't support a 
    // high-enough version of .NET to use System.Collections.Immutable.
    private ICollection<int> gameObjectIds;
    private IManipulator manipulator;

    public ColorStrategyApplier(IManipulator manipulator)
    {
        this.manipulator = manipulator;
        gameObjectIds = manipulator.GameObjectIds;
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
        foreach (int gameObjectId in gameObjectIds)
        {
            var valueOut = strategyOut.ComputeValue(gameObjectId, timeNow, timeBefore);
            var valueIn = strategyIn.ComputeValue(gameObjectId, timeNow, timeBefore);

            manipulator.SetMaterialColor(gameObjectId, CrossfadeValues.FadeColor(valueOut, valueIn, fadeOutPercent));
        }
    }
}