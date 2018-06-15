using System.Collections.Generic;

public abstract class AbstractStaticStrategy<T> : AbstractStrategy<T>
{
    public AbstractStaticStrategy(
        IProvider<ICollection<int>> gameObjectIdProvider,
        float maxIntensity, 
        float minIntensity) : base(gameObjectIdProvider, maxIntensity, minIntensity)
    {
    }

    public override T ComputeValue(int gameObjectId, float timeNow, float timeDelta)
    {
        return ComputeStrategyValue(gameObjectId);
    }

    public abstract T ComputeStrategyValue(int gameObjectId);
}
