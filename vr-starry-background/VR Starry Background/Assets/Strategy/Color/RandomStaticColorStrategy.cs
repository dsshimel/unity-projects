using UnityEngine;
using System.Collections.Generic;

public class RandomStaticColorStrategy : AbstractStaticStrategy<Color>, IColorStrategy
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

    protected override void ApplyStrategyInternal(int gameObjectId)
    {
        manipulator.SetMaterialColor(gameObjectId, ComputeStrategyValue(gameObjectId));
    }

    public override Color ComputeStrategyValue(int gameObjectId)
    {
        return colorMap[gameObjectId];
    }

    public override Color CrossFadeStrategyValues(int gameObjectId, float timeNow, float timeBefore, IStrategy<Color> thatStrategy, float percentThis)
    {
        throw new System.NotImplementedException();
    }

    protected override void ApplyStrategyWithCrossfadeInternal(int gameObjectId, IStrategy<Color> thatStrategy, float percentThis)
    {
        throw new System.NotImplementedException();
    }
}