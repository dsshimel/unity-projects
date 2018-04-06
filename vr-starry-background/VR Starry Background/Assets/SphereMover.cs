using UnityEngine;

public class SphereMover : MonoBehaviour {
    // Min distance meteor can be from player.
    public float radiusInner;
    // Max distance meteor can be from player.
    public float radiusOuter;

    private MovementStrategy movementStrategy;
    private ParticleSystem trails;
    private bool isMoving;

    void Start ()
    {
        isMoving = false;
        trails = GetComponentInChildren<ParticleSystem>();

        movementStrategy = new SphereTubeStrategy(radiusInner, radiusOuter);
        //movementStrategy = new HamsterWheelStrategy(radiusInner, radiusOuter);
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

        Renderer renderer = gameObject.GetComponent<Renderer>();
        // Is there a better way than using the magic string "_Color"?
		Color materialColor = new Color(
			Random.Range(0, 1.0f),
			Random.Range(0, 1.0f),
			Random.Range(0, 1.0f),
			Random.Range(0, 1.0f));
        renderer.material.SetColor("_Color", materialColor);

        var shape = trails.shape;
        shape.radius = newScale * 2.0f;

        var col = trails.colorOverLifetime;
        Gradient grad = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[] {
            new GradientColorKey(materialColor, 0.0f),
            new GradientColorKey(InvertColor(materialColor), 0.5f) };
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[] {
            new GradientAlphaKey(1.0f, 0.0f),
            new GradientAlphaKey(1.0f, 0.7f),
            new GradientAlphaKey(0.0f, 1.0f) };
        grad.SetKeys(colorKeys, alphaKeys);
        col.color = grad;
    }

    private void SetPosition(Vector3 position)
    {
        gameObject.transform.position = position;
    }

    private Color InvertColor(Color color)
    {
        return new Color(1.0f - color.r, 1.0f - color.g, 1.0f - color.b);
    }
}
