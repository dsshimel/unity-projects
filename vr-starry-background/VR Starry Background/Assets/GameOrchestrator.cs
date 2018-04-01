using SpinSession;
using System.Collections;
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
        session.Begin();
    }

    private void Session_CountdownEnd(object sender, System.EventArgs e)
    {
        foreach (GameObject sphere in spheres)
        {
            sphere.GetComponent<SphereMover>().StartMoving();
        }
    }

    void Update ()
    {

	}
}
