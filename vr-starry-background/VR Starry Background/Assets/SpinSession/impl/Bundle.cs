using System;
using System.Collections.Generic;

public class Bundle : IBundle
{
    private IList<IStrategyUntyped> strategies;
    private IMovementStrategy movementStrategy;
    private IColorStrategy colorStrategy;
    private ITrailsStrategy trailsStrategy;
    private ISizeStrategy sizeStrategy;

    public Bundle(IMovementStrategy movementStrat, IColorStrategy colorStrat, ITrailsStrategy trailsStrat, ISizeStrategy sizeStrat)
    {
        strategies = new List<IStrategyUntyped>();
        strategies.Add(movementStrat);
        strategies.Add(colorStrat);
        strategies.Add(trailsStrat);
        strategies.Add(sizeStrat);

        movementStrategy = movementStrat;
        colorStrategy = colorStrat;
        trailsStrategy = trailsStrat;
        sizeStrategy = sizeStrat;
    }

    public void ApplyStrategies(float timeNow, float timeBefore)
    {
        foreach (var strat in strategies)
        {
            strat.Apply(timeNow, timeBefore);
        }
    }

    public void SetIntensities(float intensity)
    {
        foreach (var strat in strategies)
        {
            strat.SetIntensity(intensity);
        }
    }

    public IColorStrategy GetColorStrategy()
    {
        return colorStrategy;
    }

    public IMovementStrategy GetMovementStrategy()
    {
        return movementStrategy;
    }

    public ISizeStrategy GetSizeStrategy()
    {
        return sizeStrategy;
    }

    public ITrailsStrategy GetTrailsStrategy()
    {
        return trailsStrategy;
    }
}