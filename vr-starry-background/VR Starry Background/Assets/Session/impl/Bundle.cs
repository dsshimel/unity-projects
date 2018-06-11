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

    public void ApplyStrategies(float timeNow, float timeDelta)
    {
        movementStrategyApplier.Apply(timeNow, timeDelta);
        colorStrategyApplier.Apply(timeNow, timeDelta);
        trailsStrategyApplier.Apply(timeNow, timeDelta);
        sizeStrategyApplier.Apply(timeNow, timeDelta);
    }

    public void ApplyStrategiesFade(IBundle bundleFadeIn, float fadeOutPercent, float timeNow, float timeDelta)
    {
        movementStrategyApplier.ApplyFade(bundleFadeIn.MovementStrategy, fadeOutPercent, timeNow, timeDelta);
        colorStrategyApplier.ApplyFade(bundleFadeIn.ColorStrategy, fadeOutPercent, timeNow, timeDelta);
        trailsStrategyApplier.ApplyFade(bundleFadeIn.TrailsStrategy, fadeOutPercent, timeNow, timeDelta);
        sizeStrategyApplier.ApplyFade(bundleFadeIn.SizeStrategy, fadeOutPercent, timeNow, timeDelta);
    }

    public void SetIntensities(float intensity)
    {
        foreach (var strat in strategies)
        {
            strat.SetIntensity(intensity);
        }
    }

    public IStrategy<Color> ColorStrategy
    {
        get
        {
            return colorStrategy;
        }
    }

    public IStrategy<Vector3> MovementStrategy
    {
        get
        {
            return movementStrategy;
        }
    }

    public IStrategy<Vector3> SizeStrategy
    {
        get
        {
            return sizeStrategy;
        }
    }

    public IStrategy<Gradient> TrailsStrategy
    {
        get
        {
            return trailsStrategy;
        }
    }
}