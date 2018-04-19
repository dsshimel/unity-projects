using System.Collections.Generic;
using UnityEngine;

public class SphereMover : MonoBehaviour {
    // Min distance meteor can be from player.
    public float radiusInner;
    // Max distance meteor can be from player.
    public float radiusOuter;

    private IManipulator manipulator;
    private IMovementStrategy movementStrategy;
    private bool isMoving;

    void Start ()
    {
        isMoving = false;

        var gameIds = new Dictionary<int, GameObject>();
        gameIds.Add(gameObject.GetInstanceID(), gameObject);
        manipulator = new Manipulator(gameIds);
        movementStrategy = new SphereTubeStrategy(manipulator, radiusInner, radiusOuter);
        InitializeSphere();
	}

	void Update ()
    {
        if (isMoving)
        {
            manipulator.SetPosition(gameObject.GetInstanceID(), movementStrategy.GetPosition(Time.deltaTime));
        }
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    public void SetIntensity(float intensity)
    {
        movementStrategy.SetIntensity(intensity);
    }

    private void InitializeSphere()
    {
        manipulator.SetPosition(gameObject.GetInstanceID(), movementStrategy.InitPosition());
    }
}
