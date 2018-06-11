using System.Collections.Generic;
using UnityEngine;

public class Bundle : IBundle
{
    private IList<IStrategyUntyped> strategies;
    private IStrategyApplier<Vector3> movementStrategyApplier;
    private IStrategyApplier<Color> colorStrategyApplier;
    private IStrategyApplier<Gradient> trailsStrategyApplier;
    private IStrategyApplier<Vector3> sizeStrategyApplier;

    public Bundle(
        IStrategyApplier<Vector3> movementStratApplier,
        IStrategyApplier<Color> colorStratApplier,
        IStrategyApplier<Gradient> trailsStratApplier,
        IStrategyApplier<Vector3> sizeStratApplier)
    {
        movementStrategyApplier = movementStratApplier;
        colorStrategyApplier = colorStratApplier;
        trailsStrategyApplier = trailsStratApplier;
        sizeStrategyApplier = sizeStratApplier;

        strategies = new List<IStrategyUntyped>
        {
            movementStrategyApplier.Strategy,
            colorStrategyApplier.Strategy,
            trailsStrategyApplier.Strategy,
            sizeStrategyApplier.Strategy
        };
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
            return colorStrategyApplier.Strategy;
        }
    }

    public IStrategy<Vector3> MovementStrategy
    {
        get
        {
            return movementStrategyApplier.Strategy;
        }
    }

    public IStrategy<Vector3> SizeStrategy
    {
        get
        {
            return sizeStrategyApplier.Strategy;
        }
    }

    public IStrategy<Gradient> TrailsStrategy
    {
        get
        {
            return trailsStrategyApplier.Strategy;
        }
    }
}