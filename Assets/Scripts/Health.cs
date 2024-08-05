using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    // ref: https://www.youtube.com/watch?v=3uyolYVsiWc&ab_channel=Blackthornprod

    public int health; // current num of hearts
    // for now the default value I set is 3
    public int numOfHearts; // current max num of hearts
    // for now the default initial value is 3, the player needs to "upgrade" to get to 10 hearts in game

    public Image[] hearts; // stores the max num of hearts (cannot get more than this number of hearts no matter how the player "upgrades")
    // for now the default value I set is 10
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHeartsDisplay();
    }

    void Update()
    {
        UpdateHeartsDisplay();
    }

    // Update is called once per frame
    // Sprite art logistics goes here
    void UpdateHeartsDisplay()
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

    // Method to reduce health by amount
    public void ReduceHealth(int amount)
    {
        health -= amount; 
        if (health < 0) health = 0; // health cannot be negative
        UpdateHeartsDisplay();
    }

    // Method to increase health by amount
    // not used for now
    public void IncreaseHealth(int amount)
    {
        health += amount; 
        if (health > hearts.Length) health = hearts.Length; // health cannot exceed the max health
        UpdateHeartsDisplay();
    }

    // Method to check if player is dead
    public bool IsDead()
    {
        return health <= 0;
    }

    // Method to restore health to full
    public void RestoreFullHealth()
    {
        health = numOfHearts;
        UpdateHeartsDisplay();
    }


}