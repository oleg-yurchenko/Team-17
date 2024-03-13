using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObjects : MonoBehaviour
{
    private List<CheckpointSingle> CheckpointSingleList;
    public CheckpointSingle currentCheckpoint;

    private GameObject player;

    private void Awake() {
        this.player = GameObject.FindWithTag("Player");
        Transform checkpointTransform = transform.Find("Checkpoints");
        CheckpointSingleList = new List<CheckpointSingle>();
        this.currentCheckpoint = checkpointTransform.GetChild(0).GetComponent<CheckpointSingle>();
        Debug.Log(currentCheckpoint);

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
        Debug.Log("You Died! Resetting to checkpoint: " + currentCheckpoint.name);
        player.transform.position = currentCheckpoint.transform.position;
        Rigidbody2D  playerRigidBody = player.GetComponent<Rigidbody2D>();
        playerRigidBody.velocity = Vector3.zero;
        playerRigidBody.angularVelocity = 0;
    }
}
