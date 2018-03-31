using UnityEngine;

public class SphereTubeStrategy : MeteorMovementStrategy
{
    // Min distance meteor can be from player.
    public float radiusInner;
    // Max distance meteor can be from player.
    public float radiusOuter;

    private float polarAngleTheta;
    private float azimuthAnglePhi;
    private float radius;
    private float angularVelocity;
    private float initTime;

    public SphereTubeStrategy(float radiusInner, float radiusOuter)
    {
        this.radiusInner = radiusInner;
        this.radiusOuter = radiusOuter;
    }

    public Vector3 GetPosition()
    {
        return ComputePosition(Time.time - initTime);
    }

    public Vector3 InitPosition()
    {
        radius = Random.Range(radiusInner, radiusOuter);
        polarAngleTheta = Random.Range(0, 2 * Mathf.PI);
        azimuthAnglePhi = Random.Range(0, 2 * Mathf.PI);
        angularVelocity = Random.Range(1.0f, 2.0f);

        initTime = Time.time;
        return ComputePosition(0);
    }

    private Vector3 ComputePosition(float timeDelta)
    {
        float azimuth = azimuthAnglePhi + (timeDelta * angularVelocity);

        float newX = radius * Mathf.Sin(polarAngleTheta) * Mathf.Cos(azimuth);
        float newY = radius * Mathf.Sin(polarAngleTheta) * Mathf.Sin(azimuth);
        float newZ = radius * Mathf.Cos(polarAngleTheta);
        return new Vector3(newX, newY, newZ);
    }
}