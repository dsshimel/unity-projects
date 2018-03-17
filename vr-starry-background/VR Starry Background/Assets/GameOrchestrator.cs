using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour {

    public GameObject spherePrefab;
    public int numSpheres;
    
	void Start () {
        for (var i = 0; i < numSpheres; i++)
        {
            GameObject sphere = Instantiate(spherePrefab);
        }
    }
	
	void Update ()
    {

	}
}
