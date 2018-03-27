using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMover : MonoBehaviour {
    // Min distance meteor can be from player.
    public float radiusInner;
    // Max distance meteor can be from player.
    public float radiusOuter;
    // Z = Rsin(angleZY)
    // Y = Rcos(angleZY)
    public float lengthX;

    private float angleZY;
    private float radius;
    private float x;
    private float angularVelocity;

	// Use this for initialization
	void Start ()
    {
        initializeSphere();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Make the spheres move in a parabolic arc like a comet
        float newY = radius * Mathf.Sin(angleZY);
        float newZ = radius * Mathf.Cos(angleZY);
        gameObject.transform.position = new Vector3(x, newY, newZ);

        angleZY += -1 * angularVelocity * Mathf.PI;
    }

    private void initializeSphere()
    {
		TrailRenderer trailRenderer = gameObject.GetComponent<TrailRenderer>();
		trailRenderer.Clear();

        radius = Random.Range(radiusInner, radiusOuter);
        angleZY = Random.Range(0, 2 * Mathf.PI);
        angularVelocity = Random.Range(0.005f, 0.01f);

        // The field of view from the camera should determine these numbers
        float newX = Random.Range(-lengthX, lengthX);
        x = newX;
        float newY = radius * Mathf.Sin(angleZY);
        float newZ = radius * Mathf.Cos(angleZY);
        gameObject.transform.position = new Vector3(newX, newY, newZ);

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
}
