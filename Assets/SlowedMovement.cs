using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowedMovement : MonoBehaviour
{
    public float c_speed = 0.1f;
    public GameObject platform;
    // Start is called before the first frame update
    void Start()
    {
        platform = GameObject.Find("Platform");
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position.y >= platform.transform.position.y + 0.7f) && (transform.position.y <= platform.transform.position.y + 0.9f) && (transform.position.x >= platform.transform.position.x - 4.0f) && (transform.position.x <= platform.transform.position.x + 4.0f)) {
            c_speed-=0.0001f;
        } 
        else {
            c_speed = 0.1f;
        }   
        float xPos = Input.GetAxis("Horizontal");
        float yPos = Input.GetAxis("Vertical");
        float tempXPos = 0.1f*c_speed*xPos;
        float tempYPos = 0.1f*c_speed*yPos;
        Vector3 moveDir = new Vector3(tempXPos,tempYPos,0.0f);
        transform.position += moveDir;
    }
}
