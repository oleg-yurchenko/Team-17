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

    public float c_wallSpeed;
    private Vector2 horizontalForce = new Vector2();
    private Vector2 verticalForce = new Vector2();
    private Rigidbody2D body;
    private int disabled;
    private string playerState; // "air" for jumping, "ground" for on ground, "wall" for on the wall
	private ChargeJumpScript chargeJump;
    public bool hasJumped;
    private float jumpTime;
	private bool isSlowing = false;
	private float initialForce;
    public bool hasDoubleJumped;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerState = "air";
        hasJumped = false;
        disabled = 0;
		chargeJump = GetComponent<ChargeJumpScript>();
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
        disabled--;
        if (onWall() && body.velocity.y < c_wallSpeed)
        {
            Debug.Log("yes");
            body.velocity = new Vector2(body.velocity.x, c_wallSpeed);
            hasJumped = false;
        }

        bool inAir = (playerState == "air");

        horizontalForce = new Vector2();
        verticalForce = new Vector2();
        float horizontalMultiplier = (inAir ? c_airMoveForceMultiplier : c_moveForceMultiplier);
        if (Input.GetKey("a") && disabled <= 0)
        {
            // horizontalForce += Vector2.left * horizontalMultiplier;
            body.velocity = new Vector2(-c_speedLimit*horizontalMultiplier, body.velocity.y);
        }
        if (Input.GetKey("d") && disabled <= 0)
        {
            // horizontalForce += Vector2.right * horizontalMultiplier;
            body.velocity = new Vector2(c_speedLimit*horizontalMultiplier, body.velocity.y);
        }
        if (playerState == "ground" && !hasJumped && Input.GetKey("w")) {
            verticalForce += Vector2.up * c_jumpForceMultiplier;
            Jump();
            playerState = "air";
            hasJumped = true;
        } else if (body.velocity.y == 0.0f) {
            hasJumped = false;
            hasDoubleJumped = false;
        }
        
        // if (body.velocity.x <= c_speedLimit)
            // body.AddForce(horizontalForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // Check the normal of the contact point
                if (Mathf.Abs(contact.normal.y) < 0.5)
                {
                    if (contact.normal.x > 0)
                    {
                        // Collision with left side
                        playerState = "wallLeft";
                        // Debug.Log("Collided on the left side");
                    }
                    else if (contact.normal.x < 0)
                    {
                        // Collision with right side
                        playerState = "wallRight";
                        // Debug.Log("Collided on the right side");
                    }
                    return;
                }
            }
            playerState = "ground";
        }
        if (collision.gameObject.tag == "Ground")
        {
            playerState = "ground";
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (onWall())
        {
            playerState = "air";
            // The object stopped colliding with the side of the terrain
            // Debug.Log("Stopped colliding with the side of the object.");
        }
    }

    bool onWall()
    {
        return (playerState == "wallLeft") || (playerState == "wallRight");
    }

    public string getPlayerState()
    {
        return playerState;
    }

    public void setPlayerState(string playerState)
    {
        this.playerState = playerState;
    }

    public bool getHasJumped()
    {
        return hasJumped;
    }

    public void setHasJumped(bool jumped)
    {
        hasJumped = jumped;
    }

    public void setDisabled(int frames)
    {
        disabled = frames;
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