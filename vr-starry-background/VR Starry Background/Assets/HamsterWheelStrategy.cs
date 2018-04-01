using UnityEngine;
using UnityEditor;

public class HamsterWheelStrategy : MeteorMovementStrategy
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
    private float intensity;

    public HamsterWheelStrategy(float radiusInner, float radiusOuter)
    {
        this.radiusInner = radiusInner;
        this.radiusOuter = radiusOuter;
        this.maxXLength = 40;
    }

    public Vector3 GetPosition()
    {
        return ComputePosition(Time.time - initTime);
    }

    public Vector3 InitPosition()
    {
        radius = Random.Range(radiusInner, radiusOuter);
        angleZY = Random.Range(0, 2 * Mathf.PI);
        xLength = Random.Range(-maxXLength, maxXLength);
        angularVelocity = Random.Range(1.0f, 2.0f);

        initTime = Time.time;
        return ComputePosition(0);
    }

    public void SetIntensity(float intensity)
    {
        this.intensity = intensity;
    }

    // timeDelta is in seconds
    private Vector3 ComputePosition(float timeDelta)
    {
        float zy = angleZY - (timeDelta * angularVelocity);

        float newY = radius * Mathf.Sin(zy);
        float newZ = radius * Mathf.Cos(zy);
        return new Vector3(xLength, newY, newZ);
    }
}