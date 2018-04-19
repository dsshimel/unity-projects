using SpinSession;
using System.Collections.Generic;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour {

    public GameObject spherePrefab;
    public int numSpheres;
    public int radiusInner;
    public int radiusOuter;

    private OldSession oldSession;
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
        currentBundle.ApplyStrategies();

        IPlaylist playlist = new Playlist();
        playlist.AddBundle(currentBundle);
        session = new Session(playlist, /* countdowTime= */ 1.0f);

        // TODO: Get rid of this code
        oldSession = gameObject.AddComponent<OldSession>();
        oldSession.CountdownEnd += Session_CountdownEnd;
        oldSession.ActivePeriodStart += Session_ActivePeriodStart;
        oldSession.RestPeriodStart += Session_RestPeriodStart;
        oldSession.Begin();
    }

    private void Session_CountdownEnd(object sender, System.EventArgs e)
    {
        foreach (GameObject sphere in comets.Values)
        {
            sphere.GetComponent<SphereMover>().StartMoving();
        }
    }

    private void Session_ActivePeriodStart(object sender, System.EventArgs e)
    {
        foreach (GameObject sphere in comets.Values)
        {
            sphere.GetComponent<SphereMover>().SetIntensity(1.0f);
        }
    }

    private void Session_RestPeriodStart(object sender, System.EventArgs e)
    {
        foreach (GameObject sphere in comets.Values)
        {
            sphere.GetComponent<SphereMover>().SetIntensity(0.5f);
        }
    }

    void Update ()
    {
        currentBundle.IncrementTime(Time.deltaTime);
	}
}
