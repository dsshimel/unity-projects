using UnityEngine;
using System.Collections.Generic;

public class Manipulator : IManipulator
{
    private IDictionary<int, GameObject> gameObjectMap;

    public Manipulator(IDictionary<int, GameObject> gameObjects)
    {
        gameObjectMap = gameObjects;
    }

    public ICollection<int> GetGameObjectIds()
    {
        return gameObjectMap.Keys;
    }

    public void SetLocalScale(int objectId, Vector3 scale)
    {
        GameObject gameObject;
        if (gameObjectMap.TryGetValue(objectId, out gameObject))
        {
            gameObject.transform.localScale = scale;
        }
    }

    public void SetMaterialColor(int objectId, Color color)
    {
        GameObject gameObject;
        if (gameObjectMap.TryGetValue(objectId, out gameObject))
        {
            Renderer renderer = gameObject.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", color);
        }
    }

    public void SetParticleColorOverLifetimeGradient(int objectId, Gradient gradient)
    {
        GameObject gameObject;
        if (gameObjectMap.TryGetValue(objectId, out gameObject))
        {
            ParticleSystem trails = gameObject.GetComponentInChildren<ParticleSystem>();
            var col = trails.colorOverLifetime;
            col.color = gradient;
        }
    }

    public void SetParticleRadius(int objectId, float radius)
    {
        GameObject gameObject;
        if (gameObjectMap.TryGetValue(objectId, out gameObject))
        {
            ParticleSystem trails = gameObject.GetComponentInChildren<ParticleSystem>();
            var shape = trails.shape;
            shape.radius = radius;
        }
    }

    public void SetPosition(int objectId, Vector3 position)
    {
        GameObject gameObject;
        if (gameObjectMap.TryGetValue(objectId, out gameObject))
        {
           gameObject.transform.position = position;
        }
    }
}