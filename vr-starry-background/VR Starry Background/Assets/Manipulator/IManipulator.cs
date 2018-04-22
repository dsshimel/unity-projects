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
    void SetParticleRadius(int objectId, float radius);

    void SetPosition(int objectId, Vector3 position);
    void SetPositionXFade(int objectId, CrossfadeValues.Vector3XFade positionXFade);

    void SetLocalScale(int objectId, Vector3 scale);
}