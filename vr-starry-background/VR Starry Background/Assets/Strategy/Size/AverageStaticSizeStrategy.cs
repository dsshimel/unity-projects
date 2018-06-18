using UnityEngine;
using System.Collections.Generic;

public class AverageStaticSizeStrategy : AbstractStaticStrategy<Vector3>
{
    private IDictionary<int, Vector3> scaleMap;
    private readonly float scaleMin;
    private readonly float scaleMax;

    public AverageStaticSizeStrategy(
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
            float newScale = (scaleMin + 2 * scaleMax) / 3;
            scaleMap.Add(gameObjectId, new Vector3(newScale, newScale, newScale));
        }
    }

    public override Vector3 ComputeStrategyValue(int gameObjectId)
    {
        return scaleMap[gameObjectId];
    }
}