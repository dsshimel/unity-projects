using System.Collections.Generic;
using UnityEngine;

public class ParticleSizeMatchStaticStrategy : AbstractStaticStrategy<float>
{
    private IStrategy<Vector3> sizeStrategy;
    private IDictionary<int, float> radiusMap;

    public ParticleSizeMatchStaticStrategy(
        IProvider<ICollection<int>> gameObjectIdProvider,
        IStrategy<Vector3> sizeStrategy,
        float intensityMin, 
        float intensityMax) : base(gameObjectIdProvider, intensityMin, intensityMax)
    {
        this.sizeStrategy = sizeStrategy;

        radiusMap = new Dictionary<int, float>();
        foreach (int gameObjectId in gameObjectIds)
        {
            radiusMap[gameObjectId] = sizeStrategy.ComputeInitialValue(gameObjectId).magnitude / 2;
        }
    }

    public override float ComputeStrategyValue(int gameObjectId)
    {
        return radiusMap[gameObjectId];
    }
}