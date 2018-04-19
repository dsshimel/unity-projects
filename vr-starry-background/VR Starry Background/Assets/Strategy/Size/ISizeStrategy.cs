using UnityEngine;
using UnityEditor;

public interface ISizeStrategy : IStrategy
{
    Vector3 GetSize(int gameObjectId);
}