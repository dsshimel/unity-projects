using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMovementStrategy : AbstractStrategy, IMovementStrategy
{
    protected bool isMoving;

    public AbstractMovementStrategy(IList<int> gameObjectIds, IManipulator manipulator) : base(gameObjectIds, manipulator)
    {
        
    }

    public abstract Vector3 GetPosition(float timeDelta);

    public abstract Vector3 InitPosition();
}