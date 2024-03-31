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
        public bool displayImmediately = false; 

        
        protected IEnumerator WriteText(string input, Text textHolder, Color textColor, Font textFont, float delayText, float delayBetweenLines = 1.0f) // showing letters one by one with a lil delayText
        {
            textHolder.color = textColor;
            textHolder.font = textFont;
            // finished = true;
            // delayText = 0.1f; // default value for delayText
            bool textFullyWritten = false;
            // bool displayImmediately = false;

            for(int i = 0; i < input.Length; i++)
            {

                // if the mouse click is not detected, then shows the letter one by one 
                // if (!Input.GetMouseButtonDown(0))
                // {
                //     textHolder.text += input[i];
                //     yield return new WaitForSeconds(delayText); // each letter shows up in the textHolder box 0.1s by 0.1s
                // }

                if (displayImmediately)
                {
                    textHolder.text = input; // Set the entire line immediately
                    textFullyWritten = true;
                    break;
                }

                textHolder.text += input[i];
                yield return new WaitForSeconds(delayText);

                // Check if mouse button is pressed while text is being written
                // if (Input.GetMouseButtonDown(0))
                // {
                //     displayImmediately = true; // Set flag to display text immediately
                // }


                // textHolder.text += input[i];
                // yield return null; // This can sometimes give Unity's UI system more time to update
                // yield return new WaitForSeconds(delayText); // each letter shows up in the textHolder box 0.1s by 0.1s
                
                // Check if mouse button is pressed while text is being written
                // if (Input.GetMouseButtonDown(0))
                // {
                //     // Finish writing text immediately
                //     textHolder.text = input;
                //     textFullyWritten = true;
                //     break;
                // }

                // yield return new WaitForSeconds(delayText); // each letter shows up in the textHolder box 0.1s by 0.1s

            }

            // textFullyWritten = true;

            // Wait for mouse click to proceed if text is still being written
            while (!Input.GetMouseButtonDown(0))
            {
                yield return null;
            }

            // If text has finished writing, wait for another mouse click to proceed to the next dialogue line
            // if (!textFullyWritten)
            // {
            //     yield return new WaitForSeconds(delayBetweenLines); // Wait for delay between lines
            //     finished = true;
            //     yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            // }

            // yield return new WaitForSeconds(delayBetweenLines); // some time between lines
            yield return new WaitUntil(() => Input.GetMouseButton(0)); // player has to click their mouse to move forward to the next line

            finished = true; // our line is finished when the loop is finished

            // If text has finished writing, wait for another mouse click to proceed to the next dialogue line
            // while (textFullyWritten)
            // {
            //     // yield return new WaitForSeconds(delayBetweenLines); // Wait for delay between lines
            //     finished = true;
            //     yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            // }
        }


        // Function to skip dialogue by holding down Tab for 3 seconds
        protected IEnumerator SkipDialogue()
        {
            float holdTime = 0f;
            while (holdTime < 3f)
            {
                if (Input.GetKey(KeyCode.Tab))
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