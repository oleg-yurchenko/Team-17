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
        
        protected IEnumerator WriteText(string input, Text textHolder, Color textColor, Font textFont, float delay) // showing letters one by one with a lil delay
        {
            textHolder.color = textColor;
            textHolder.font = textFont;
            // finished = true;
            // delay = 0.1f; // default value for delay

            for(int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                yield return null; // This can sometimes give Unity's UI system more time to update
                yield return new WaitForSeconds(delay); // each letter shows up in the textHolder box 0.1s by 0.1s
            }

            finished = true; // our line is finished when the loop is finished

        }
    }

}