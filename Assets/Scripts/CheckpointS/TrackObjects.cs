using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObjects : MonoBehaviour
{
    private List<CheckpointSingle> CheckpointSingleList;
    public CheckpointSingle currentCheckpoint;
    private CheckpointSingle initialCheckpoint; // The very first checkpoint 
    // we need this here because for now, if the player dies, the player is set to the beginning of the game

    private GameObject player;
    private Health playerHealth; // tracking player Health when checkpoints are reset

    private void Awake() {
        this.player = GameObject.FindWithTag("Player");
        this.playerHealth = player.GetComponent<Health>(); // Get the Health component from the player
        Transform checkpointTransform = transform.Find("Checkpoints");
        CheckpointSingleList = new List<CheckpointSingle>();
        // this.currentCheckpoint = checkpointTransform.GetChild(0).GetComponent<CheckpointSingle>();
        // Debug.Log(currentCheckpoint);

        // setting up initial checkpoints
        this.initialCheckpoint = checkpointTransform.GetChild(0).GetComponent<CheckpointSingle>();
        this.currentCheckpoint = initialCheckpoint;

        foreach (Transform thisCheckpoint in checkpointTransform) {
            CheckpointSingle checkpointSingle = thisCheckpoint.GetComponent<CheckpointSingle>();
            checkpointSingle.SetTrackCheckpoints(this);
            CheckpointSingleList.Add(checkpointSingle);
            // Debug.Log(thisCheckpoint);
        }
    }

    public void PlayerThroughCheckpoint(CheckpointSingle checkpointSingle) {
        this.currentCheckpoint = checkpointSingle;
        Debug.Log("Passed " + checkpointSingle.name);
    }

    public void ResetLevel() {
        // BroadcastMessage("ResetLevel");
        // Debug.Log("you reduce heart 33333");
        playerHealth.ReduceHealth(1); // reduce the player Health by 1 heart

        // check if the player is dead or not
        if (playerHealth.IsDead()) 
        {
            Debug.Log("You Died! Resetting to the very beginning.");
            currentCheckpoint = initialCheckpoint; // Reset to the first checkpoint
            playerHealth.RestoreFullHealth(); // Restore full health
        } else {
            Debug.Log("You have fallen! Resetting to checkpoint: " + currentCheckpoint.name);
        }
        // Debug.Log("You Died! Resetting to checkpoint: " + currentCheckpoint.name);

        player.transform.position = currentCheckpoint.transform.position;
        Rigidbody2D  playerRigidBody = player.GetComponent<Rigidbody2D>();
        playerRigidBody.velocity = Vector3.zero;
        playerRigidBody.angularVelocity = 0;
    }
}
