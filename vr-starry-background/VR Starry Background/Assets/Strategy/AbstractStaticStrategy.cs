public abstract class AbstractStaticStrategy<T> : AbstractStrategy<T>
{
    protected bool didApply;

    public AbstractStaticStrategy(IManipulator manipulator) : base(manipulator)
    {
        didApply = false;
    }

    public override void ApplyStrategy(float timeNow, float timeBefore)
    {
        if (didApply)
        {
            return;
        }

        foreach (int gameObjectId in gameObjectIds)
        {
            ApplyStrategyInternal(gameObjectId);
        }
        didApply = true;
    }

    public override T ComputeStrategyValue(int gameObjectId, float timeNow, float timeBefore)
    {
        return ComputeStrategyValue(gameObjectId);
    }

    public abstract T ComputeStrategyValue(int gameObjectId);

    protected abstract void ApplyStrategyInternal(int gameObjectId);
}