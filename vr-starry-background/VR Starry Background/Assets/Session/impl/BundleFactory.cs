using UnityEngine;
using System.Collections.Generic;

public class BundleFactory
{
    private readonly Manipulator manipulator;
    private readonly IProvider<ICollection<int>> gameObjectIdProvider;
    public readonly float radiusInner;
    public readonly float radiusOuter;
    public readonly float angularVelocityMin;
    public readonly float angularVelocityMax;
    public readonly float intensityMin;
    public readonly float intensityMax;
    public readonly float scaleMin;
    public readonly float scaleMax;

    public BundleFactory(
        Manipulator manipulator, 
        float radiusInner, 
        float radiusOuter, 
        float angularVelocityMin, 
        float angularVelocityMax,
        float intensityMin,
        float intensityMax,
        float scaleMin,
        float scaleMax)
    {
        this.manipulator = manipulator;
        this.gameObjectIdProvider = manipulator;
        this.radiusInner = radiusInner;
        this.radiusOuter = radiusOuter;
        this.angularVelocityMin = angularVelocityMin;
        this.angularVelocityMax = angularVelocityMax;
        this.intensityMin = intensityMin;
        this.intensityMax = intensityMax;
        this.scaleMin = scaleMin;
        this.scaleMax = scaleMax;
    }

    public IBundle create()
    {
        // TODO: Looks like I should be passing the strategy to the applier. But then
        // how will I cross fade between two strategies? Could pass in the next
        // strategy as well, though that might couple it too closely to the ordering
        // in the playlist.
        // TODO: One idea is to feed one strategy into the next in a chain to create coherency
        // between the patterns
        IStrategy<Vector3> movementStrat;
        var randomizeMovementStrategyPositionParams = flipCoin();
        var randomizeMovementStrategyVelocities = flipCoin();
        var alternateMovementStrategyDirections = flipCoin();
        if (flipCoin())
        {
            movementStrat = new SphereTubeStrategy(
                gameObjectIdProvider,
                radiusInner,
                radiusOuter,
                randomizeMovementStrategyPositionParams,
                angularVelocityMin,
                angularVelocityMax,
                intensityMin,
                intensityMax,
                randomizeMovementStrategyVelocities,
                alternateMovementStrategyDirections);
        }
        else
        {
            movementStrat = new HamsterWheelStrategy(
                gameObjectIdProvider,
                radiusInner,
                radiusOuter,
                randomizeMovementStrategyPositionParams,
                angularVelocityMin,
                angularVelocityMax,
                intensityMin,
                intensityMax,
                randomizeMovementStrategyVelocities,
                alternateMovementStrategyDirections);
        }
        var movementStrategyApplier = new MovementStrategyApplier(manipulator, movementStrat);

        IStrategy<Color> colorStrat;
        if (flipCoin())
        {
            colorStrat = new RandomStaticColorStrategy(
                gameObjectIdProvider,
                intensityMin,
                intensityMax);
        } else
        {
            var duration = 5.0f;
            colorStrat = new RainbowColorWheelStrategy(
                gameObjectIdProvider, 
                duration,
                intensityMin,
                intensityMax);
        }
        var colorStrategyApplier = new ColorStrategyApplier(
            manipulator,
            colorStrat);

        IStrategy<Vector3> sizeStrat;
        if (flipCoin())
        {
            sizeStrat = new RandomStaticSizeStrategy(
                gameObjectIdProvider,
                intensityMin,
                intensityMax,
                scaleMin,
                scaleMax);
        } else
        {
            sizeStrat = new AverageStaticSizeStrategy(
                gameObjectIdProvider,
                intensityMin,
                intensityMax,
                scaleMin,
                scaleMax);
        }
        var sizeStrategyApplier = new SizeStrategyApplier(manipulator, sizeStrat);

        IStrategy<Gradient> trailsStrat;
        if (flipCoin())
        {
            trailsStrat = new ColorMatchStaticGradientStrategy(
                gameObjectIdProvider,
                colorStrat,
                intensityMin,
                intensityMax);
        } else
        {
            trailsStrat = new RainbowColorMatchStaticGradientStrategy(
                gameObjectIdProvider, 
                colorStrat, 
                intensityMin, 
                intensityMax);
        }
        var trailsStrategyApplier = new TrailsStrategyApplier(manipulator, trailsStrat);

        var particleSizeStrat = new ParticleSizeMatchStaticStrategy(
            gameObjectIdProvider,
            sizeStrat,
            intensityMin,
            intensityMax);
        var particleSizeStrategyApplier = new ParticleRadiusStrategyApplier(manipulator, particleSizeStrat);

        return new Bundle(
                movementStrategyApplier,
                colorStrategyApplier,
                trailsStrategyApplier,
                sizeStrategyApplier,
                particleSizeStrategyApplier);
    }

    private bool flipCoin()
    {
        return Random.value <= 0.5;
    }
}