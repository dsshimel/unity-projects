using UnityEngine;

public class SphereTubeStrategy : AbstractMovementStrategy
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
    private float intensity;

    public SphereTubeStrategy(float radiusInner, float radiusOuter)
    {
        this.radiusInner = radiusInner;
        this.radiusOuter = radiusOuter;
        intensity = 1;
    }

    override public Vector3 GetPosition(float timeDelta)
    {
        float azimuth = azimuthAnglePhi + (timeDelta * angularVelocity);

        float newX = radius * Mathf.Sin(polarAngleTheta) * Mathf.Cos(azimuth);
        float newY = radius * Mathf.Sin(polarAngleTheta) * Mathf.Sin(azimuth);
        float newZ = radius * Mathf.Cos(polarAngleTheta);
        return new Vector3(newX, newY, newZ);
    }

    override public Vector3 InitPosition()
    {
        radius = Random.Range(radiusInner, radiusOuter);
        polarAngleTheta = Random.Range(0, 2 * Mathf.PI);
        azimuthAnglePhi = Random.Range(0, 2 * Mathf.PI);
        angularVelocity = Random.Range(1.0f, 2.0f);

        return GetPosition(0);
    }
}