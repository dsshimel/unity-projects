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
            Color color = new Color(
                Random.Range(0, 1.0f),
                Random.Range(0, 1.0f),
                Random.Range(0, 1.0f));
            colorMap.Add(gameObjectId, color);
        }
    }

    public override Color ComputeStrategyValue(int gameObjectId)
    {
        return colorMap[gameObjectId];
    }
}