using System.Collections.Generic;
using UnityEngine;

public class SphereTubeStrategy : AbstractStrategy<Vector3>, IMovementStrategy
{
    // Min distance meteor can be from player.
    public float radiusInner;
    // Max distance meteor can be from player.
    public float radiusOuter;

    private IDictionary<int, AngleParams> angleParamsMap;

    public override CometProperty Property
    {
        get
        {
            return CometProperty.POSITION;
        }
    }

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

    public override void Apply(float timeNow, float timeBefore)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            manipulator.SetPosition(gameObjectId, ComputeValue(gameObjectId, timeNow, timeBefore));
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

    public override Vector3 ComputeValue(int gameObjectId, float timeNow, float timeBefore)
    {
        var timeDelta = timeNow - timeBefore;
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

    public override Vector3 CrossFadeValues(int gameObjectId, float timeNow, float timeBefore, IStrategy<Vector3> thatStrategy, float percentThis)
    {
        // TODO: Put this implementation in an AbstractMovementStrategy class?
        var valueThis = ComputeValue(gameObjectId, timeNow, timeBefore);
        var valueThat = thatStrategy.ComputeValue(gameObjectId, timeNow, timeBefore);
        var xfader = new CrossfadeValues.Vector3XFade(valueThis, valueThat, percentThis);
        return xfader.GetXFadeValue();
    }

    public override void ApplyStrategyWithCrossfade(float timeNow, float timeBefore, IStrategy<Vector3> thatStrategy, float percentThis)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            manipulator.SetPosition(gameObjectId, CrossFadeValues(gameObjectId, timeNow, timeBefore, thatStrategy, percentThis));
        }
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