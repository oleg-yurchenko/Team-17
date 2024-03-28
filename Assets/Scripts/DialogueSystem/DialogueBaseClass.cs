using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        // whether the dialogue is finished or not
        public bool finished{get; private set;} // don want anyone to be able to change it outside
        
        protected IEnumerator WriteText(string input, Text textHolder, Color textColor, Font textFont, float delayText, float delayBetweenLines = 1.0f) // showing letters one by one with a lil delayText
        {
            textHolder.color = textColor;
            textHolder.font = textFont;
            // finished = true;
            // delayText = 0.1f; // default value for delayText

            for(int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                yield return null; // This can sometimes give Unity's UI system more time to update
                yield return new WaitForSeconds(delayText); // each letter shows up in the textHolder box 0.1s by 0.1s
            }

            // yield return new WaitForSeconds(delayBetweenLines); // some time between lines
            yield return new WaitUntil(() => Input.GetMouseButton(0)); // player has to click their mouse to move forward to the next line

            finished = true; // our line is finished when the loop is finished

        }


        // Function to skip dialogue by holding down K for 3 seconds
        protected IEnumerator SkipDialogue()
        {
            float holdTime = 0f;
            while (holdTime < 3f)
            {
                if (Input.GetKey(KeyCode.K))
                {
                    holdTime += Time.deltaTime;
                }
                else
                {
                    holdTime = 0f;
                }
                yield return null;
            }

            // If the space button is held for 5 seconds, set dialogue to finished
            finished = true;
        }
    }

}