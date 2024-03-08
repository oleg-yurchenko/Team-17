using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass // inherite
    {
        private Text textHolder;
        [Header("Text Options")] // allowing sections in Unity with custom names
        [SerializeField]private string input; // SerializeField: so it can be edited in Unity(?)
        [SerializeField]private Color textColor;
        [SerializeField]private Font textFont;

        private void Awake()
        {
            textHolder = GetComponent<Text>();

            // StartCoroutine(WriteText(input, textHolder));
            if (textHolder != null)
            {
                textHolder.text = ""; // Reset text value
                StartCoroutine(WriteText(input, textHolder, textColor, textFont));
            }
            else
            {
                Debug.LogError("Text component not found on the GameObject.", this);
            }
        }
        
    
    }

}

