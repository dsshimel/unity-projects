﻿using UnityEngine;
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

    public void SetMaterialColor(int objectId, Color color)
    {
        GameObject gameObject;
        if (gameObjectMap.TryGetValue(objectId, out gameObject))
        {
            Renderer renderer = gameObject.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", color);
        }
    }
}