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

    public BundleFactory(
        Manipulator manipulator, 
        float radiusInner, 
        float radiusOuter, 
        float angularVelocityMin, 
        float angularVelocityMax)
    {
        this.manipulator = manipulator;
        this.gameObjectIdProvider = manipulator;
        this.radiusInner = radiusInner;
        this.radiusOuter = radiusOuter;
        this.angularVelocityMin = angularVelocityMin;
        this.angularVelocityMax = angularVelocityMax;
    }

    public IBundle create()
    {
        // TODO: Looks like I should be passing the strategy to the applier. But then
        // how will I cross fade between two strategies? Could pass in the next
        // strategy as well, though that might couple it too closely to the ordering
        // in the playlist.
        // TODO: I am passing the manipulator to the strategies because it is the authoritative source
        // of game object IDs. Maybe extract a ProvideGameObjectIds interface?
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
                randomizeMovementStrategyVelocities,
                alternateMovementStrategyDirections);
        }
        var movementStrategyApplier = new MovementStrategyApplier(manipulator, movementStrat);

        IStrategy<Color> colorStrat;
        if (flipCoin())
        {
            colorStrat = new RandomStaticColorStrategy(gameObjectIdProvider);
        } else
        {
            var duration = 5.0f;
            colorStrat = new RainbowColorWheel(gameObjectIdProvider, duration);
        }
        var colorStrategyApplier = new ColorStrategyApplier(manipulator, colorStrat);

        var sizeStrat = new RandomStaticSizeStrategy(gameObjectIdProvider);
        var sizeStrategyApplier = new SizeStrategyApplier(manipulator, sizeStrat);

        var trailsStrat = new ColorMatchStaticGradientStrategy(gameObjectIdProvider, colorStrat);
        var trailsStrategyApplier = new TrailsStrategyApplier(manipulator, trailsStrat);

        return new Bundle(
                movementStrategyApplier,
                colorStrategyApplier,
                trailsStrategyApplier,
                sizeStrategyApplier);
    }

    private bool flipCoin()
    {
        return Random.value <= 0.5;
    }
}