using System.Collections.Generic;

public abstract class AbstractStrategy : IStrategy
{
    // This list should be immutable but Unity doesn't support a 
    // high-enough version of .NET to use System.Collections.Immutable.
    protected IList<int> gameObjectIds;
    protected IManipulator manipulator;
    protected float intensity;
    protected float time;

    public AbstractStrategy(IList<int> gameObjectIds, IManipulator manipulator)
    {
        this.manipulator = manipulator;
        this.gameObjectIds = gameObjectIds;
    }

    public void SetIntensity(float intensity)
    {
        this.intensity = intensity;
    }

    public float IncrementTime(float delta)
    {
        time += delta;
        return time;
    }
}