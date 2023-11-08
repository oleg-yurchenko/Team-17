using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPrototypeController : MonoBehaviour
{
    public float c_moveForceMultiplier;
    public float c_airMoveForceMultiplier;
    public float c_jumpForceMultiplier;
    public float c_speedLimit;
    private Vector2 horizontalForce = new Vector2();
    private Vector2 verticalForce = new Vector2();
    private Rigidbody2D body;
    private bool hasJumped;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        hasJumped = false;
    }

    // Update is called once per frame
    void Update() {

    }
    // FixedUpdate used for physics calculations
    void FixedUpdate()
    {
        horizontalForce = new Vector2();
        verticalForce = new Vector2();
        float horizontalMultiplier = (hasJumped ? c_airMoveForceMultiplier : c_moveForceMultiplier);
        if (Input.GetKey("w"))
            verticalForce += Vector2.up * c_jumpForceMultiplier;
        if (Input.GetKey("a"))
            horizontalForce += Vector2.left * horizontalMultiplier;
        if (Input.GetKey("d"))
            horizontalForce += Vector2.right * horizontalMultiplier;

        if (!hasJumped && verticalForce.magnitude > 0.0f) {
            body.AddForce(verticalForce, ForceMode2D.Impulse);
            hasJumped = true;
        } else if (body.velocity.y == 0.0f) {
            hasJumped = false;
        }
        if (body.velocity.x <= c_speedLimit)
            body.AddForce(horizontalForce);
    }
}
