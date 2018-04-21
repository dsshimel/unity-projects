using UnityEngine;

public abstract class AbstractMovementStrategy : AbstractStrategy, IMovementStrategy
{
    protected bool isMoving;

    public AbstractMovementStrategy(IManipulator manipulator) : base(manipulator)
    {
        
    }

    public abstract Vector3 GetPosition(float timeDelta);

    public abstract Vector3 InitPosition();
}