
public class Bundle : IBundle
{
    private IMovementStrategy movementStrategy;
    private IColorStrategy colorStrategy;
    private ITrailsStrategy trailsStrategy;

    public Bundle(IMovementStrategy movementStrat, IColorStrategy colorStrat, ITrailsStrategy trailsStrat)
    {
        movementStrategy = movementStrat;
        colorStrategy = colorStrat;
        trailsStrategy = trailsStrat;
    }

    public void ApplyStrategies()
    {
        movementStrategy.ApplyStrategy();
        colorStrategy.ApplyStrategy();
        trailsStrategy.ApplyStrategy();
    }

    public float IncrementTime(float delta)
    {
        movementStrategy.IncrementTime(delta);
        colorStrategy.IncrementTime(delta);
        float time = trailsStrategy.IncrementTime(delta);
        return time;
    }
}