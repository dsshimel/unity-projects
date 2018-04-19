using System.Collections.Generic;
using UnityEngine;

/**
 * Changes properties of the game objects (e.g. their position, their color).
 */
public interface IManipulator
{
    ICollection<int> GetGameObjectIds();

    void SetMaterialColor(int objectId, Color color);

    void SetParticleColorOverLifetimeGradient(int objectId, Gradient gradient);

    void SetPosition(int objectId, Vector3 position);

    void SetLocalScale(int objectId, Vector3 scale);
}