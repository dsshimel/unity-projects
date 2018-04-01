﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MovementStrategy {

    // Initialize and return the position of the meteor.
    Vector3 InitPosition();

    // Gets the current position of the meteor at time t.
    // timeDelta is in seconds
    Vector3 GetPosition(float timeDelta);

    void SetIntensity(float intensity);
}