using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    [SerializeField] public Camera endcam;


    public void end()
    {
        endcam.depth = Camera.main.depth + 2;
    }
}
