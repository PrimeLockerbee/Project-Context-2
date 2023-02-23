using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandStateKeeper : MonoBehaviour
{
    [SerializeField] public int i_SouthIslandState = 0;

    [SerializeField] public Material southmaterial;

    [SerializeField] public GameObject GoodEndCanvas;
    [SerializeField] public GameObject BadEndCanvas;

    [SerializeField] public GameObject QuestCanvas;

    void Start()
    {
        i_SouthIslandState = 0;
    }

    void Update()
    {
        CheckIfDone();
    }

    void CheckIfDone()
    {
        if (i_SouthIslandState < 0)
        {

            StartCoroutine(WaitSecondsBad());

        }
        if (i_SouthIslandState > 0)
        {

            StartCoroutine(WaitSecondsGood());

        }
    }

    IEnumerator WaitSecondsGood()
    {
        yield return new WaitForSeconds(2);

        southmaterial.color = Color.black;

        yield return new WaitForSeconds(5);

        BadEndCanvas.SetActive(true);
    }

    IEnumerator WaitSecondsBad()
    {
        yield return new WaitForSeconds(2);

        southmaterial.color = Color.blue;

        yield return new WaitForSeconds(5);

        GoodEndCanvas.SetActive(true);
    }
}
