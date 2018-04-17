using SpinSession;
using System.Collections.Generic;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour {

    public GameObject spherePrefab;
    public int numSpheres;

    private OldSession session;
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
        var movementStrat = new SphereTubeStrategy(manipulator, 30, 100);
        var colorStrat = new RandomStaticColorStrategy(manipulator);
        currentBundle = new Bundle(movementStrat, colorStrat);
        currentBundle.ApplyStrategies();

        session = gameObject.AddComponent<OldSession>();
        session.CountdownEnd += Session_CountdownEnd;
        session.ActivePeriodStart += Session_ActivePeriodStart;
        session.RestPeriodStart += Session_RestPeriodStart;
        session.Begin();
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
