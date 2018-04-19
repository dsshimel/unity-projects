using UnityEngine;
using System.Collections.Generic;

public class RandomStaticSizeStrategy : AbstractStaticStrategy, ISizeStrategy
{
    private IDictionary<int, Vector3> scaleMap;

    public RandomStaticSizeStrategy(IManipulator manipulator) : base(manipulator)
    {
        scaleMap = new Dictionary<int, Vector3>();
        foreach (int gameObjectId in gameObjectIds)
        {
            // Might look better if this distribution was logarithmic instead of linear
            float newScale = Random.Range(0.01f, 2);
            scaleMap.Add(gameObjectId, new Vector3(newScale, newScale, newScale));
        }
    }

    public Vector3 GetSize(int gameObjectId)
    {
        return scaleMap[gameObjectId];
    }

    protected override void ApplyStrategyInternal(int gameObjectId)
    {
        manipulator.SetLocalScale(gameObjectId, scaleMap[gameObjectId]);
    }
}