using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        [Header("Next Scene")]
        [SerializeField] private string nextSceneName; // Serialize the next scene name

        private void Awake()
        {
            StartCoroutine(dialogueSquence());
        }

        private IEnumerator dialogueSquence() // loop all the obj
        {
            // Deactivate(); // when u activate one dialogue line, u should deactivate the others

            StartCoroutine(SkipDialogue()); // Start the skip dialogue coroutine
            
            for(int i = 0; i < transform.childCount; i++)
            {
                Deactivate(); // when u activate one dialogue line, u should deactivate the others

                // GameObject childObject = transform.GetChild(i).gameObject;
                // DialogueLine dialogueLine = childObject.GetComponent<DialogueLine>();

                // // Set up the callback for the finished event
                // dialogueLine.SetDialogueFinishedCallback(ActivateNextDialogue);
                // // Activate the dialogue line
                // dialogueLine.gameObject.SetActive(true);
                transform.GetChild(i).gameObject.SetActive(true); 
                // // Wait until the current dialogue line finishes
                // yield return new WaitUntil(() => dialogueLine.finished);
                yield return new WaitUntil(()=> transform.GetChild(i).GetComponent<DialogueLine>().finished); 

                // transform.GetChild(i).gameObject.SetActive(false); 

                // // tell the dialogue sequence when exactly is the dialogue finished so we can activate the next one

                // if (dialogueLine != null)
                // {
                //     // Set up the callback for the finished event
                //     dialogueLine.SetDialogueFinishedCallback(ActivateNextDialogue);

                //     // Activate the dialogue line
                //     dialogueLine.gameObject.SetActive(true);

                //     // Wait until the current dialogue line finishes
                //     yield return new WaitUntil(() => dialogueLine.finished);
                // }
                // else
                // {
                //     Debug.LogError("DialogueLine component not found on GameObject: " + childObject.name, childObject);
                // }
            }
            // gameObject.SetActive(false); 
            // All dialogues are finished, load the next scene
            LoadNextScene();

        }

        // private void ActivateNextDialogue()
        // {
        //     // Deactivate the current dialogue line
        //     Deactivate();
        // }

        
        // Function to skip dialogue by holding down K for 3 seconds
        private IEnumerator SkipDialogue()
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

            // If the space button is held for 5 seconds, load the next scene
            LoadNextScene();
        }

        
        private void Deactivate()
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                // Debug.LogError("Not Deactivating...???"+ gameObject.activeSelf);
                transform.GetChild(i).gameObject.SetActive(false); 
                // Debug.LogError("Deactivating...???"+ transform.GetChild(i).gameObject.activeSelf);

                // if(gameObject.activeSelf == true)
                // {
                //     transform.GetChild(i).gameObject.SetActive(false); 
                //     Debug.LogError(" 1" + transform.GetChild(i).gameObject.activeSelf);
                // }
                // else
                // {
                //     transform.GetChild(i).gameObject.SetActive(true);
                //     Debug.LogError(" 2" + transform.GetChild(i).gameObject.activeSelf);
                // }
            }
        }

        private void LoadNextScene()
        {
            // Load the next scene
            SceneManager.LoadScene(nextSceneName);
            // if (nextScene.IsValid())
            // {
            //     // Load the next scene using the SceneReference
            //     SceneManager.LoadScene(nextScene.ScenePath);
            // }
            // else
            // {
            //     Debug.LogError("Invalid SceneReference for next scene in DialogueHolder.");
            // }
        }
    }


}
