using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdderSubstracter : MonoBehaviour
{
    public void AddToState(int num)
    {
        var add = GameObject.Find("GameManager").GetComponent<IslandStateKeeper>();
        add._islandState += num;
    }

    public void SubstractFromState(int num)
    {
        var add = GameObject.Find("GameManager").GetComponent<IslandStateKeeper>();
        add._islandState -= num;
    }

    public void AddToCompletedQuests()
    {
        var add = GameObject.Find("GameManager").GetComponent<IslandStateKeeper>();
        add._questsCompleted += 1;
    }
}
