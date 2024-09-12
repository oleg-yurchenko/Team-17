using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehaviour : MonoBehaviour
{
    private CoinDisplay coinDisplay;
    // Start is called before the first frame update
    void Start()
    {
        coinDisplay = gameObject.AddComponent<CoinDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            coinDisplay.addCoins();
        }
    }
}
