using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBarrier : MonoBehaviour
{
    public Vector3 spawnposition;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = spawnposition;
        }
    }
}
