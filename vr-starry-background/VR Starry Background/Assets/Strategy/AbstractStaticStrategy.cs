public abstract class AbstractStaticStrategy : AbstractStrategy
{
    protected bool didApply;

    public AbstractStaticStrategy(IManipulator manipulator) : base(manipulator)
    {
        didApply = false;
    }

    public override void ApplyStrategy(float time)
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

    protected abstract void ApplyStrategyInternal(int gameObjectId);
}