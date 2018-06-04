﻿using UnityEngine;
using System.Collections.Generic;

public class RandomStaticSizeStrategy : AbstractStaticStrategy<Vector3>, ISizeStrategy
{
    private IDictionary<int, Vector3> scaleMap;

    public override CometProperty Property
    {
        get
        {
            return CometProperty.LOCAL_SCALE;
        }
    }

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

    protected override void ApplyStrategyInternal(int gameObjectId)
    {
        manipulator.SetLocalScale(gameObjectId, ComputeStrategyValue(gameObjectId));
    }

    public override Vector3 ComputeStrategyValue(int gameObjectId)
    {
        return scaleMap[gameObjectId];
    }

    public override Vector3 CrossFadeValues(int gameObjectId, float timeNow, float timeBefore, IStrategy<Vector3> thatStrategy, float percentThis)
    {
        // TODO: This is the same code for all Vector3 crossfading. Conslidated somehow.
        var valueThis = ComputeValue(gameObjectId, timeNow, timeBefore);
        var valueThat = thatStrategy.ComputeValue(gameObjectId, timeNow, timeBefore);
        var xfader = new CrossfadeValues.Vector3XFade(valueThis, valueThat, percentThis);
        return xfader.GetXFadeValue();
    }

    protected override void ApplyStrategyWithCrossfadeInternal(int gameObjectId, IStrategy<Vector3> thatStrategy, float percentThis)
    {
        // TODO: Shouldn't need to pass in zeros for timeNow and timeBefore in a static strategy
        manipulator.SetLocalScale(gameObjectId, CrossFadeValues(gameObjectId, 0, 0, thatStrategy, percentThis));
    }
}