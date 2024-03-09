using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followplayer : MonoBehaviour
{
    // public GameObject player;
    // public float offset;
    // public float smoothing;

    // public Vector3 playerPosition;
    // private Vector3 velocity = Vector3.zero;

    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothing = 0.5f;

    private Vector3 velocity = Vector3.zero;

    private Vector3 playerPosition;

    [SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         playerPosition = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);

         if (target.transform.localScale.x > 0f) {
             offset = new Vector3(5f, 0f, -10f);
         }
         else if (target.transform.localScale.x < 0f) {
             offset = new Vector3(-5f, 0f, -10f);
         }
        
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing);

    }

    
}
