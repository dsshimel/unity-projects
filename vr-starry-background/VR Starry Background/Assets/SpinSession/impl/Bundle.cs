using System.Collections.Generic;
using UnityEngine;

public class Bundle : IBundle
{
    private IList<IStrategyUntyped> strategies;
    private IMovementStrategy movementStrategy;
    private IStrategyApplier<Vector3> movementStrategyApplier;
    private IColorStrategy colorStrategy;
    private ITrailsStrategy trailsStrategy;
    private ISizeStrategy sizeStrategy;

    public Bundle(
        IMovementStrategy movementStrat,
        IStrategyApplier<Vector3> movementStratApplier,
        IColorStrategy colorStrat,
        ITrailsStrategy trailsStrat,
        ISizeStrategy sizeStrat)
    {
        strategies = new List<IStrategyUntyped>();
        strategies.Add(movementStrat);
        strategies.Add(colorStrat);
        strategies.Add(trailsStrat);
        strategies.Add(sizeStrat);

        movementStrategy = movementStrat;
        movementStrategyApplier = movementStratApplier;
        colorStrategy = colorStrat;
        trailsStrategy = trailsStrat;
        sizeStrategy = sizeStrat;
    }

    public void ApplyStrategies(float timeNow, float timeBefore)
    {
        movementStrategyApplier.Apply(movementStrategy, timeNow, timeBefore);
        //movementStrategy.Apply(timeNow, timeBefore);
        colorStrategy.Apply(timeNow, timeBefore);
        trailsStrategy.Apply(timeNow, timeBefore);
        sizeStrategy.Apply(timeNow, timeBefore);
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