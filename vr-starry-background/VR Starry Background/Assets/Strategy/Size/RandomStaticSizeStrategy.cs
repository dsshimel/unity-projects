using UnityEngine;
using System.Collections.Generic;

public class RandomStaticSizeStrategy : AbstractStrategy, ISizeStrategy
{
    private IDictionary<int, Vector3> scaleMap;
    private bool didApply;

    public RandomStaticSizeStrategy(IManipulator manipulator) : base(manipulator)
    {
        scaleMap = new Dictionary<int, Vector3>();
        foreach (int gameObjectId in gameObjectIds)
        {
            // Might look better if this distribution was logarithmic instead of linear
            float newScale = Random.Range(0.01f, 2);
            scaleMap.Add(gameObjectId, new Vector3(newScale, newScale, newScale));
        }

        didApply = false;
    }

    public override void ApplyStrategy()
    {
        if (didApply)
        {
            return;
        }

        foreach (int gameObjectId in gameObjectIds)
        {
            manipulator.SetLocalScale(gameObjectId, scaleMap[gameObjectId]);
        }
        didApply = true;
    }


}