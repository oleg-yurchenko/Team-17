using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        protected IEnumerator WriteText(string input, Text textHolder, Color textColor, Font textFont) // showing letters one by one with a lil delay
        {
            textHolder.color = textColor;
            textHolder.font = textFont;

            for(int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                yield return new WaitForSeconds(0.1f); // each letter shows up in the textHolder box 0.1s by 0.1s
            }

        }
    }

}