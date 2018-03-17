using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMover : MonoBehaviour {
    private const float CAMERA_Z_POSITION = -10;

    private float zSpeed = -0.1f;
    private float xSpeed;
    private float ySpeed;

	// Use this for initialization
	void Start ()
    {
        randomizeObject();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Make the spheres move in a parabolic arc like a comet
        Vector3 translation = new Vector3(xSpeed, ySpeed, zSpeed);
        gameObject.transform.Translate(translation);

        if (gameObject.transform.position.z < CAMERA_Z_POSITION)
        {
            randomizeObject();
        }
    }

    private void randomizeObject()
    {
        // The field of view from the camera should determine these numbers
        float newX = Random.Range(-10, 10);
        float newY = Random.Range(-9, 11);
        float newZ = 10;
        gameObject.transform.position = new Vector3(newX, newY, newZ);

        // Might look better if this distribution was logarithmic instead of linear
        float newScale = Random.Range(0.01f, 2);
        gameObject.transform.localScale = new Vector3(newScale, newScale, newScale);

        zSpeed = Random.Range(-0.5f, -0.1f);

        Renderer renderer = gameObject.GetComponent<Renderer>();
        // Is there a better way than using the magic string "_Color"?
        renderer.material.SetColor(
            "_Color",
            new Color(
                Random.Range(0, 1.0f),
                Random.Range(0, 1.0f),
                Random.Range(0, 1.0f),
                Random.Range(0, 1.0f)));

        xSpeed = Random.Range(-0.1f, 0.1f);
        ySpeed = Random.Range(-0.1f, 0.1f);
    }
}
