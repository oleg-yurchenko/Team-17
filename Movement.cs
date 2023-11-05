using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adapted from https://www.youtube.com/watch?v=KiVL5rfVdTw&ab_channel=Sorcerer

public class Movement : MonoBehaviour
{
    public float speed = 0.5f;
    //Vector3 temp = Vector3.right + Vector3.up;
    void Start() 
    {

    }
    // Update is called once per frame
    void Update()
    {
        // if (transform.position.x <= -10) {
        //     temp -= 2*Vector3.left;
        // } else if (transform.position.x >= 10) {
        //     temp -= 2*Vector3.right;
        // } else if (transform.position.y <= -5) {
        //     temp -= 2*Vector3.down;
        // } else if (transform.position.y >= 5) {
        //     temp -= 2*Vector3.up;
        // }
        // transform.position += temp;
        if (speed <= 0.0f) {
            return;
        }
        float xPos = Input.GetAxis("Horizontal");
        float yPos = Input.GetAxis("Vertical");
        // if ((xPos + yPos) < 10) {
        //     speed -= 0.001f;
        // } else {
        //     speed += 0.001f;
        // }
        float tempXPos = speed*xPos;
        float tempYPos = speed*yPos;
        Vector3 moveDir = new Vector3(tempXPos,tempYPos,0.0f);
        transform.position += moveDir;
        speed -= 0.001f;
    }
}
