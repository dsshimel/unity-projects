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
        // TODO: Get rid of the null check.
        this.gameObjectIds = manipulator == null ? new List<int>() : manipulator.GetGameObjectIds();
    }

    public void SetIntensity(float intensity)
    {
        this.intensity = intensity;
    }

    public float IncrementTime(float delta)
    {
        time += delta;
        ApplyStrategy();
        return time;
    }

    public abstract void ApplyStrategy();
}