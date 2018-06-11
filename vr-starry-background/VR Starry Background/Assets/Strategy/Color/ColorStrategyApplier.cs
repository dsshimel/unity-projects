using UnityEngine;
using System.Collections.Generic;

public class ColorStrategyApplier : IStrategyApplier<Color>
{
    // This list should be immutable but Unity doesn't support a 
    // high-enough version of .NET to use System.Collections.Immutable.
    private readonly ICollection<int> gameObjectIds;
    private readonly IManipulator manipulator;

    public ColorStrategyApplier(Manipulator manipulator)
    {
        this.manipulator = manipulator;
        gameObjectIds = manipulator.Value;
    }

    void IStrategyApplier<Color>.Apply(IStrategy<Color> strategy, float timeNow, float timeDelta)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            manipulator.SetMaterialColor(gameObjectId, strategy.ComputeValue(gameObjectId, timeNow, timeDelta));
        }
    }

    void IStrategyApplier<Color>.ApplyFade(IStrategy<Color> strategyOut, IStrategy<Color> strategyIn, float fadeOutPercent, float timeNow, float timeDelta)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            var valueOut = strategyOut.ComputeValue(gameObjectId, timeNow, timeDelta);
            var valueIn = strategyIn.ComputeValue(gameObjectId, timeNow, timeDelta);

            manipulator.SetMaterialColor(gameObjectId, CrossfadeValues.FadeColor(valueOut, valueIn, fadeOutPercent));
        }
    }
}