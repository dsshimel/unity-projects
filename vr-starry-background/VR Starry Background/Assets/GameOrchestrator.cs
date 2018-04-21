using System.Collections.Generic;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour {

    public GameObject spherePrefab;
    public int numSpheres;
    public int radiusInner;
    public int radiusOuter;

    private ISession session;
    private IDictionary<int, GameObject> comets;
    private IManipulator manipulator;

    private IBundle currentBundle;

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
        var colorStrat = new RandomStaticColorStrategy(manipulator);
        var sizeStrat = new RandomStaticSizeStrategy(manipulator);
        var trailsStrat = new ColorAndSizeMatchGradientStrategy(manipulator, colorStrat, sizeStrat);
        currentBundle = new Bundle(movementStrat, colorStrat, trailsStrat, sizeStrat);

        IPlaylist playlist = new Playlist();
        Interval interval = new Interval(10, 5);
        playlist.AddEntry(currentBundle, interval);
        playlist.AddEntry(currentBundle, interval);
        playlist.AddEntry(currentBundle, interval);
        playlist.AddEntry(currentBundle, interval);
        playlist.AddEntry(currentBundle, interval);

        session = new Session(playlist, /* countdowTime= */ 1.0f);
        session.Start();
    }

    void Update ()
    {
        session.IncrementTime(Time.deltaTime);
	}
}
