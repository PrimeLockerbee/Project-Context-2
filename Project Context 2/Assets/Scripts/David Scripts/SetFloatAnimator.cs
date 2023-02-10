using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetFloatAnimator : MonoBehaviour
{
    public Slider slider;   
    Animator m_Animator;


    public void Slider(string String)
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_Animator.SetFloat(String, slider.normalizedValue);
    }
}
