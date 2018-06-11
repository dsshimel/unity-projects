using UnityEngine;
using System.Collections.Generic;

public class ColorStrategyApplier : IStrategyApplier<Color>
{
    // This list should be immutable but Unity doesn't support a 
    // high-enough version of .NET to use System.Collections.Immutable.
    private readonly ICollection<int> gameObjectIds;
    private readonly IManipulator manipulator;
    private readonly IStrategy<Color> strategy;

    public ColorStrategyApplier(Manipulator manipulator, IStrategy<Color> strategy)
    {
        this.manipulator = manipulator;
        gameObjectIds = manipulator.Value;
    }

    void IStrategyApplier<Color>.Apply(float timeNow, float timeDelta)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            manipulator.SetMaterialColor(gameObjectId, strategy.ComputeValue(gameObjectId, timeNow, timeDelta));
        }
    }

    void IStrategyApplier<Color>.ApplyFade(IStrategy<Color> strategyIn, float fadeOutPercent, float timeNow, float timeDelta)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            var valueOut = strategy.ComputeValue(gameObjectId, timeNow, timeDelta);
            var valueIn = strategyIn.ComputeValue(gameObjectId, timeNow, timeDelta);

            manipulator.SetMaterialColor(gameObjectId, CrossfadeValues.FadeColor(valueOut, valueIn, fadeOutPercent));
        }
    }
}