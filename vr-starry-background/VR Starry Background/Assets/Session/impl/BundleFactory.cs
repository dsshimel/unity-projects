using UnityEngine;
using UnityEditor;

public class BundleFactory
{
    private readonly IManipulator manipulator;
    public int radiusInner = 30;
    public int radiusOuter = 100;

    public BundleFactory(IManipulator manipulator)
    {
        this.manipulator = manipulator;
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
            movementStrat = new SphereTubeStrategy(manipulator, radiusInner, radiusOuter);
        } else
        {
            movementStrat = new HamsterWheelStrategy(manipulator, radiusInner, radiusOuter);
        }
        var movementStratApplier = new MovementStrategyApplier(manipulator);

        var colorStrat = new RandomStaticColorStrategy(manipulator);
        var colorStratApplier = new ColorStrategyApplier(manipulator);

        var sizeStrat = new RandomStaticSizeStrategy(manipulator);
        var sizeStratApplier = new SizeStrategyApplier(manipulator);

        var trailsStrat = new ColorMatchStaticGradientStrategy(manipulator, colorStrat);
        var trailsStratApplier = new TrailsStrategyApplier(manipulator);

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