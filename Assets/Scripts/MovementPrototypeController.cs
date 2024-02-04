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
    public bool hasJumped;
    private bool jumpTime;

    public bool hasDoubleJumped;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        hasJumped = false;
        jumpTime = 0.2f;
        hasDoubleJumped = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("w")) {
            verticalForce += Vector2.up * c_jumpForceMultiplier;
            Jump();
        }
        if (body.velocity.y == 0.0f) {
            hasJumped = false;
            hasDoubleJumped = false;
        }
    }
    //not sure how to implement this code in fixed update (input.getkeydown doesn't work very well)

    // FixedUpdate used for physics calculations
    void FixedUpdate()
    {
        horizontalForce = new Vector2();
        verticalForce = new Vector2();
        float horizontalMultiplier = (hasJumped ? c_airMoveForceMultiplier : c_moveForceMultiplier);
        if (Input.GetKey("a"))
            horizontalForce += Vector2.left * horizontalMultiplier;
        if (Input.GetKey("d"))
            horizontalForce += Vector2.right * horizontalMultiplier;

        if (body.velocity.x <= c_speedLimit)
            body.AddForce(horizontalForce);
    }

    void Jump() 
    {
        if (!hasJumped) {
                body.AddForce(verticalForce, ForceMode2D.Impulse);
                hasJumped = true;
            } 
            else if (!hasDoubleJumped) {
                body.AddForce(verticalForce, ForceMode2D.Impulse);
                hasDoubleJumped = true;
            }
    }
}
// can think about increasing gravity at the height of the jump to make it 'feel' better