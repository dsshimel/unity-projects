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
    private IStrategyApplier<Gradient, ITrailsStrategy> trailsStrategyApplier;
    private ISizeStrategy sizeStrategy;
    private IStrategyApplier<Vector3, ISizeStrategy> sizeStrategyApplier;

    public Bundle(
        IMovementStrategy movementStrat,
        IStrategyApplier<Vector3, IMovementStrategy> movementStratApplier,
        IColorStrategy colorStrat,
        IStrategyApplier<Color, IColorStrategy> colorStratApplier,
        ITrailsStrategy trailsStrat,
        IStrategyApplier<Gradient, ITrailsStrategy> trailsStratApplier,
        ISizeStrategy sizeStrat,
        IStrategyApplier<Vector3, ISizeStrategy> sizeStratApplier)
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
        trailsStrategyApplier = trailsStratApplier;
        sizeStrategy = sizeStrat;
        sizeStrategyApplier = sizeStratApplier;
    }

    public void ApplyStrategies(float timeNow, float timeBefore)
    {
        movementStrategyApplier.Apply(movementStrategy, timeNow, timeBefore);
        colorStrategyApplier.Apply(colorStrategy, timeNow, timeBefore);
        trailsStrategyApplier.Apply(trailsStrategy, timeNow, timeBefore);
        sizeStrategyApplier.Apply(sizeStrategy, timeNow, timeBefore);
    }

    public void ApplyStrategiesFade(IBundle bundleFadeIn, float fadeOutPercent, float timeNow, float timeBefore)
    {
        // TODO: Fading would look smoother if I passed the time-before-zero (i.e. negative time) to the fade in strategy
        movementStrategyApplier.ApplyFade(movementStrategy, bundleFadeIn.GetMovementStrategy(), fadeOutPercent, timeNow, timeBefore);
        colorStrategyApplier.ApplyFade(colorStrategy, bundleFadeIn.GetColorStrategy(), fadeOutPercent, timeNow, timeBefore);
        trailsStrategyApplier.Apply(trailsStrategy, timeNow, timeBefore);
        sizeStrategyApplier.Apply(sizeStrategy, timeNow, timeBefore);
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