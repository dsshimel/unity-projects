using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Plane floor;

    private Rigidbody rb;
    private int count;

    // called on first frame that the script is active
    // (usually the first frame of the game)
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        SetWinText("");
    }

    // do physics here
    void FixedUpdate()
    {
        // xbox controller support comes for free!
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");
        var justPressedJump = Input.GetButtonDown("Jump");
        //var holdingJump = Input.GetButton("Jump");
        var finishedPressingJump = Input.GetButtonDown("Jump");

        var x = moveHorizontal;
        var y = 0;
        var z = moveVertical;
        var movement = new Vector3(x, y, z);

        rb.AddForce(movement * speed);
        if ((justPressedJump || /*holdingJump || */finishedPressingJump) && this.transform.position.y == 0.5)
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // "Pick Up" is a magic string here
        // It needs to be added to the list of tags in the
        // Pickup prefab
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        // 12 is a magic number here :/
        if (count == 12)
        {
            SetWinText("You Win!");
        }
    }

    private void SetWinText(string newText)
    {
        winText.text = newText;
    }
}
