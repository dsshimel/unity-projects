using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MeteorMovementStrategy {

    // Initialize and return the position of the meteor.
    Vector3 InitPosition();

    // Gets the current position of the meteor at time t.
    Vector3 GetPosition();

    void SetIntensity(float intensity);
}
