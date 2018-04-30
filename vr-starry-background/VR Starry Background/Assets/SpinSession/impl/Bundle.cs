using System;
using System.Collections.Generic;

public class Bundle : IBundle
{
    private IList<IStrategyUntyped> strategies;

    public Bundle(IMovementStrategy movementStrat, IColorStrategy colorStrat, ITrailsStrategy trailsStrat, ISizeStrategy sizeStrat)
    {
        strategies = new List<IStrategyUntyped>();
        strategies.Add(movementStrat);
        strategies.Add(colorStrat);
        strategies.Add(trailsStrat);
        strategies.Add(sizeStrat);
    }

    public void ApplyStrategies(float timeNow, float timeBefore)
    {
        foreach (var strat in strategies)
        {
            strat.ApplyStrategy(timeNow, timeBefore);
        }
    }

    public void SetIntensities(float intensity)
    {
        foreach (var strat in strategies)
        {
            strat.SetIntensity(intensity);
        }
    }
}