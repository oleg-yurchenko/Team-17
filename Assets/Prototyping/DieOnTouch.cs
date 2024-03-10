using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieOnTouch : MonoBehaviour
{
    public int Respawn;
    
    // This method is called when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player
        if (collision.gameObject.tag == "Player")
        {
            // Debug.Log("touching death");
            // Load the initial scene
            SceneManager.LoadScene(Respawn);
        }
    }
}
