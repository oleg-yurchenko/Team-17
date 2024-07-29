using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqaureObstacle : MonoBehaviour
{

    // ref: https://www.youtube.com/watch?v=RuvfOl8HhhM&ab_channel=MoreBBlakeyyy

    public GameObject PointABeforeHill; // left
    public GameObject PointBBeforeHill; // right

    private Rigidbody2D body;
    private Animator anim; // no use for now
    private Transform currPoint;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // no use for now
        currPoint = PointBBeforeHill.transform; // starting at B
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector2 point = currPoint.position - transform.position; // give us a direction that the obstacle wants to go towards
        if(currPoint == PointBBeforeHill.transform) // starting at B
        {
            body.velocity = new Vector2(speed, 0); // don want y to change for this sqaure obs
        }
        else
        {
            body.velocity = new Vector2(-speed, 0); // going to the other direction
        }

        // check if the obs has reached B again
        if(Vector2.Distance(transform.position, currPoint.position) < 0.5f && currPoint == PointBBeforeHill.transform)  
        {   
            // set the current point to A
            currPoint = PointABeforeHill.transform;
        }

        // check if the obs has reached A again
        if(Vector2.Distance(transform.position, currPoint.position) < 0.5f && currPoint == PointABeforeHill.transform)  
        {   
            // set the current point to B
            currPoint = PointBBeforeHill.transform;
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
                // Debug.Log("you touched 11111");
                trackObjects.ResetLevel();
                // Debug.Log("you reset 22222");
            }
        }
    }
}
