using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandStateKeeper : MonoBehaviour
{
    [SerializeField] public int _islandState = 0;
    [SerializeField] public int _questsCompleted = 0;

    [SerializeField] public GameObject endcam;

    [SerializeField] public GameObject EndCanvas;

    void Start()
    {
        _islandState = 0;

    }

    void Update()
    {
       CheckIfDone();
    }

    public void CheckIfDone()
    {
        //endcam.depth = Camera.main.depth + 2;
        
        if (_islandState == -3)
        {
        }
        if (_islandState == 3)
        {
        }


        if (_questsCompleted == 2)
        {
            StartCoroutine(WaitSecondsGood());
        }
    }

    IEnumerator WaitSecondsGood()
    {
        yield return new WaitForSeconds(3);

        endcam.SetActive(true);

        yield return new WaitForSeconds(6);

        EndCanvas.SetActive(true);
    }
}
