﻿using UnityEngine;
using UnityEditor;

public class HamsterWheelStrategy : AbstractMovementStrategy
{
    // Min distance meteor can be from player.
    public float radiusInner;
    // Max distance meteor can be from player.
    public float radiusOuter;
    public float maxXLength;

    private float radius;
    private float angleZY;
    private float angularVelocity;
    private float initTime;
    private float xLength;

    public HamsterWheelStrategy(float radiusInner, float radiusOuter)
    {
        this.radiusInner = radiusInner;
        this.radiusOuter = radiusOuter;
        this.maxXLength = 40;
        intensity = 1.0f;
    }

    override public Vector3 GetPosition(float timeDelta)
    {
        float attenuatedAngularVelocity = angularVelocity * intensity;
        float zy = angleZY - (timeDelta * attenuatedAngularVelocity);

        float newY = radius * Mathf.Sin(zy);
        float newZ = radius * Mathf.Cos(zy);
        return new Vector3(xLength, newY, newZ);
    }

    override public Vector3 InitPosition()
    {
        radius = Random.Range(radiusInner, radiusOuter);
        angleZY = Random.Range(0, 2 * Mathf.PI);
        xLength = Random.Range(-maxXLength, maxXLength);
        angularVelocity = Random.Range(1.0f, 2.0f);

        return GetPosition(0);
    }
}