using System.Collections.Generic;

/**
 * Changes properties of the game objects (e.g. their position, their color).
 */
public interface IManipulator
{
    ICollection<int> GetGameObjectIds();
}