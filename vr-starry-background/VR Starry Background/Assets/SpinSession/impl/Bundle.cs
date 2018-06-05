using System.Collections.Generic;
using UnityEngine;

public class Bundle : IBundle
{
    private IList<IStrategyUntyped> strategies;
    private IMovementStrategy movementStrategy;
    private IStrategyApplier<Vector3, IMovementStrategy> movementStrategyApplier;
    private IColorStrategy colorStrategy;
    private IStrategyApplier<Color, IColorStrategy> colorStrategyApplier;
    private ITrailsStrategy trailsStrategy;
    private ISizeStrategy sizeStrategy;

    public Bundle(
        IMovementStrategy movementStrat,
        IStrategyApplier<Vector3, IMovementStrategy> movementStratApplier,
        IColorStrategy colorStrat,
        IStrategyApplier<Color, IColorStrategy> colorStratApplier,
        ITrailsStrategy trailsStrat,
        ISizeStrategy sizeStrat)
    {
        strategies = new List<IStrategyUntyped>
        {
            movementStrat,
            colorStrat,
            trailsStrat,
            sizeStrat
        };

        movementStrategy = movementStrat;
        movementStrategyApplier = movementStratApplier;
        colorStrategy = colorStrat;
        colorStrategyApplier = colorStratApplier;
        trailsStrategy = trailsStrat;
        sizeStrategy = sizeStrat;
    }

    public void ApplyStrategies(float timeNow, float timeBefore)
    {
        movementStrategyApplier.Apply(movementStrategy, timeNow, timeBefore);
        //movementStrategy.Apply(timeNow, timeBefore);
        colorStrategyApplier.Apply(colorStrategy, timeNow, timeBefore);
        //colorStrategy.Apply(timeNow, timeBefore);
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