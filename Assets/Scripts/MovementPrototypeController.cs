using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPrototypeController : MonoBehaviour
{
    public float c_originalMoveForceMultiplier;
    public float c_moveForceMultiplier;
    public float c_airMoveForceMultiplier;
    public float c_jumpForceMultiplier;
    public float c_speedLimit;
    public float c_wallSpeed;
    public float c_stopThreshold; // Threshold below which player is considered stopped
	public float c_decelerationRate; // Rate at which the player decelerates when speed is below threshold
    // public bool onGround; // T for on ground, F for not on ground

    private Vector2 horizontalForce = new Vector2();
    private Vector2 verticalForce = new Vector2();
    private Rigidbody2D body;
    private int disabled;
    public string playerState; // "air" for jumping, "ground" for on ground, "wallRight"&"wallLeft" for on the wall
	
    private ChargeJumpScript chargeJump;
    public bool hasJumped;
    private float jumpTime;
	private bool isSlowing = false;
	private float initialForce;
    public bool hasDoubleJumped;
	private Animator animator;
	private const float MOVING_ANIMATION_THRESHOLD = 0.5f;
	private const float AIRBORNE_ANIMATION_THRESHOLD = 1.0f;
	private Vector3 initialScale;
	private int doubleJumpDelay = 0;
 
    // public const float STOP_THRESHOLD = 0.4f; // Threshold below which player is considered stopped
	// public const float decelerationRate = 5.0f; // Rate at which the player decelerates when speed is below threshold
    
    [SerializeField]
	private const int DOUBLE_JUMP_THRESHOLD = 40;

    [SerializeField] private GameObject levelObject;
    private TrackObjects trackObject;
    
    
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
		animator = GetComponent<Animator>();
		initialScale = transform.localScale;
        c_originalMoveForceMultiplier = c_moveForceMultiplier;
        trackObject = levelObject.GetComponent<TrackObjects>();
    }

    // Update is called once per frame
    void Update() {
		body.rotation = 0.0f;
		transform.eulerAngles = new Vector3(0,0,0);
		ReducePlayerSpeed();
		animator.SetBool("isGroundedMoving", Math.Abs(body.velocity.x) > MOVING_ANIMATION_THRESHOLD);
		animator.SetBool("isAirborne", Math.Abs(body.velocity.y) > AIRBORNE_ANIMATION_THRESHOLD);
		if (!onWall())
			if (body.velocity.x < 0.0f)
				transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
			else
				transform.localScale = initialScale;
    }

    // FixedUpdate used for physics calculations
    void FixedUpdate()
    {
        disabled--;
        if (disabled > 0)
        {
            return;
        }
        if (onWall() && body.velocity.y < c_wallSpeed)
        {
            // Debug.Log("yes");
            body.velocity = new Vector2(body.velocity.x, c_wallSpeed);
            hasJumped = false;
        }

        bool inAir = (playerState == "air");

        horizontalForce = new Vector2();
        verticalForce = new Vector2();
        float horizontalMultiplier = (inAir ? c_airMoveForceMultiplier : c_moveForceMultiplier);
        if (Input.GetKey("a") && disabled <= 0)
        {
            horizontalForce += Vector2.left * horizontalMultiplier;
            //body.velocity = new Vector2(-c_speedLimit*horizontalMultiplier, body.velocity.y);
        }
        if (Input.GetKey("d") && disabled <= 0)
        {
            horizontalForce += Vector2.right * horizontalMultiplier;
            //body.velocity = new Vector2(c_speedLimit*horizontalMultiplier, body.velocity.y);
        }
        if (Input.GetKey("w")) {
            verticalForce += Vector2.up * c_jumpForceMultiplier;
            Jump();
        //} else if (playerState == "ground") {
        } else if (body.velocity.y == 0.0f && playerState == "ground") {
            hasJumped = false;
            hasDoubleJumped = false;
			doubleJumpDelay = 0;
        }

        // Debug.Log("1111111: " + playerState);
        // Gradually slow down the player when there is no horizontal input (when the player is on the ground)
        if ((playerState == "ground") && (playerState != "air") && (playerState != "wallLeft") && (playerState != "wallRight") && (playerState != "") && (playerState != null) && ((!Input.GetKey("a") && !Input.GetKey("d"))))
        {
            Vector2 targetVelocity = new Vector2(c_stopThreshold, body.velocity.y);
            body.velocity = Vector2.Lerp(body.velocity, targetVelocity, Time.fixedDeltaTime * c_decelerationRate);
            // Debug.Log("222222: " + playerState);
            // Complete stop when speed is below threshold 
            if ( (playerState == "ground") && (playerState != "air") && (playerState != "wallRight") && (playerState != "wallLeft") && (playerState != "") && (playerState != null) && (Mathf.Abs(body.velocity.x) < c_stopThreshold))
            {
                // body.velocity = Vector2.zero;
                body.velocity = new Vector2(0f, body.velocity.y);
            }

        }

        
        if (body.velocity.x <= c_speedLimit)
            body.AddForce(horizontalForce);
		if (hasJumped)
			doubleJumpDelay++;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // for wall
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
                        setDoubleJumpDelay(0);
                    }
                    else if (contact.normal.x < 0)
                    {
                        // Collision with right side
                        playerState = "wallRight";
                        // Debug.Log("Collided on the right side");
                        setDoubleJumpDelay(0);
                    }
                    return;
                }
            }
            // playerState = "ground"; // if collision.gameObject.tag == "Wall", playerState = "ground" ????
        }
        // for obstacle
        if (collision.gameObject.tag == "Obstacle")
        {
            // reseting player location
            trackObject.ResetLevel();
            // reseting dragon location
            // dragon.reset();

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

    bool onWalLLeft() {
        return (playerState == "wallLeft");
    }

       bool onWallRight()
    {
        return (playerState == "wallRight");
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

    public void setDoubleJumpDelay(int num)
    {
        doubleJumpDelay = num;
    }

    void Jump() 
    {
        if (!hasJumped && !onWall()) {
            body.AddForce(verticalForce, ForceMode2D.Impulse);
            hasJumped = true;
			playerState = "air";
        }
        else if (!hasDoubleJumped && doubleJumpDelay >= DOUBLE_JUMP_THRESHOLD && playerState == "air") {
            body.AddForce(verticalForce, ForceMode2D.Impulse);
            hasDoubleJumped = true;
			doubleJumpDelay = 0;
			playerState = "air";
        }
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

    //tempory reset function
    public void reset(float x, float y)
    {
        transform.position = new Vector2(x, y);
    }
}
// can think about increasing gravity at the height of the jump to make it 'feel' better