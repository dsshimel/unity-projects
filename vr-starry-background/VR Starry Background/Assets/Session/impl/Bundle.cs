using System.Collections.Generic;
using UnityEngine;

public class Bundle : IBundle
{
    private IList<IStrategyUntyped> strategies;
    private IStrategyApplier<Vector3> movementStrategyApplier;
    private IStrategyApplier<Color> colorStrategyApplier;
    private IStrategyApplier<Gradient> trailsStrategyApplier;
    private IStrategyApplier<Vector3> sizeStrategyApplier;
    private IStrategyApplier<float> particleSizeStrategyApplier;

    public Bundle(
        IStrategyApplier<Vector3> movementStrategyApplier,
        IStrategyApplier<Color> colorStrategyApplier,
        IStrategyApplier<Gradient> trailsStrategyApplier,
        IStrategyApplier<Vector3> sizeStrategyApplier,
        IStrategyApplier<float> particleSizeStrategyApplier)
    {
        this.movementStrategyApplier = movementStrategyApplier;
        this.colorStrategyApplier = colorStrategyApplier;
        this.trailsStrategyApplier = trailsStrategyApplier;
        this.sizeStrategyApplier = sizeStrategyApplier;
        this.particleSizeStrategyApplier = particleSizeStrategyApplier;

        strategies = new List<IStrategyUntyped>
        {
            this.movementStrategyApplier.Strategy,
            this.colorStrategyApplier.Strategy,
            this.trailsStrategyApplier.Strategy,
            this.sizeStrategyApplier.Strategy,
            this.particleSizeStrategyApplier.Strategy
        };
    }

    public void ApplyStrategies(float timeNow, float timeDelta)
    {
        movementStrategyApplier.Apply(timeNow, timeDelta);
        colorStrategyApplier.Apply(timeNow, timeDelta);
        trailsStrategyApplier.Apply(timeNow, timeDelta);
        sizeStrategyApplier.Apply(timeNow, timeDelta);
        particleSizeStrategyApplier.Apply(timeNow, timeDelta);
    }

    public void ApplyStrategiesFade(IBundle bundleFadeIn, float fadeOutPercent, float timeNow, float timeDelta)
    {
        movementStrategyApplier.ApplyFade(bundleFadeIn.MovementStrategy, fadeOutPercent, timeNow, timeDelta);
        colorStrategyApplier.ApplyFade(bundleFadeIn.ColorStrategy, fadeOutPercent, timeNow, timeDelta);
        trailsStrategyApplier.ApplyFade(bundleFadeIn.TrailsStrategy, fadeOutPercent, timeNow, timeDelta);
        sizeStrategyApplier.ApplyFade(bundleFadeIn.SizeStrategy, fadeOutPercent, timeNow, timeDelta);
        particleSizeStrategyApplier.ApplyFade(bundleFadeIn.ParticleSizeStrategy, fadeOutPercent, timeNow, timeDelta);
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

    public IStrategy<float> ParticleSizeStrategy
    {
        get
        {
            return particleSizeStrategyApplier.Strategy;
        }
    }
}