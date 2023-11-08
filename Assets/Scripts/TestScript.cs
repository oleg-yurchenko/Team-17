using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Sprite squareSprite; // Assign the square sprite in the Inspector
    public float moveSpeed = 2.0f; // Adjust the speed as needed
    private bool isRotating = false; 
    // public float rotationSpeed = 180.0f; // Adjust the rotation speed as needed
    public float rotationAngle = 360.0f; // Adjust the rotation angle as needed
    public float rotationRadius = 2.0f; // Adjust the rotation radius as needed

    // Start is called before the first frame update
    void Start()
    {
        GameObject squareObject = new GameObject("Square");
        SpriteRenderer spriteRenderer = squareObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = squareSprite;
        squareObject.transform.position = new Vector3(rotationRadius, 0, 0); // Initial position of the square, offset to the right
        // Adjust scale or other properties if needed

        
    }

    // Update is called once per frame
    void Update()
    {
        MoveSquare();

        // whether or not it's rotating is depending on whether or not the player hit the R key (once)

        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     isRotating = !isRotating; // Toggle rotation
        // }
        // if (isRotating)
        // {
        //     RotateSquare();
        // }

        if (Input.GetKeyDown(KeyCode.R) && !isRotating)
        {
            RotateSquare();
        }
        
    }

    /// <summary>
    /// Moving the square in a constant speed
    /// </summary>
    void MoveSquare()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime); // Moves the object in the x-direction
        // NOTE: left is right???????

    }

    /// <summary>
    /// Rotate the square 360 degree in the same direction that it's moving
    /// </summary>
    void RotateSquare()
    {
        // transform.Rotate(Vector3.forward, 360f * Time.deltaTime); // Rotates 360 degrees per second around the Z-axis
        // transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime); // Rotates around its Z-axis
        isRotating = true;
        StartCoroutine(RotateAndStop());
    }

    System.Collections.IEnumerator RotateAndStop()
    {

        // rotateing around z-axis

        // float startRotation = transform.rotation.eulerAngles.z;
        // float endRotation = startRotation + rotationAngle;
        // float t = 0;
        // while (t < 1)
        // {
        //     t += Time.deltaTime;
        //     float zRotation = Mathf.Lerp(startRotation, endRotation, t);
        //     transform.rotation = Quaternion.Euler(0, 0, zRotation);
        //     yield return null;
        // }
        // isRotating = false;

        //rotating around itself??????
        Vector3 startRotation = transform.eulerAngles;
        Vector3 endRotation = startRotation + new Vector3(0, 0, rotationAngle);
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, t);
            yield return null;
        }

        isRotating = false;
    }
}
