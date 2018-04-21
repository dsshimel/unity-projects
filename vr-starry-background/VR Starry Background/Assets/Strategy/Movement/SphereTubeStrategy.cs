using System.Collections.Generic;
using UnityEngine;

public class SphereTubeStrategy : AbstractMovementStrategy
{
    // Min distance meteor can be from player.
    public float radiusInner;
    // Max distance meteor can be from player.
    public float radiusOuter;

    private IDictionary<int, AngleParams> initPositions;
    private float polarAngleTheta;
    private float azimuthAnglePhi;
    private float radius;
    private float angularVelocity;
    private float initTime;

    public SphereTubeStrategy(IManipulator manipulator, float radiusInner, float radiusOuter) : base(manipulator)
    {
        this.radiusInner = radiusInner;
        this.radiusOuter = radiusOuter;
        initPositions = new Dictionary<int, AngleParams>();
        intensity = 0.0f;
    }

    override public Vector3 GetPosition(float timeDelta)
    {
        float attenuatedAngularVelocity = intensity * angularVelocity;
        azimuthAnglePhi += timeDelta * attenuatedAngularVelocity;

        float newX = radius * Mathf.Sin(polarAngleTheta) * Mathf.Cos(azimuthAnglePhi);
        float newY = radius * Mathf.Sin(polarAngleTheta) * Mathf.Sin(azimuthAnglePhi);
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

    public override void ApplyStrategy(float timeNow, float timeBefore)
    {
        float delta = timeNow - timeBefore;
    }

    class AngleParams
    {
        public AngleParams(float radius, float polarAngleTheta, float azimuthAnglePhi)
        {
            Radius = radius;
            PolarAngleTheta = polarAngleTheta;
            AzimuthAnglePhi = azimuthAnglePhi;
        }

        public float Radius { get; set; }
        public float PolarAngleTheta { get; set; }
        public float AzimuthAnglePhi { get; set; }
    }
}