using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public abstract class CircularMovementStrategy : AbstractStrategy<Vector3>
{
    // Min distance meteor can be from player.
    public readonly float radiusInner;
    // Max distance meteor can be from player.
    public readonly float radiusOuter;
    public readonly float maxXLength;
    public readonly bool randomizePositionParams;
    public readonly float angularVelocityMin;
    public readonly float angularVelocityMax;
    public readonly float angularVelocityAverage;
    public readonly bool randomizeVelocities;
    public readonly bool alternateDirections;

    public CircularMovementStrategy(
        IProvider<ICollection<int>> gameObjectIdProvider,
        float radiusInner,
        float radiusOuter,
        bool randomizePositionParams,
        float angularVelocityMin,
        float angularVelocityMax,
        float intensityMin, 
        float intensityMax,
        bool randomizeVelocities,
        bool alternateDirections) : base(gameObjectIdProvider, intensityMin, intensityMax)
    {
        this.radiusInner = radiusInner;
        this.radiusOuter = radiusOuter;
        this.maxXLength = radiusOuter;
        this.randomizePositionParams = randomizePositionParams;
        this.angularVelocityMin = angularVelocityMin;
        this.angularVelocityMax = angularVelocityMax;
        this.angularVelocityAverage = (angularVelocityMin + angularVelocityMax) / 2;
        this.randomizeVelocities = randomizeVelocities;
        this.alternateDirections = alternateDirections;
    }

    protected float RandomVelocity()
    {
        return Random.Range(angularVelocityMin, angularVelocityMax);
    }
}