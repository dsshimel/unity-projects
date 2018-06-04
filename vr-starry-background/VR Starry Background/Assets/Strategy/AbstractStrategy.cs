using System.Collections.Generic;

public abstract class AbstractStrategy<T> : IStrategy<T>
{
    // This list should be immutable but Unity doesn't support a 
    // high-enough version of .NET to use System.Collections.Immutable.
    protected ICollection<int> gameObjectIds;
    protected IManipulator manipulator;
    protected float intensity;

    public abstract CometProperty Property { get; }

    public AbstractStrategy(IManipulator manipulator)
    {
        this.manipulator = manipulator;
        gameObjectIds = manipulator.GetGameObjectIds();
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

    public abstract void Apply(float timeNow, float timeBefore);

    public abstract T ComputeValue(int gameObjectId, float timeNow, float timeBefore);

    public abstract T CrossFadeValues(int gameObjectId, float timeNow, float timeBefore, IStrategy<T> otherStrategy, float percentThis);

    public abstract void ApplyStrategyWithCrossfade(float timeNow, float timeBefore, IStrategy<T> thatStrategy, float percentThis);
}