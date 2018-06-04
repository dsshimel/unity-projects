using System;
using System.Collections.Generic;

public abstract class AbstractStrategyApplier<T> : IStrategyApplier<T>
{
    // This list should be immutable but Unity doesn't support a 
    // high-enough version of .NET to use System.Collections.Immutable.
    protected ICollection<int> gameObjectIds;
    protected IManipulator manipulator;

    public AbstractStrategyApplier(IManipulator manipulator)
    {
        this.manipulator = manipulator;
        gameObjectIds = manipulator.GetGameObjectIds();
    }

    void IStrategyApplier<T>.Apply(IStrategy<T> strategy, float timeNow, float timeBefore)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            ApplyInternal(strategy.Property, strategy.ComputeValue(gameObjectId, timeNow, timeBefore));
        }
    }

    protected abstract void ApplyInternal(CometProperty property, T value);

    void IStrategyApplier<T>.ApplyFade(IStrategy<T> strategyOut, IStrategy<T> strategyIn, float fadeOutPercent, float timeNow, float timeBefore)
    {
        throw new NotImplementedException();
    }
}