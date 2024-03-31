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

        [Header("Time Parameters")]
        [SerializeField]private float delayText;
        [SerializeField]private float delayBetweenLines;


        [Header("Input Images")]
        [SerializeField]private Sprite dialogueSprite;
        [SerializeField]private Image imageHolder;

        // private System.Action dialogueFinishedCallback;


        private void Awake()
        {
            textHolder = GetComponent<UnityEngine.UI.Text>();
            // textHolder.text = ""; // in case we put sth in Unity and forgot to delete it

            // StartCoroutine(WriteText(input, textHolder));
            if (textHolder != null)
            {
                textHolder.text = ""; // Reset text value
                imageHolder.sprite = dialogueSprite;
                imageHolder.preserveAspect = true;
            }
            else
            {
                Debug.LogError("1 Text component not found on the GameObject.", this);
            }

        }


        private void Start()
        {

            if (textHolder != null)
            {
                textHolder.text = ""; // Reset text value
                StartCoroutine(WriteText(input, textHolder, textColor, textFont, delayText, delayBetweenLines));
            }
            else
            {
                Debug.LogError("2 Text component not found on the GameObject.", this);
            }

        
        }

        // private void OnDialogueFinished()
        // {
        //     // Invoke the callback to signal that this dialogue is finished
        //     dialogueFinishedCallback?.Invoke();
        // }

        // // Set the callback for the finished event
        // public void SetDialogueFinishedCallback(System.Action callback)
        // {
        //     dialogueFinishedCallback = callback;
        // }

        private void Update()
        {
            // Check if mouse button is pressed while text is being written
            if (Input.GetMouseButtonDown(0))
            {
                displayImmediately = true; // Set flag to display text immediately
            }
        }
        
    }

}

