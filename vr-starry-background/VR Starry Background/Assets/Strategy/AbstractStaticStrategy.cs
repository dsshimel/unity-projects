public abstract class AbstractStaticStrategy<T> : AbstractStrategy<T>
{
    public AbstractStaticStrategy(IManipulator manipulator) : base(manipulator)
    {
    }

    public override T ComputeValue(int gameObjectId, float timeNow, float timeDelta)
    {
        return ComputeStrategyValue(gameObjectId);
    }

    public abstract T ComputeStrategyValue(int gameObjectId);
}
