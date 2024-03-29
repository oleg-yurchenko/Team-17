using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMoveRef : MonoBehaviour
{
    [Header("Next Scene")]
    [SerializeField] private string nextSceneName; // Serialize the next scene name

    // Override
    private void OnTriggerEnter2D(Collider2D other) // other: game object walks into the area
    {
        if(other.tag == "Player"){ // other.GetComponent<Player>() to see if the game object has a Player component works too(?)
            // Player entered, move to next level/scene
            SceneManager.LoadScene(nextSceneName); // this nextScene MUST be in "Scenes in Build": File->Build Settings
        }

    }

    // private void LoadNextScene()
    // {
    //     // Load the next scene
    //     SceneManager.LoadScene(nextSceneName);
            
    // }
}
