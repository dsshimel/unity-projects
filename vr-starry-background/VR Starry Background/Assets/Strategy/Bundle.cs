
using System.Collections;
using System.Collections.Generic;

public class Bundle : IBundle
{
    private IList<IStrategy> strategies;

    public Bundle(IMovementStrategy movementStrat, IColorStrategy colorStrat, ITrailsStrategy trailsStrat, ISizeStrategy sizeStrat)
    {
        strategies = new List<IStrategy>();
        strategies.Add(movementStrat);
        strategies.Add(colorStrat);
        strategies.Add(trailsStrat);
        strategies.Add(sizeStrat);
    }

    public void ApplyStrategies()
    {
        foreach (IStrategy strat in strategies)
        {
            strat.ApplyStrategy();
        }
    }

    public void IncrementTime(float delta)
    {
        foreach (IStrategy strat in strategies)
        {
            strat.IncrementTime(delta);
        }
    }
}