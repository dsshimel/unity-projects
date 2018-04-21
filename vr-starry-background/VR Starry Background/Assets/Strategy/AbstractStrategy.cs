using System.Collections.Generic;

public abstract class AbstractStrategy : IStrategy
{
    // This list should be immutable but Unity doesn't support a 
    // high-enough version of .NET to use System.Collections.Immutable.
    protected ICollection<int> gameObjectIds;
    protected IManipulator manipulator;
    protected float intensity;
    protected float time;

    public AbstractStrategy(IManipulator manipulator)
    {
        this.manipulator = manipulator;
        gameObjectIds = manipulator.GetGameObjectIds();
        time = 0;
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

    public float IncrementTime(float delta)
    {
        time += delta;
        ApplyStrategy(time);
        return time;
    }

    public abstract void ApplyStrategy(float time);
}