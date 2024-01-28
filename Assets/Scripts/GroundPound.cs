using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : MonoBehaviour
{
    public float c_poundForceMultiplier;
    public bool isPounding;
    public bool canPound;
    public bool isFrozen;
    public LayerMask groundLayer;
    public float poundDistance = 1;
    public float freezeTimeThreshold = 0.5f;
    public Vector2 boxSize = new Vector2(1f, 2.5f); // Adjust the size of the rectangle

    private float lastTapTime;
    private float initFrozenTime;
    private float frozenTime;
    public float doubleTapTimeThreshold = 0.5f; // Adjust this threshold as needed
    
    private Vector2 verticalForce = new Vector2();
    private Collider2D collider;
    private SpriteRenderer renderer;
    
    
    private Rigidbody2D rb;


    void Start()
    {
        isPounding = false;
        canPound = false;
        isFrozen = false;
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isFrozen)
        {
            if (Input.GetKeyDown("s") && canPound)
            {
                // Check if it's a double tap
                if (Time.time - lastTapTime < doubleTapTimeThreshold)
                {
                    isPounding = true;
                    canPound = false;
                    isFrozen = true;
                    initFrozenTime = Time.time;
                    renderer.color = Color.white;
                }

                lastTapTime = Time.time;
            }

            //on the ground again - free movement
            if (!canPound && !isPounding)
            {
                rb.constraints = RigidbodyConstraints2D.None;
            }
        } 
        else //player is frozen - freeze for a second before pounding
        {
            //freeze x and y position
            if (frozenTime - initFrozenTime < freezeTimeThreshold)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            } 
            else {
                //freeze only x, let fall
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.None;
                isFrozen = false;
                verticalForce = new Vector2();
                verticalForce += Vector2.down * c_poundForceMultiplier;
                rb.AddForce(verticalForce, ForceMode2D.Impulse);
            }
            frozenTime = Time.time;
        }

    }

    // FixedUpdate used for physics calculations
    void FixedUpdate()
    {
        //print("Can Blink: "+canBlink.ToString());
        //print("Is Blinking: "+isBlinking.ToString());
        //print("Active Timer: "+blink_activeTimer.ToString());
        //print("delta time: " + Time.fixedDeltaTime);

        //cast ray to check if poundDistance away from the ground
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxSize, 0f, Vector2.down, 0f, groundLayer);
        //not near ground - can pound
        if (hit.collider == null)
        {
            // cant pound if already pounding
            if (isPounding == false)
            {
                canPound = true;
                renderer.color = Color.red;
            }
            
        } else
        {
            canPound = false;
            isPounding = false;
            renderer.color = Color.white;
        }
        
       


    }
}
