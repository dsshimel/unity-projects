using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // So can the camera now arbitrarily change the functionality of player
    // through GameObject's API? This doesn't seem like the best from an
    // encapsulation perspective.
    public GameObject player;

    private Vector3 offsetAtStartOfGame;

	// Use this for initialization
	void Start () {
        // camera's vector from origin minus player's vector from origin
        offsetAtStartOfGame = this.transform.position - player.transform.position;
	}
	
	void LateUpdate () {
        this.transform.position = player.transform.position + offsetAtStartOfGame;
	}
}
