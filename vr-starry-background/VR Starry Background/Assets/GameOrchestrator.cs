using System.Collections.Generic;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour {

    public GameObject spherePrefab;
    public int numSpheres;
    public int radiusInner;
    public int radiusOuter;

    private ISession session;
    private IManipulator manipulator;

	void Start ()
    {
        var comets = new Dictionary<int, GameObject>();
        for (var i = 0; i < numSpheres; i++)
        {
            GameObject comet = Instantiate(spherePrefab);
            comets.Add(comet.GetInstanceID(), comet);
        }

        manipulator = new Manipulator(comets);

        // TODO: Generate the playlist of strategies here?
        // TODO: Looks like I should be passing the strategy to the applier. But then
        // how will I cross fade between two strategies? Could pass in the next
        // strategy as well, though that might couple it too closely to the ordering
        // in the playlist.
        // TODO: I am passing the manipulator to the strategies because it is the authoritative source
        // of game object IDs. Maybe extract a ProvideGameObjectIds interface?
        var movementStrat = new SphereTubeStrategy(manipulator, radiusInner, radiusOuter);
        var movementStrat2 = new SphereTubeStrategy(manipulator, radiusInner, radiusOuter);
        var hamsterMovementStrat = new HamsterWheelStrategy(manipulator, radiusInner, radiusOuter);
        var hamsterMovementStrat2 = new HamsterWheelStrategy(manipulator, radiusInner, radiusOuter);
        var movementStratApplier = new MovementStrategyApplier(manipulator);

        var colorStrat = new RandomStaticColorStrategy(manipulator);
        var colorStrat2 = new RandomStaticColorStrategy(manipulator);
        var hamsterColorStrat = new RandomStaticColorStrategy(manipulator);
        var hamsterColorStrat2 = new RandomStaticColorStrategy(manipulator);
        var colorStratApplier = new ColorStrategyApplier(manipulator);

        var sizeStrat = new RandomStaticSizeStrategy(manipulator);
        var sizeStrat2 = new RandomStaticSizeStrategy(manipulator);
        var hamsterSizeStrat = new RandomStaticSizeStrategy(manipulator);
        var hamsterSizeStrat2 = new RandomStaticSizeStrategy(manipulator);
        var sizeStratApplier = new SizeStrategyApplier(manipulator);

        var trailsStrat = new ColorMatchStaticGradientStrategy(manipulator, colorStrat);
        var trailsStrat2 = new ColorMatchStaticGradientStrategy(manipulator, colorStrat);
        var hamsterTrailsStrat = new ColorMatchStaticGradientStrategy(manipulator, hamsterColorStrat);
        var hamsterTrailsStrat2 = new ColorMatchStaticGradientStrategy(manipulator, hamsterColorStrat);
        var trailsStratApplier = new TrailsStrategyApplier(manipulator);

        var bundle =
            new Bundle(
                movementStrat, movementStratApplier, colorStrat, colorStratApplier, trailsStrat, trailsStratApplier, sizeStrat, sizeStratApplier);
        var hamsterBundle =
            new Bundle(
                hamsterMovementStrat, movementStratApplier, hamsterColorStrat, colorStratApplier, hamsterTrailsStrat, trailsStratApplier, hamsterSizeStrat, sizeStratApplier);
        var bundle2 =
            new Bundle(
                movementStrat2, movementStratApplier, colorStrat2, colorStratApplier, trailsStrat2, trailsStratApplier, sizeStrat2, sizeStratApplier);
        var hamsterBundle2 =
            new Bundle(
                hamsterMovementStrat2, movementStratApplier, hamsterColorStrat2, colorStratApplier, hamsterTrailsStrat2, trailsStratApplier, hamsterSizeStrat2, sizeStratApplier);

        IPlaylist playlist = new Playlist(new List<IBundle>(), new List<Interval>());
        Interval interval = new Interval(5, 2.5f, 5.0f);
        // TODO: This should be done by a PlaylistBuilder.
        playlist.AddEntry(bundle, interval);
        playlist.AddEntry(hamsterBundle, interval);
        playlist.AddEntry(bundle2, interval);
        playlist.AddEntry(hamsterBundle2, interval);

        session = new Session(playlist, /* countdowTime= */ 1.0f);
        session.Start();
    }

    void Update ()
    {
        session.IncrementTime(Time.deltaTime);
	}
}
