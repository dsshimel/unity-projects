public abstract class AbstractStaticStrategy<T> : AbstractStrategy<T>
{
    public AbstractStaticStrategy(IManipulator manipulator) : base(manipulator)
    {
    }

    public override T ComputeValue(int gameObjectId, float timeNow, float timeBefore)
    {
        return ComputeStrategyValue(gameObjectId);
    }

    public abstract T ComputeStrategyValue(int gameObjectId);
}
