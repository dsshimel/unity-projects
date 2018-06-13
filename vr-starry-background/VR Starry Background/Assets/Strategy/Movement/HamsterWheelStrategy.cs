using System.Collections.Generic;
using UnityEngine;

// TODO: Factor out a CircularMovementStrategy class since a lot of code
// is shared with SphereTubeStrategy.
public class HamsterWheelStrategy : AbstractStrategy<Vector3>
{
    // Min distance meteor can be from player.
    public readonly float radiusInner;
    // Max distance meteor can be from player.
    public readonly float radiusOuter;
    public readonly float maxXLength;
    public readonly bool randomizeParams;
    public readonly float angularVelocityMin;
    public readonly float angularVelocityMax;
    public readonly float angularVelocityAverage;
    public readonly bool randomizePositionParams;
    public readonly bool alternateDirections;

    private IDictionary<int, CylinderParams> cylinderParamsMap;

    // TODO: Add bool for controlling if half the comets go the opposite direction
    public HamsterWheelStrategy(
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
        this.maxXLength = radiusOuter;
        this.randomizeParams = randomizePositionParams;
        this.angularVelocityMin = angularVelocityMin;
        this.angularVelocityMax = angularVelocityMax;
        this.angularVelocityAverage = (angularVelocityMin + angularVelocityMax) / 2;
        this.randomizePositionParams = randomizeVelocities;
        this.alternateDirections = alternateDirections;

        this.cylinderParamsMap = this.randomizeParams
            ? InitCylinderParamsRandom()
            : InitCylinderParams();
    }

    private IDictionary<int, CylinderParams> InitCylinderParamsRandom()
    {
        var paramsMap = new Dictionary<int, CylinderParams>();
        var direction = 1;
        foreach (int gameObjectId in gameObjectIds)
        {
            paramsMap.Add(gameObjectId, CreateCylinderParams(direction));
            if (alternateDirections)
            {
                direction *= -1;
            }
        }
        return paramsMap;
    }

    private IDictionary<int, CylinderParams> InitCylinderParams()
    {
        var paramsMap = new Dictionary<int, CylinderParams>();
        var gameObjectEnumerator = gameObjectIds.GetEnumerator();
        gameObjectEnumerator.MoveNext();

        var numRadii = 3;
        var numAngleZYs = 8;
        var numLengthSpacings = 14;
        var radiusUnit = (radiusOuter - radiusInner) / (numRadii - 1);
        var direction = 1;
        for (var r = 0; r < numRadii; r++)
        {
            var radius = (r * radiusUnit) + radiusInner;
            for (var j = 0; j < numAngleZYs; j++)
            {
                var angleZYUnit = (2 * Mathf.PI) / numAngleZYs;
                var angleZY = j * angleZYUnit;

                for (var k = 0; k < numLengthSpacings; k++)
                {
                    var xLengthUnit = 2 * maxXLength / (numLengthSpacings - 1);
                    var xLength = (k * xLengthUnit) - maxXLength;

                    var angularVelocity = randomizePositionParams ? RandomVelocity() : angularVelocityAverage;
                    angularVelocity *= direction;
                    var cylinderParams = new CylinderParams(radius, angleZY, xLength, angularVelocity);
                    paramsMap.Add(gameObjectEnumerator.Current, cylinderParams);

                    gameObjectEnumerator.MoveNext();
                    if (alternateDirections)
                    {
                        direction *= -1;
                    }
                }
            }
        }

        return paramsMap;
    }

    private CylinderParams CreateCylinderParams(int direction)
    {
        return new CylinderParams(
            Random.Range(radiusInner, radiusOuter),
            Random.Range(0, 2 * Mathf.PI),
            Random.Range(-maxXLength, maxXLength),
            direction * RandomVelocity());
    }

    private float RandomVelocity()
    {
        return Random.Range(angularVelocityMin, angularVelocityMax);
    }

    public override Vector3 ComputeValue(int gameObjectId, float timeNow, float timeDelta)
    {
        var cylinderParams = cylinderParamsMap[gameObjectId];

        float attenuatedAngularVelocity = cylinderParams.AngularVelocity * intensity;
        cylinderParams.AngleZY -= timeDelta * attenuatedAngularVelocity;

        float newY = cylinderParams.Radius * Mathf.Sin(cylinderParams.AngleZY);
        float newZ = cylinderParams.Radius * Mathf.Cos(cylinderParams.AngleZY);

        return new Vector3(cylinderParams.XLength, newY, newZ);
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