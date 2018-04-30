using System.Collections.Generic;

public abstract class AbstractStrategy<T> : IStrategy<T>
{
    // This list should be immutable but Unity doesn't support a 
    // high-enough version of .NET to use System.Collections.Immutable.
    protected ICollection<int> gameObjectIds;
    protected IManipulator manipulator;
    protected float intensity;

    public AbstractStrategy(IManipulator manipulator)
    {
        this.manipulator = manipulator;
        gameObjectIds = manipulator.GetGameObjectIds();
        intensity = 1.0f;
    }

    public void SetIntensity(float intensity)
    {
        if (intensity <= 0)
        {
            throw new System.ArgumentException("intensity must be positive");
        }
        this.intensity = intensity;
    }

    public abstract void ApplyStrategy(float timeNow, float timeBefore);

    public abstract T ComputeStrategyValue(int gameObjectId, float timeNow, float timeBefore);

    public abstract T CrossFadeStrategyValues(int gameObjectId, float timeNow, float timeBefore, IStrategy<T> otherStrategy, float percentThis);

    public abstract void ApplyStrategyWithCrossfade(float timeNow, float timeBefore, IStrategy<T> thatStrategy, float percentThis);
}