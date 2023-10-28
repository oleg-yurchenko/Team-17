using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPrototypeController : MonoBehaviour
{
    public float c_moveForceMultiplier;
    public float c_jumpForceMultiplier;
    public float c_speedLimit;
    private Vector2 horizontalForce = new Vector2();
    private Vector2 verticalForce = new Vector2();
    private Rigidbody2D body;
    private bool c_hasJumped;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        c_hasJumped = false;
    }

    // Update is called once per frame
    void Update() {

    }
    // FixedUpdate used for physics calculations
    void FixedUpdate()
    {
        horizontalForce = new Vector2();
        verticalForce = new Vector2();
        if (Input.GetKey("w"))
            verticalForce += Vector2.up * c_jumpForceMultiplier;
        if (Input.GetKey("a"))
            horizontalForce += Vector2.left * c_moveForceMultiplier;
        if (Input.GetKey("d"))
            horizontalForce += Vector2.right * c_moveForceMultiplier;

        if (!c_hasJumped && verticalForce.magnitude > 0.0f) {
                body.AddForce(verticalForce, ForceMode2D.Impulse);
                c_hasJumped = true;
        } else if (body.velocity.y == 0.0f) {
            if (body.velocity.x <= c_speedLimit)
                body.AddForce(horizontalForce);
            c_hasJumped = false;
        }
    }
}
