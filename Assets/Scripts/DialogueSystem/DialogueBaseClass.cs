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
        
        protected IEnumerator WriteText(string input, Text textHolder, Color textColor, Font textFont, float delayText, float delayBetweenLines) // showing letters one by one with a lil delayText
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

            yield return new WaitForSeconds(delayBetweenLines); // some time between lines

            finished = true; // our line is finished when the loop is finished

        }
    }

}