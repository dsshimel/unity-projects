using System.Collections.Generic;
using UnityEngine;

public class Bundle : IBundle
{
    private IList<IStrategyUntyped> strategies;
    private IStrategy<Vector3> movementStrategy;
    private IStrategyApplier<Vector3> movementStrategyApplier;
    private IColorStrategy colorStrategy;
    private IStrategyApplier<Color> colorStrategyApplier;
    private ITrailsStrategy trailsStrategy;
    private IStrategyApplier<Gradient> trailsStrategyApplier;
    private ISizeStrategy sizeStrategy;
    private IStrategyApplier<Vector3> sizeStrategyApplier;

    public Bundle(
        IStrategy<Vector3> movementStrat,
        IStrategyApplier<Vector3> movementStratApplier,
        IColorStrategy colorStrat,
        IStrategyApplier<Color> colorStratApplier,
        ITrailsStrategy trailsStrat,
        IStrategyApplier<Gradient> trailsStratApplier,
        ISizeStrategy sizeStrat,
        IStrategyApplier<Vector3> sizeStratApplier)
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
        trailsStrategyApplier.ApplyFade(trailsStrategy, bundleFadeIn.GetTrailsStrategy(), fadeOutPercent, timeNow, timeBefore);
        sizeStrategyApplier.ApplyFade(sizeStrategy, bundleFadeIn.GetSizeStrategy(), fadeOutPercent, timeNow, timeBefore);
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

    public IStrategy<Vector3> GetMovementStrategy()
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