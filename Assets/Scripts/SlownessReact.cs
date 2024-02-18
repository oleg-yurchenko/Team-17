using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlownessReact : MonoBehaviour
{
    public float c_speed = 0.001f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        float xPos = Input.GetAxis("Horizontal");
        float yPos = Input.GetAxis("Vertical");
        float tempXPos = 0.1f*c_speed*xPos;
        float tempYPos = 0.1f*c_speed*yPos;
        Vector3 moveDir = new Vector3(tempXPos, tempYPos, 0.0f);
        transform.position += moveDir;
        */
    }

    void OnCollisionStay2D(Collision2D collision) {
        /*
       if (collision.gameObject.name == "LeftFloor") {
            Debug.Log("This is the left floor");
            c_speed /= 1.001f;
       } else if (collision.gameObject.name == "RightFloor") {
            Debug.Log("This is the right floor");
            c_speed *= 1.001f;
       }
       */
       float originalMoveForceMultiplier = gameObject.GetComponent<MovementPrototypeController>().c_originalMoveForceMultiplier;
        if (collision.gameObject.tag == "Slow") {
            gameObject.GetComponent<MovementPrototypeController>().c_moveForceMultiplier = originalMoveForceMultiplier * c_speed;
        } else {
            gameObject.GetComponent<MovementPrototypeController>().c_moveForceMultiplier = originalMoveForceMultiplier;
        }
    }
    /*
    void OnCollisionStay2D(Collision2D col) {
        if (col.gameObject.name == "Tilemap") {
            //Debug.Log("This is a tile map");
            c_speed /= 1.001f;
        } //else if (col.gameObject.name == "Tilemap (1)") {
        //     Debug.Log("This is another tile map");
        //     c_speed /= 1.001f;
        // }
    }
    */
}
