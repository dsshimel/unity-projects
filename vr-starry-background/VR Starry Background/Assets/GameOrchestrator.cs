﻿using System.Collections.Generic;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour {

    public GameObject spherePrefab;
    public int numSpheres;
    public int radiusInner;
    public int radiusOuter;

    private ISession session;
    private IDictionary<int, GameObject> comets;
    private IManipulator manipulator;

	void Start ()
    {
        comets = new Dictionary<int, GameObject>();
        for (var i = 0; i < numSpheres; i++)
        {
            GameObject comet = Instantiate(spherePrefab);
            comets.Add(comet.GetInstanceID(), comet);
        }

        manipulator = new Manipulator(comets);

        // Generate the playlist of strategies here?
        var movementStrat = new SphereTubeStrategy(manipulator, radiusInner, radiusOuter);
        var movementStratApplier = new MovementStrategyApplier(manipulator);
        var hamsterStrat = new HamsterWheelStrategy(manipulator, radiusInner, radiusOuter);
        var colorStratApplier = new ColorStrategyApplier(manipulator);
        var colorStrat = new RandomStaticColorStrategy(manipulator);
        var sizeStrat = new RandomStaticSizeStrategy(manipulator);
        var trailsStrat = new ColorAndSizeMatchGradientStrategy(manipulator, colorStrat, sizeStrat);

        var bundle = new Bundle(movementStrat, movementStratApplier, colorStrat, colorStratApplier, trailsStrat, sizeStrat);
        var hamsterBundle = new Bundle(hamsterStrat, movementStratApplier, colorStrat, colorStratApplier, trailsStrat, sizeStrat);

        IPlaylist playlist = new Playlist(new List<IBundle>(), new List<Interval>());
        Interval interval = new Interval(3, 1.5f, 1.0f);
        // This should be done by a PlaylistBuilder.
        playlist.AddEntry(bundle, interval);
        playlist.AddEntry(hamsterBundle, interval);
        playlist.AddEntry(bundle, interval);
        playlist.AddEntry(hamsterBundle, interval);
        playlist.AddEntry(bundle, interval);

        session = new Session(playlist, /* countdowTime= */ 1.0f);
        session.Start();
    }

    void Update ()
    {
        session.IncrementTime(Time.deltaTime);
	}
}
