using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.QuestMachine;

public class JournalTrigger : MonoBehaviour
{
    public GameObject _journal;

  void Update()
  {
    if(Input.GetKeyDown(KeyCode.J))
    {
       QuestMachine.GetQuestJournal().ToggleJournalUI();
    }
  }
}
