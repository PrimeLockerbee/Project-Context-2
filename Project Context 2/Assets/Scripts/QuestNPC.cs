using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestNPC : MonoBehaviour
{
    public string npcName = "QuestNPC";
    public string[] dialogue;

    public GameObject dialogueContainer;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private int dialogueIndex = 0;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            // Show dialogue options when player enters the NPC's trigger area
            ShowDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Hide dialogue options when player leaves the NPC's trigger area
            HideDialogue();
        }
    }

    public void ShowDialogue()
    {
        // Show the NPC's name and current dialogue on the screen
        dialogueIndex = 0;
        UpdateDialogue();
        dialogueContainer.SetActive(true);
    }

    public void HideDialogue()
    {
        // Hide the NPC's name and dialogue from the screen
        dialogueContainer.SetActive(false);
    }

    public void NextDialogue()
    {
        // Move to the next dialogue
        dialogueIndex++;

        // If there is no more dialogue, hide the dialogue options
        if (dialogueIndex >= dialogue.Length)
        {
            HideDialogue();
        }
        else
        {
            UpdateDialogue();
        }
    }

    private void UpdateDialogue()
    {
        // Display the current dialogue on the screen
        nameText.text = npcName;
        dialogueText.text = dialogue[dialogueIndex];
    }
}
