using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPrototypeController : MonoBehaviour
{
    public float c_moveForceMultiplier;
    public float c_airMoveForceMultiplier;
    public float c_jumpForceMultiplier;
    public float c_speedLimit;
	private ChargeJumpScript chargeJump;
    private Vector2 horizontalForce = new Vector2();
    private Vector2 verticalForce = new Vector2();
    private Rigidbody2D body;
    public bool hasJumped;
    private float jumpTime;
	private bool isSlowing = false;
	private float initialForce;

    public bool hasDoubleJumped;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
		chargeJump = GetComponent<ChargeJumpScript>();
        hasJumped = false;
        jumpTime = 0.2f;
		initialForce = c_moveForceMultiplier;
        hasDoubleJumped = false;
    }

    // Update is called once per frame
    void Update() {
		ReducePlayerSpeed();
    }
    //not sure how to implement this code in fixed update (input.getkeydown doesn't work very well)

    // FixedUpdate used for physics calculations
    void FixedUpdate()
    {
		if (Input.GetKey("w")) {
            verticalForce += Vector2.up * c_jumpForceMultiplier;
            Jump();
        }
        if (body.velocity.y == 0.0f) {
            hasJumped = false;
            hasDoubleJumped = false;
        }
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
			/*
            else if (!hasDoubleJumped) {
                body.AddForce(verticalForce, ForceMode2D.Impulse);
                hasDoubleJumped = true;
            }
			*/
    }
	public void ReducePlayerSpeed()
	{
		if (chargeJump.c_isCharging && !isSlowing)
		{
			c_moveForceMultiplier /= 2.0f;
			Debug.Log("Slowing Player!");
			
			isSlowing = true; // Mark that the speed reduction has occurred
		}

		else if (!chargeJump.c_isCharging && isSlowing)
		{
			c_moveForceMultiplier = initialForce;
			isSlowing = false;
		}
	}
}
// can think about increasing gravity at the height of the jump to make it 'feel' better