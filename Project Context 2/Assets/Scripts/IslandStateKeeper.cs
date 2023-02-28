using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandStateKeeper : MonoBehaviour
{
    [SerializeField] public int i_SouthIslandState = 0;

    [SerializeField] public Camera endcam;


    [SerializeField] public Renderer waterMaterial;
    [SerializeField] public Material badMaterial;
    [SerializeField] public Material goodMaterial;

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

    public void CheckIfDone()
    {
        //endcam.depth = Camera.main.depth + 2;
        
        if (i_SouthIslandState == -2)
        {
            //endcam.depth = Camera.main.depth + 2;
            //StartCoroutine(WaitSecondsBad());

        }
        if (i_SouthIslandState == 2)
        {
           // endcam.depth = Camera.main.depth + 2;
            //StartCoroutine(WaitSecondsGood());

        }
        
    }

    IEnumerator WaitSecondsGood()
    {
        yield return new WaitForSeconds(4);

        waterMaterial.GetComponent<Renderer>().material = goodMaterial;


        yield return new WaitForSeconds(6);

        GoodEndCanvas.SetActive(true);
    }

    IEnumerator WaitSecondsBad()
    {
        yield return new WaitForSeconds(4);

        waterMaterial.GetComponent<Renderer>().material = badMaterial;


        yield return new WaitForSeconds(6);

        BadEndCanvas.SetActive(true);
    }
}
