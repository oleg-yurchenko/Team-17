using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehaviour : MonoBehaviour
{
    private int coins = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Coin")
        {
            coins++;
            if (coins == 1)
            {
                Debug.Log("You have " + coins + " coin!");
            }
            else
            {
                Debug.Log("You have " + coins + " coins!");
            }
            Destroy(collision.gameObject);
        }
    }
}
