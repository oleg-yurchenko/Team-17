using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

//Note to Self: all void functions are default private in c#/unity
public class CoinDisplay : MonoBehaviour
{
    private TextMeshProUGUI myGui;
    private int coins = 0;
    // Start is called before the first frame update
    public void Start()
    {
        myGui = GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myGui != null) {
            myGui.SetText("Your Coins: " + coins);
        }
    }

    public void addCoins() {
        this.coins++;
    }
}
