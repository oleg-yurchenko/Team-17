using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GravityDirection
{
    UP,
    DOWN,
}

public class MovementTest : MonoBehaviour
{
    [SerializeField] GravityDirection direction = GravityDirection.DOWN;
    [SerializeField] float movementSpeed = 5;
    [SerializeField] float jumpSpeed = 6;
    [SerializeField] float groundCheckHeight = 0.7f;
    [SerializeField] float groundCheckDistance = 0.15f;
    
    Rigidbody2D rb;
    bool isGrounded = false;
    [SerializeField] LayerMask groundMask;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        HandleGroundMovement();
        HandleGravitySwap();
        
    }

    void CheckGrounded()
    {
        Vector2 rayCastDirection;
        if(direction == GravityDirection.UP)
        {
            rayCastDirection = Vector2.up;
        } else
        {
            rayCastDirection= Vector2.down;
        }

        if (Physics2D.Raycast(transform.position + Vector3.right * groundCheckDistance, rayCastDirection, groundCheckHeight, groundMask) ||
            Physics2D.Raycast(transform.position + Vector3.left * groundCheckDistance, rayCastDirection, groundCheckHeight, groundMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }


    }
    void HandleGroundMovement()
    {
        if(Input.GetKey(KeyCode.W) && isGrounded)
        {
            isGrounded = false;
            if(direction == GravityDirection.UP)
            {
                rb.velocity = new Vector2(rb.velocity.x, -jumpSpeed);
            } else
            {
                
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                
            }
            
        }
        if(Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    void HandleGravitySwap()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (direction != GravityDirection.UP)
            {
                direction = GravityDirection.UP;
                transform.rotation = Quaternion.Euler(Vector3.forward * 180);
                rb.gravityScale = -1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (direction != GravityDirection.DOWN)
            {
                direction = GravityDirection.DOWN;
                transform.rotation = Quaternion.Euler(Vector3.forward * 0);
                rb.gravityScale = 1;
            }
        }
    }
}
