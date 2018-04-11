
public class Bundle : IBundle
{
    private IMovementStrategy movementStrategy;

    public Bundle(IMovementStrategy movementStrategy)
    {
        this.movementStrategy = movementStrategy;
    }
}