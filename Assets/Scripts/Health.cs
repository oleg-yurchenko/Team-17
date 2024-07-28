using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int health; // current num of hearts
    public int numOfHearts; // current max num of hearts

    public Image[] hearts; // stores the forever max num of hearts 
    // for now the default value I set is 10
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // Sprite art logistics goes here
    void Update()
    {

        // the player should not gain more health than the max num of heart containers
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++) // for every heart images in hearts
        {

            if (i < health) // for the current num of hearts
            {
                hearts[i].sprite = fullHeart; // display a full heart
            }
            else
            {
                hearts[i].sprite = emptyHeart; // display an empty heart
            }

            if (i < numOfHearts) // within the range of our current max heart num
            {
                hearts[i].enabled = true; // we want heart[i] to be visible
            }
            else
            {
                hearts[i].enabled = false; // we don want the rest to be visible
            }
        }
        
    }


    // health system logistics goes here
    // void FixedUpdate()
    // {


    // }
}