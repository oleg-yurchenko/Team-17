using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePrototype : MonoBehaviour
{
    public float grappleDistance = 10f;
    public LayerMask grappleLayer;
    public float grappleForce = 5f;
    public float swingForce = 2f;

    private bool isGrappling = false;
    private Vector2 grapplePoint;
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
            // Debug.Log("Grappling");
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
            // Debug.Log("Stop Grappling");
        }
    }

    void FixedUpdate()
    {
        if (isGrappling)
        {
            Vector2 grappleDir = (grapplePoint - (Vector2)transform.position).normalized;
            body.AddForce(grappleDir * grappleForce, ForceMode2D.Force);

            // Swing force, still testing if this really works
            body.AddForce(new Vector2(grappleDir.y, -grappleDir.x) * swingForce, ForceMode2D.Force);
        }
    }

    void StartGrapple()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 grappleDirection = (mousePosition - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, grappleDirection, grappleDistance, grappleLayer);

        // Still testing how to make it grapple up and not just straight towrds the mouse direction
        if (hit.collider != null)
        {
            grapplePoint = hit.point;
            isGrappling = true;

            // animations can probably go here
        }
    }

    void StopGrapple()
    {
        isGrappling = false;
        
        // stopping animations?
    }
}