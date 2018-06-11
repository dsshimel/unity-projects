using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class BundleFactory
{
    private readonly Manipulator manipulator;
    private readonly IProvider<ICollection<int>> gameObjectIdProvider;
    public int radiusInner = 30;
    public int radiusOuter = 100;

    public BundleFactory(Manipulator manipulator)
    {
        this.manipulator = manipulator;
        this.gameObjectIdProvider = manipulator;
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
        if (flipCoin())
        {
            movementStrat = new SphereTubeStrategy(gameObjectIdProvider, radiusInner, radiusOuter);
        } else
        {
            movementStrat = new HamsterWheelStrategy(gameObjectIdProvider, radiusInner, radiusOuter);
        }
        var movementStratApplier = new MovementStrategyApplier(manipulator, movementStrat);

        var colorStrat = new RandomStaticColorStrategy(gameObjectIdProvider);
        var colorStratApplier = new ColorStrategyApplier(manipulator, colorStrat);

        var sizeStrat = new RandomStaticSizeStrategy(gameObjectIdProvider);
        var sizeStratApplier = new SizeStrategyApplier(manipulator, sizeStrat);

        var trailsStrat = new ColorMatchStaticGradientStrategy(gameObjectIdProvider, colorStrat);
        var trailsStratApplier = new TrailsStrategyApplier(manipulator, trailsStrat);

        return new Bundle(
                movementStrat,
                movementStratApplier,
                colorStrat,
                colorStratApplier,
                trailsStrat,
                trailsStratApplier,
                sizeStrat,
                sizeStratApplier);
    }

    private bool flipCoin()
    {
        return Random.value <= 0.5;
    }
}