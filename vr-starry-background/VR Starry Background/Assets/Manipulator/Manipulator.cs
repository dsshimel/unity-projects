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
}