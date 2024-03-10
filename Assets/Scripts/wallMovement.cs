using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallMovement : MonoBehaviour
{

    public float c_wallJumpForceMultiplier;
    public float c_wallHorizontalVelocity;
    public int movementDisableFrames;
    private string playerState;
    private MovementPrototypeController something;
    private Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        something = GetComponent<MovementPrototypeController>();
        playerState = something.getPlayerState();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        playerState = something.getPlayerState();
        if (!something.getHasJumped())
        {
            // Debug.Log("passed jump");
        }
        // Debug.Log(playerState);


        if (playerState == "wallLeft" && !something.getHasJumped() && Input.GetKey("w") /*&& Input.GetKey("d")*/)
        {
            body.velocity = new Vector2(1 * c_wallHorizontalVelocity, body.velocity.y);
            body.AddForce(Vector2.up * c_wallJumpForceMultiplier, ForceMode2D.Impulse);
            playerState = "air";
            something.setHasJumped(true);
            something.setDisabled(movementDisableFrames);
        }
        else if (playerState == "wallRight" && !something.getHasJumped() && Input.GetKey("w") /*&& Input.GetKey("a")*/)
        {
            body.velocity = new Vector2(-1 * c_wallHorizontalVelocity, body.velocity.y);
            body.AddForce(Vector2.up * c_wallJumpForceMultiplier, ForceMode2D.Impulse);
            playerState = "air";
            something.setHasJumped(true);
            something.setDisabled(movementDisableFrames);
        }
    }
}
