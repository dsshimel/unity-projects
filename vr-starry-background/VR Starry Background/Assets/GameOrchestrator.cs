using SpinSession;
using System.Collections.Generic;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour {

    public GameObject spherePrefab;
    public int numSpheres;

    private Session session;
    private IList<GameObject> spheres;

	void Start ()
    {
        spheres = new List<GameObject>();

        for (var i = 0; i < numSpheres; i++)
        {
            GameObject sphere = Instantiate(spherePrefab);
            spheres.Add(sphere);
        }

        session = gameObject.AddComponent<Session>();
        session.CountdownEnd += Session_CountdownEnd;
        session.ActivePeriodStart += Session_ActivePeriodStart;
        session.RestPeriodStart += Session_RestPeriodStart;
        session.Begin();
    }

    private void Session_CountdownEnd(object sender, System.EventArgs e)
    {
        foreach (GameObject sphere in spheres)
        {
            sphere.GetComponent<SphereMover>().StartMoving();
        }
    }

    private void Session_ActivePeriodStart(object sender, System.EventArgs e)
    {
        foreach (GameObject sphere in spheres)
        {
            sphere.GetComponent<SphereMover>().SetIntensity(1.0f);
        }
    }

    private void Session_RestPeriodStart(object sender, System.EventArgs e)
    {
        foreach (GameObject sphere in spheres)
        {
            sphere.GetComponent<SphereMover>().SetIntensity(0.5f);
        }
    }

    void Update ()
    {

	}
}
