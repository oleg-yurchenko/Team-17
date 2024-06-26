using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleObstacle : MonoBehaviour
{
    public GameObject PointABigSkyIsland; // First point of the triangle
    public GameObject PointBBigSkyIsland; // Second point of the triangle
    public GameObject PointCBigSkyIsland; // Third point of the triangle

    private Rigidbody2D body;
    private Animator anim; // no use for now
    private Transform currentPoint;
    public float speed;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // no use for now
        currentPoint = PointABigSkyIsland.transform; // Start at Point A
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Determine direction towards the current point
        Vector2 direction = (currentPoint.position - transform.position).normalized;
        // Debug.Log(direction);

        // Move towards the current point
        body.velocity = direction * speed;
        // Debug.Log(body.velocity);


        // Check if the obstacle has reached the current point
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            // Move to the next point in the triangle
            if (currentPoint == PointABigSkyIsland.transform)
            {
                currentPoint = PointBBigSkyIsland.transform; // A -> B
            }    
            else if (currentPoint == PointBBigSkyIsland.transform)
            {
                currentPoint = PointCBigSkyIsland.transform; // B -> C
            }
            else if (currentPoint == PointCBigSkyIsland.transform)
            {
                currentPoint = PointABigSkyIsland.transform; // C -> A
            }
        }
    }
}
