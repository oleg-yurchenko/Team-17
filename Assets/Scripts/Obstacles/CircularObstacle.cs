using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularObstacle : MonoBehaviour
{
    public GameObject centerPositionInJail; // GameObject used as the center of the circular path
    public float radius = 5f; // Radius of the circular path // can be adjusted in Unity
    public float speed = 1f;  // Speed of the obstacle's movement // can be adjusted in Unity
    private float angle; // Current angle of the obstacle on the circle

    void Start()
    {
        // Ensure the centerPositionInJail is assigned
        if (centerPositionInJail == null)
        {
            Debug.LogError("Center position is not assigned.");
        }

        // Initialize the angle based on the initial position
        Vector2 direction = transform.position - centerPositionInJail.transform.position;
        angle = Mathf.Atan2(direction.y, direction.x);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (centerPositionInJail != null)
        {
            // Update the angle
            angle += speed * Time.deltaTime;

            // Calculate the new position of the obstacle using trigonometric functions
            float x = centerPositionInJail.transform.position.x + Mathf.Cos(angle) * radius;
            float y = centerPositionInJail.transform.position.y + Mathf.Sin(angle) * radius;

            // Set the new position of the obstacle
            transform.position = new Vector2(x, y);
        }
    }



    // if the player gets in contact with the sqaure, reset level and teleport the player to the nearest check point.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Find the TrackObjects script in the scene
            TrackObjects trackObjects = FindObjectOfType<TrackObjects>();
            if (trackObjects != null)
            {
                trackObjects.ResetLevel();
            }
        }
    }
}