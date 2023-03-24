using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JournalOpener : MonoBehaviour
{
   public UnityEvent m_MyEvent;

    void Start()
    {
        if (m_MyEvent == null)
            m_MyEvent = new UnityEvent();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            m_MyEvent.Invoke();
        }
    }
}
