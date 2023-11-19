using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adapted from https://www.youtube.com/watch?v=KiVL5rfVdTw&ab_channel=Sorcerer

public class Movement : MonoBehaviour
{
    public float c_speed = 0.1f;
    void Start() 
    {
    }
    // Update is called once per frame
    void Update()
    {
        float xPos = Input.GetAxis("Horizontal");
        float yPos = Input.GetAxis("Vertical");
        float tempXPos = 0.1f*c_speed*xPos;
        float tempYPos = 0.1f*c_speed*yPos;
        Vector3 moveDir = new Vector3(tempXPos,tempYPos,0.0f);
        transform.position += moveDir;
    }
}
