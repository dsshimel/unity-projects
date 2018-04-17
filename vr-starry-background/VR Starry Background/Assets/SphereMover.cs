using System.Collections.Generic;
using UnityEngine;

public class SphereMover : MonoBehaviour {
    // Min distance meteor can be from player.
    public float radiusInner;
    // Max distance meteor can be from player.
    public float radiusOuter;

    private IMovementStrategy movementStrategy;
    private ParticleSystem trails;
    private bool isMoving;

    void Start ()
    {
        isMoving = false;
        trails = GetComponentInChildren<ParticleSystem>();

        var manipulator = new Manipulator(new Dictionary<int, GameObject>());
        movementStrategy = new SphereTubeStrategy(manipulator, radiusInner, radiusOuter);
        initializeSphere();
	}

	void Update ()
    {
        if (isMoving)
        {
            SetPosition(movementStrategy.GetPosition(Time.deltaTime));
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

    private void initializeSphere()
    {    
        SetPosition(movementStrategy.InitPosition());

        // Might look better if this distribution was logarithmic instead of linear
        float newScale = Random.Range(0.01f, 2);
        gameObject.transform.localScale = new Vector3(newScale, newScale, newScale);

        var shape = trails.shape;
        shape.radius = newScale * 2.0f;
    }

    private void SetPosition(Vector3 position)
    {
        gameObject.transform.position = position;
    }
}
