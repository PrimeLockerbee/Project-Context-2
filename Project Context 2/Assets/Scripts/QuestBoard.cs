using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBoard : MonoBehaviour
{
    [SerializeField] GameObject QuestPanel;

    private void Start()
    {
        QuestPanel.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            QuestPanel.SetActive(true);
            Time.timeScale = 0.001f;
        }
    }

    public void ResumeGame()
    {
        QuestPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
