using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;

    // called on first frame that the script is active
    // (usually the first frame of the game)
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // do physics here
    void FixedUpdate()
    {
        // float moveHorizontal = Input.GetAxis("Horizontal");
        // float moveVertical = Input.GetAxis("Vertical");

        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var x = moveHorizontal;
        var y = 0;
        var z = moveVertical;
        var movement = new Vector3(x, y, z);

        rb.AddForce(movement * speed);
    }
}
