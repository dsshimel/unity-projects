using System.Collections.Generic;
using UnityEngine;

public class Bundle : IBundle
{
    private IList<IStrategyUntyped> strategies;
    private IStrategy<Vector3> movementStrategy;
    private IStrategyApplier<Vector3> movementStrategyApplier;
    private IStrategy<Color> colorStrategy;
    private IStrategyApplier<Color> colorStrategyApplier;
    private IStrategy<Gradient> trailsStrategy;
    private IStrategyApplier<Gradient> trailsStrategyApplier;
    private IStrategy<Vector3> sizeStrategy;
    private IStrategyApplier<Vector3> sizeStrategyApplier;

    public Bundle(
        IStrategy<Vector3> movementStrat,
        IStrategyApplier<Vector3> movementStratApplier,
        IStrategy<Color> colorStrat,
        IStrategyApplier<Color> colorStratApplier,
        IStrategy<Gradient> trailsStrat,
        IStrategyApplier<Gradient> trailsStratApplier,
        IStrategy<Vector3> sizeStrat,
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

    public IStrategy<Color> GetColorStrategy()
    {
        return colorStrategy;
    }

    public IStrategy<Vector3> GetMovementStrategy()
    {
        return movementStrategy;
    }

    public IStrategy<Vector3> GetSizeStrategy()
    {
        return sizeStrategy;
    }

    public IStrategy<Gradient> GetTrailsStrategy()
    {
        return trailsStrategy;
    }
}