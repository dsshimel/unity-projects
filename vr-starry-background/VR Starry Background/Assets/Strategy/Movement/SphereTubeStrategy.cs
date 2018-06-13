﻿using System.Collections.Generic;
using UnityEngine;

public class SphereTubeStrategy : AbstractStrategy<Vector3>
{
    // Min distance meteor can be from player.
    public readonly float radiusInner;
    // Max distance meteor can be from player.
    public readonly float radiusOuter;
    public readonly bool randomizePositionParams;
    public readonly float angularVelocityMin;
    public readonly float angularVelocityMax;
    public readonly float angularVelocityAverage;
    public readonly bool randomizeVelocities;
    public readonly bool alternateDirections;

    private IDictionary<int, AngleParams> angleParamsMap;

    public SphereTubeStrategy(
        IProvider<ICollection<int>> gameObjectIdProvider,
        float radiusInner,
        float radiusOuter,
        bool randomizePositionParams,
        float angularVelocityMin,
        float angularVelocityMax,
        bool randomizeVelocities,
        bool alternateDirections) : base(gameObjectIdProvider)
    {
        this.radiusInner = radiusInner;
        this.radiusOuter = radiusOuter;
        this.randomizePositionParams = randomizePositionParams;
        this.angularVelocityMin = angularVelocityMin;
        this.angularVelocityMax = angularVelocityMax;
        this.angularVelocityAverage = (angularVelocityMin + angularVelocityMax) / 2;
        this.randomizeVelocities = randomizeVelocities;
        this.alternateDirections = alternateDirections;

        this.angleParamsMap = this.randomizePositionParams
            ? InitAngleParamsRandom()
            : InitAngleParams();
    }

    private IDictionary<int, AngleParams> InitAngleParamsRandom()
    {
        var paramsMap = new Dictionary<int, AngleParams>();
        var direction = 1;
        foreach (var gameObjectId in gameObjectIds)
        {
            paramsMap.Add(gameObjectId, CreateAngleParams(direction));
            if (alternateDirections)
            {
                direction *= -1;
            }
        }
        return paramsMap;
    }

    private IDictionary<int, AngleParams> InitAngleParams()
    {
        var paramsMap = new Dictionary<int, AngleParams>();
        var gameObjectEnumerator = gameObjectIds.GetEnumerator();
        gameObjectEnumerator.MoveNext();

        var numRadii = 3;
        var numPolarAngles = 14;
        var numAzimuthAngles = 8;
        var radiusUnit = (radiusOuter - radiusInner) / (numRadii - 1);
        var direction = 1;
        for (var r = 0; r < numRadii; r++)
        {
            var radius = (r * radiusUnit) + radiusInner;
            for (var j = 0; j < numPolarAngles; j++)
            {
                var polarAngleUnit = Mathf.PI / numPolarAngles;
                var polarAngleTheta = j * polarAngleUnit + (polarAngleUnit / 2);

                for (var k = 0; k < numAzimuthAngles; k++)
                {
                    var azimuthAngleUnit = 2 * Mathf.PI / numAzimuthAngles;
                    var azimuthAnglePhi = k * azimuthAngleUnit + (azimuthAngleUnit / 2);

                    var angularVelocity = randomizeVelocities ? RandomVelocity() : angularVelocityAverage;
                    angularVelocity *= direction;
                    var angleParams = new AngleParams(radius, polarAngleTheta, azimuthAnglePhi, angularVelocity);
                    paramsMap.Add(gameObjectEnumerator.Current, angleParams);

                    if (alternateDirections)
                    {
                        direction *= -1;
                    }
                    gameObjectEnumerator.MoveNext();
                }
            }
        }

        return paramsMap;
    }

    private AngleParams CreateAngleParams(int direction)
    {
        return new AngleParams(
            Random.Range(radiusInner, radiusOuter),
            Random.Range(0, 2 * Mathf.PI),
            Random.Range(0, 2 * Mathf.PI),
            direction * RandomVelocity());
    }

    private float RandomVelocity()
    {
        return Random.Range(angularVelocityMin, angularVelocityMax);
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
        public AngleParams(
            float radius,
            float polarAngleTheta,
            float azimuthAnglePhi,
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