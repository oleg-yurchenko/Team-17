using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieOnTouch : MonoBehaviour
{
    private TrackObjects trackObject;
    
    // This method is called when another collider enters the trigger collider attached to this object

    void Start() {
        this.trackObject = transform.parent.parent.parent.GetComponent<TrackObjects>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player
        if (collision.gameObject.tag == "Player")
        {
            // Debug.Log("touching death");
            // Load the initial scene
            // Debug.Log("dieontouch reset"); // there are sooo many ResetLevel calls.......
            trackObject.ResetLevel();
        }
    }
}
