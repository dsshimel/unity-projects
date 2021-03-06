﻿using UnityEngine;
using System.Collections.Generic;

public class RandomStaticSizeStrategy : AbstractStaticStrategy<Vector3>
{
    private IDictionary<int, Vector3> scaleMap;
    private readonly float scaleMin;
    private readonly float scaleMax;

    public RandomStaticSizeStrategy(
        IProvider<ICollection<int>> gameObjectIdProvider, 
        float intensityMin, 
        float intensityMax,
        float scaleMin,
        float scaleMax) : base(gameObjectIdProvider, intensityMin, intensityMax)
    {
        this.scaleMin = scaleMin;
        this.scaleMax = scaleMax;

        scaleMap = new Dictionary<int, Vector3>();
        foreach (int gameObjectId in gameObjectIds)
        {
            // Might look better if this distribution was logarithmic instead of linear
            float newScale = Random.Range(scaleMin, scaleMax);
            scaleMap.Add(gameObjectId, new Vector3(newScale, newScale, newScale));
        }
    }

    public override Vector3 ComputeStrategyValue(int gameObjectId)
    {
        return scaleMap[gameObjectId];
    }
}