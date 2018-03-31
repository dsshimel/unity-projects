using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMover : MonoBehaviour {
    // Min distance meteor can be from player.
    public float radiusInner;
    // Max distance meteor can be from player.
    public float radiusOuter;

    private MeteorMovementStrategy movementStrategy;

	// Use this for initialization
	void Start ()
    {
        movementStrategy = new SphereTubeStrategy(radiusInner, radiusOuter);
        //movementStrategy = new HamsterWheelStrategy(radiusInner, radiusOuter);
        initializeSphere();
	}
	
	// Update is called once per frame
	void Update ()
    {
        SetPosition(movementStrategy.GetPosition());
    }

    private void initializeSphere()
    {
        TrailRenderer trailRenderer = gameObject.GetComponent<TrailRenderer>();
		trailRenderer.Clear();
        
        SetPosition(movementStrategy.InitPosition());

        // Might look better if this distribution was logarithmic instead of linear
        float newScale = Random.Range(0.01f, 2);
        gameObject.transform.localScale = new Vector3(newScale, newScale, newScale);

        Renderer renderer = gameObject.GetComponent<Renderer>();
        // Is there a better way than using the magic string "_Color"?
		Color materialColor = new Color(
			Random.Range(0, 1.0f),
			Random.Range(0, 1.0f),
			Random.Range(0, 1.0f),
			Random.Range(0, 1.0f));
        renderer.material.SetColor("_Color", materialColor);

		trailRenderer.endColor = materialColor;
		trailRenderer.startColor = materialColor;
    }

    private void SetPosition(Vector3 position)
    {
        gameObject.transform.position = position;
    }
}
