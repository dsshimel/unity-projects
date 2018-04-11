using SpinSession;
using System.Collections.Generic;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour {

    public GameObject spherePrefab;
    public int numSpheres;

    private OldSession session;
    private IDictionary<int, GameObject> spheres;
    private IManipulator manipulator;

	void Start ()
    {
        spheres = new Dictionary<int, GameObject>();

        for (var i = 0; i < numSpheres; i++)
        {
            GameObject sphere = Instantiate(spherePrefab);
            spheres.Add(sphere.GetInstanceID(), sphere);
        }

        manipulator = new Manipulator(spheres);

        // Generate the playlist of strategies here?

        session = gameObject.AddComponent<OldSession>();
        session.CountdownEnd += Session_CountdownEnd;
        session.ActivePeriodStart += Session_ActivePeriodStart;
        session.RestPeriodStart += Session_RestPeriodStart;
        session.Begin();
    }

    private void Session_CountdownEnd(object sender, System.EventArgs e)
    {
        foreach (GameObject sphere in spheres.Values)
        {
            sphere.GetComponent<SphereMover>().StartMoving();
        }
    }

    private void Session_ActivePeriodStart(object sender, System.EventArgs e)
    {
        foreach (GameObject sphere in spheres.Values)
        {
            sphere.GetComponent<SphereMover>().SetIntensity(1.0f);
        }
    }

    private void Session_RestPeriodStart(object sender, System.EventArgs e)
    {
        foreach (GameObject sphere in spheres.Values)
        {
            sphere.GetComponent<SphereMover>().SetIntensity(0.5f);
        }
    }

    void Update ()
    {

	}
}
