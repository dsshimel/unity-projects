using System.Collections.Generic;
using UnityEngine;

public class SphereTubeStrategy : AbstractStrategy, IMovementStrategy
{
    // Min distance meteor can be from player.
    public float radiusInner;
    // Max distance meteor can be from player.
    public float radiusOuter;

    private IDictionary<int, AngleParams> angleParamsMap;

    public SphereTubeStrategy(IManipulator manipulator, float radiusInner, float radiusOuter) : base(manipulator)
    {
        this.radiusInner = radiusInner;
        this.radiusOuter = radiusOuter;

        angleParamsMap = new Dictionary<int, AngleParams>();
        foreach (int gameObjectId in gameObjectIds)
        {
            angleParamsMap.Add(gameObjectId, CreateAngleParams());
        }
    }

    private Vector3 GetPosition(float timeDelta, AngleParams angleParams)
    {
        float attenuatedAngularVelocity = intensity * angleParams.AngularVelocity;
        angleParams.AzimuthAnglePhi += timeDelta * attenuatedAngularVelocity;

        var radius = angleParams.Radius;
        var polarAngleTheta = angleParams.PolarAngleTheta;
        var azimuthAnglePhi = angleParams.AzimuthAnglePhi;
        float newX = radius * Mathf.Sin(polarAngleTheta) * Mathf.Cos(azimuthAnglePhi);
        float newY = radius * Mathf.Sin(polarAngleTheta) * Mathf.Sin(azimuthAnglePhi);
        float newZ = radius * Mathf.Cos(polarAngleTheta);

        return new Vector3(newX, newY, newZ);
    }

    public override void ApplyStrategy(float timeNow, float timeBefore)
    {
        float delta = timeNow - timeBefore;
        foreach (int gameObjectId in gameObjectIds)
        {
            Vector3 position = GetPosition(delta, angleParamsMap[gameObjectId]);
            manipulator.SetPosition(gameObjectId, position);
        }
    }

    private AngleParams CreateAngleParams()
    {
        return new AngleParams(
            Random.Range(radiusInner, radiusOuter),
            Random.Range(0, 2 * Mathf.PI),
            Random.Range(0, 2 * Mathf.PI),
            Random.Range(1.0f, 2.0f)); ;
    }

    class AngleParams
    {
        public AngleParams(float radius, float polarAngleTheta, float azimuthAnglePhi,
            float angularVelocity)
        {
            Radius = radius;
            PolarAngleTheta = polarAngleTheta;
            AzimuthAnglePhi = azimuthAnglePhi;
            AngularVelocity = angularVelocity;
        }

        public float Radius { get; set; }
        public float PolarAngleTheta { get; set; }
        public float AzimuthAnglePhi { get; set; }
        public float AngularVelocity { get; set; }
    }
}