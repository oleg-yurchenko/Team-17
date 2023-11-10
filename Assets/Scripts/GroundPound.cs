using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : MonoBehaviour
{
    public float c_poundForceMultiplier;

    private Vector2 verticalForce = new Vector2();
    private bool isPounding;
    private bool canPound;
    private Collider2D collider;
    private SpriteRenderer renderer;
    public LayerMask groundLayer;
    public float maxDistance = 0.1f;
    private Rigidbody2D rb;


    void Start()
    {
        isPounding = false;
        canPound = false;
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check for your freeze condition, for example, when a key is pressed
        if (Input.GetKeyDown("down") && canPound)
        {
            // Freeze the position of the player along the X and Y axes

            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            verticalForce = new Vector2();
            verticalForce += Vector2.down * c_poundForceMultiplier;
            rb.AddForce(verticalForce, ForceMode2D.Impulse);

        }

        if (!canPound)
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }

        // Add other player movement or input handling code here
    }

    // FixedUpdate used for physics calculations
    void FixedUpdate()
    {
        //print("Can Blink: "+canBlink.ToString());
        //print("Is Blinking: "+isBlinking.ToString());
        //print("Active Timer: "+blink_activeTimer.ToString());
        //print("delta time: " + Time.fixedDeltaTime);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, maxDistance, groundLayer);
        if (hit.collider == null)
        {
            canPound = true;
        } else
        {
            canPound = false;
        }
        
        if (Input.GetKey("down") && canPound) {
            isPounding = true;
            canPound = false;
        }
        

    }
}
