
public class Bundle : IBundle
{
    private IMovementStrategy movementStrategy;
    private IColorStrategy colorStrategy;

    public Bundle(IMovementStrategy movementStrategy, IColorStrategy colorStrategy)
    {
        this.movementStrategy = movementStrategy;
        this.colorStrategy = colorStrategy;
    }

    public void ApplyStrategies()
    {
        movementStrategy.ApplyStrategy();
        colorStrategy.ApplyStrategy();
    }

    public float IncrementTime(float delta)
    {
        movementStrategy.IncrementTime(delta);
        float time = colorStrategy.IncrementTime(delta);
        return time;
    }
}