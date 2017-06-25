using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    // Not using forces (physics), so we can spin the
    // cube here instead of FixedUpdate()
	void Update () {
        // deltaTime is the time in seconds it took to complete the last frame
        // multiplying by it makes the animation smooth and frame rate independent
        // I guess there's an implied "per second" on the V3?
        this.transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}
}
