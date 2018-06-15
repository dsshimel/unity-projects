using System.Collections.Generic;

public abstract class AbstractStrategy<T> : IStrategy<T>
{
    // This list should be immutable but Unity doesn't support a 
    // high-enough version of .NET to use System.Collections.Immutable.
    protected readonly ICollection<int> gameObjectIds;
    protected readonly float maxIntensity;
    protected readonly float minIntensity;
    protected float intensity;

    public AbstractStrategy(IProvider<ICollection<int>> gameObjectIdProvider, float maxIntensity, float minIntensity)
    {
        this.maxIntensity = maxIntensity;
        this.minIntensity = minIntensity;
        gameObjectIds = gameObjectIdProvider.Value;
        this.intensity = maxIntensity;
    }

    public void SetIntensity(float intensity)
    {
        if (intensity < minIntensity|| maxIntensity < intensity)
        {
            throw new System.ArgumentException("intensity must be between " + minIntensity + " and " + maxIntensity);
        }
        this.intensity = intensity;
    }

    public abstract T ComputeValue(int gameObjectId, float timeNow, float timeDelta);

    public T ComputeInitialValue(int gameObjectId)
    {
        return ComputeValue(gameObjectId, /* timeNow= */ 0, /* timeDelta= */ 0);
    }
}