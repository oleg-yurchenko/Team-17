using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    private TrackObjects trackObject;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            this.trackObject.PlayerThroughCheckpoint(this);
        }
    }

    public void SetTrackCheckpoints(TrackObjects trackObjects) {
        this.trackObject = trackObjects;
    }
}
