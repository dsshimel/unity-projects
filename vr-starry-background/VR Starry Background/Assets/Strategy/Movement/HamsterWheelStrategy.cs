using System.Collections.Generic;
using UnityEngine;

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

    private IDictionary<int, CylinderParams> cylinderParamsMap;

    public HamsterWheelStrategy(
        IProvider<ICollection<int>> gameObjectIdProvider,
        float radiusInner,
        float radiusOuter,
        bool randomizeParams,
        float angularVelocityMin,
        float angularVelocityMax) : base(gameObjectIdProvider)
    {
        this.radiusInner = radiusInner;
        this.radiusOuter = radiusOuter;
        this.maxXLength = radiusOuter;
        this.randomizeParams = randomizeParams;
        this.angularVelocityMin = angularVelocityMin;
        this.angularVelocityMax = angularVelocityMax;
        this.angularVelocityAverage = (angularVelocityMin + angularVelocityMax) / 2;

        this.cylinderParamsMap = this.randomizeParams
            ? InitCylinderParamsRandom()
            : InitCylinderParams();
    }

    private IDictionary<int, CylinderParams> InitCylinderParamsRandom()
    {
        var paramsMap = new Dictionary<int, CylinderParams>();
        foreach (int gameObjectId in gameObjectIds)
        {
            paramsMap.Add(gameObjectId, CreateCylinderParams());
        }
        return paramsMap;
    }

    private IDictionary<int, CylinderParams> InitCylinderParams()
    {
        var paramsMap = new Dictionary<int, CylinderParams>();

        var cylinderParamsList = new List<CylinderParams>();
        var numRadii = 3;
        var numAngleZYs = 8;
        var numLengthSpacings = 14;
        var radiusUnit = (radiusOuter - radiusInner) / (numRadii - 1);
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

                    cylinderParamsList.Add(new CylinderParams(radius, angleZY, xLength, angularVelocityAverage));
                }
            }
        }

        var cylinderParamsIndex = 0;
        foreach (int gameObjectId in gameObjectIds)
        {
            paramsMap.Add(gameObjectId, cylinderParamsList[cylinderParamsIndex++]);
        }
        return paramsMap;
    }

    private CylinderParams CreateCylinderParams()
    {
        return new CylinderParams(
            Random.Range(radiusInner, radiusOuter),
            Random.Range(0, 2 * Mathf.PI),
            Random.Range(-maxXLength, maxXLength),
            Random.Range(angularVelocityMin, angularVelocityMax));
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