using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRMeter : MonoBehaviour
{
    [SerializeField] List<GameObject> blips;
    [SerializeField] int ActiveBlips;
    int targetblips;
    public void IncreasePR(int Amount)
    {
        ActiveBlips += Amount;
        InitBlips();
    }

    void Start()
    {
        InitBlips();
    }

    void InitBlips()
    {
        for(int i = 0; i < blips.Count; i++)
        {
            if (i < ActiveBlips)
            {
                blips[i].SetActive(true);
            }else
            {
                blips[i].SetActive(false);
            }
        }
    }
}
