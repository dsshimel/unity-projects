using UnityEngine;

/** A strategy for changing the color of the materials of the comets. */
public interface IColorStrategy : IStrategy
{
    Color GetColor(int gameObjectId);
}