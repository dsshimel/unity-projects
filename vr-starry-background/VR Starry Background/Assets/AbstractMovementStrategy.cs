using UnityEngine;
using UnityEditor;
using System;

public abstract class AbstractMovementStrategy : MovementStrategy
{
    protected float intensity;
    protected bool isMoving;

    public abstract Vector3 GetPosition(float timeDelta);

    public abstract Vector3 InitPosition();

    public void SetIntensity(float intensity)
    {
        this.intensity = intensity;
    }
}