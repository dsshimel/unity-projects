using System.Collections.Generic;

public abstract class AbstractStrategy<T> : IStrategy<T>
{
    // This list should be immutable but Unity doesn't support a 
    // high-enough version of .NET to use System.Collections.Immutable.
    protected readonly ICollection<int> gameObjectIds;
    protected float intensity;

    public AbstractStrategy(IProvider<ICollection<int>> gameObjectIdProvider)
    {
        gameObjectIds = gameObjectIdProvider.Value;
        intensity = 1.0f;
    }

    public void SetIntensity(float intensity)
    {
        if (intensity <= 0 || 1 < intensity)
        {
            throw new System.ArgumentException("intensity must be between 0 and 1");
        }
        this.intensity = intensity;
    }

    public abstract T ComputeValue(int gameObjectId, float timeNow, float timeDelta);

    public T ComputeInitialValue(int gameObjectId)
    {
        return ComputeValue(gameObjectId, /* timeNow= */ 0, /* timeDelta= */ 0);
    }
}