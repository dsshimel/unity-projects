using System.Collections.Generic;
using UnityEngine;

/**
 * Changes properties of the game objects (e.g. their position, their color).
 */
public interface IManipulator
{
    void SetMaterialColor(int objectId, Color color);
    void SetParticleColorOverLifetimeGradient(int objectId, Gradient gradient);
    void SetParticleRadius(int objectId, float radius);
    void SetPosition(int objectId, Vector3 position);
    void SetLocalScale(int objectId, Vector3 scale);
}