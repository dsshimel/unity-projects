﻿using System.Collections.Generic;
using UnityEngine;

public class HamsterWheelStrategy : AbstractStrategy<Vector3>, IMovementStrategy
{
    // Min distance meteor can be from player.
    public float radiusInner;
    // Max distance meteor can be from player.
    public float radiusOuter;
    public float maxXLength;

    private IDictionary<int, CylinderParams> cylinderParamsMap;

    public HamsterWheelStrategy(IManipulator manipulator, float radiusInner, float radiusOuter) : base(manipulator)
    {
        this.radiusInner = radiusInner;
        this.radiusOuter = radiusOuter;
        maxXLength = 100;
        intensity = 0.0f;

        cylinderParamsMap = new Dictionary<int, CylinderParams>();
        foreach (int gameObjectId in gameObjectIds)
        {
            cylinderParamsMap.Add(gameObjectId, CreateCylinderParams());
        }
    }

    public override void ApplyStrategy(float timeNow, float timeBefore)
    {
        foreach (int gameObjectId in gameObjectIds)
        {
            manipulator.SetPosition(gameObjectId, ComputeStrategyValue(gameObjectId, timeNow, timeBefore));
        }
    }

    private CylinderParams CreateCylinderParams()
    {
        return new CylinderParams(
            Random.Range(radiusInner, radiusOuter),
            Random.Range(0, 2 * Mathf.PI),
            Random.Range(-maxXLength, maxXLength),
            Random.Range(1.0f, 2.0f));
    }

    public override Vector3 ComputeStrategyValue(int gameObjectId, float timeNow, float timeBefore)
    {
        float timeDelta = timeNow - timeBefore;
        var cylinderParams = cylinderParamsMap[gameObjectId];

        float attenuatedAngularVelocity = cylinderParams.AngularVelocity * intensity;
        cylinderParams.AngleZY -= timeDelta * attenuatedAngularVelocity;

        float newY = cylinderParams.Radius * Mathf.Sin(cylinderParams.AngleZY);
        float newZ = cylinderParams.Radius * Mathf.Cos(cylinderParams.AngleZY);

        return new Vector3(cylinderParams.XLength, newY, newZ); ;
    }

    public override Vector3 CrossFadeStrategyValues(int gameObjectId, float timeNow, float timeBefore, IStrategy<Vector3> thatStrategy, float percentThis)
    {
        var valueThis = ComputeStrategyValue(gameObjectId, timeNow, timeBefore);
        var valueThat = thatStrategy.ComputeStrategyValue(gameObjectId, timeNow, timeBefore);
        var xfader = new CrossfadeValues.Vector3XFade(valueThis, valueThat, percentThis);
        return xfader.GetXFadeValue();
    }

    class CylinderParams
    {
        public CylinderParams(float radius, float angleZY, float xLength, float angularVelocity)
        {
            Radius = radius;
            AngleZY = angleZY;
            XLength = xLength;
            AngularVelocity = angularVelocity;
        }
        
        public float Radius { get; set; }
        public float AngleZY { get; set; }
        public float XLength { get; set; }
        public float AngularVelocity { get; set; }
    }
}