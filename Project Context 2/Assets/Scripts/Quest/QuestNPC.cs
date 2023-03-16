using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestNPC : MonoBehaviour
{
    //public string npcName = "QuestNPC";
    //public string[] dialogue;
    //public float dialogueDelay = 3f;

    //public GameObject dialogueContainer;
    //public TextMeshProUGUI nameText;
    //public TextMeshProUGUI dialogueText;

    //private int dialogueIndex = 0;
    //private Coroutine dialogueRoutine;

    public GameObject _canvas;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            // Show dialogue options when player enters the NPC's trigger area
            //ShowDialogue();
            _canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Hide dialogue options when player leaves the NPC's trigger area
            //HideDialogue();
            _canvas.SetActive(false);
        }
    }

    //public void ShowDialogue()
    //{
    //    // Show the NPC's name and current dialogue on the screen
    //    dialogueIndex = 0;
    //    UpdateDialogue();
    //    dialogueContainer.SetActive(true);
    //    dialogueRoutine = StartCoroutine(WaitForDialogue());
    //}

    //public void HideDialogue()
    //{
    //    // Hide the NPC's name and dialogue from the screen
    //    dialogueContainer.SetActive(false);
    //    StopCoroutine(dialogueRoutine);
    //}

    //private IEnumerator WaitForDialogue()
    //{
    //    while (dialogueIndex < dialogue.Length)
    //    {
    //        yield return new WaitForSeconds(dialogueDelay);
    //        NextDialogue();
    //    }
    //} 

    //public void NextDialogue()
    //{
    //    // Move to the next dialogue
    //    dialogueIndex++;

    //    // If there is no more dialogue, hide the dialogue options
    //    if (dialogueIndex >= dialogue.Length)
    //    {
    //        HideDialogue();
    //    }
    //    else
    //    {
    //        UpdateDialogue();
    //    }
    //}

    //private void UpdateDialogue()
    //{
    //    // Display the current dialogue on the screen
    //    nameText.text = npcName;
    //    dialogueText.text = dialogue[dialogueIndex];
    //}
}
