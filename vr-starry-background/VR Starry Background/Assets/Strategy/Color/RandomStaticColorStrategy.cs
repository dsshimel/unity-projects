using UnityEngine;
using System.Collections.Generic;

public class RandomStaticColorStrategy : AbstractStaticStrategy<Color>
{
    private IDictionary<int, Color> colorMap;

    public RandomStaticColorStrategy(IManipulator manipulator) : base(manipulator)
    {
        colorMap = new Dictionary<int, Color>();
        foreach (int gameObjectId in gameObjectIds)
        {
            colorMap.Add(gameObjectId, ColorHelper.RandomColor());
        }
    }

    public override Color ComputeStrategyValue(int gameObjectId)
    {
        return colorMap[gameObjectId];
    }
}