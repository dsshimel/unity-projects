
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

    public void IncrementTime(float delta)
    {
        movementStrategy.IncrementTime(delta);
        colorStrategy.IncrementTime(delta);
        trailsStrategy.IncrementTime(delta);
    }
}