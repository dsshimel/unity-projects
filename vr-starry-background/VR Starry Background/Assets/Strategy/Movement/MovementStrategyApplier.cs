using UnityEngine;

public class MovementStrategyApplier : AbstractStrategyApplier<Vector3>
{
    public MovementStrategyApplier(IManipulator manipulator) : base(manipulator)
    {
    }

    protected override void ApplyInternal(int gameObjectId, CometProperty property, Vector3 value)
    {
        if (property == CometProperty.POSITION)
        {
            manipulator.SetPosition(gameObjectId, value);
        }
    }
}