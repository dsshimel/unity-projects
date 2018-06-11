using System.Collections.Generic;
using UnityEngine;

public class SphereTubeStrategy : AbstractStrategy<Vector3>
{
    // Min distance meteor can be from player.
    public float radiusInner;
    // Max distance meteor can be from player.
    public float radiusOuter;

    private IDictionary<int, AngleParams> angleParamsMap;

    public SphereTubeStrategy(
        IProvider<ICollection<int>> gameObjectIdProvider,
        float radiusInner,
        float radiusOuter) : base(gameObjectIdProvider)
    {
        this.radiusInner = radiusInner;
        this.radiusOuter = radiusOuter;

        angleParamsMap = new Dictionary<int, AngleParams>();
        foreach (int gameObjectId in gameObjectIds)
        {
            angleParamsMap.Add(gameObjectId, CreateAngleParams());
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

    public override Vector3 ComputeValue(int gameObjectId, float timeNow, float timeDelta)
    {
        var angleParams = angleParamsMap[gameObjectId];

        float attenuatedAngularVelocity = intensity * angleParams.AngularVelocity;
        angleParams.AzimuthAnglePhi += timeDelta * attenuatedAngularVelocity;

        var radius = angleParams.Radius;
        var polarAngleTheta = angleParams.PolarAngleTheta;
        var azimuthAnglePhi = angleParams.AzimuthAnglePhi;

        // Setting the coordinates up like this makes the sphere rotate towards the
        // user like the hamster wheel.
        float newX = radius * Mathf.Cos(polarAngleTheta);
        float newY = radius * Mathf.Sin(polarAngleTheta) * Mathf.Cos(azimuthAnglePhi);
        float newZ = radius * Mathf.Sin(polarAngleTheta) * Mathf.Sin(azimuthAnglePhi);

        return new Vector3(newX, newY, newZ);
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