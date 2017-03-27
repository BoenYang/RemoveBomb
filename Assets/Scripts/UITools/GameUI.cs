using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameUI : MonoBehaviour
{

    public List<ResultStar> Stars; 

    private void Awake()
    {
        GameManager.Ins.OnGotStar += OnGetStar;
        GameManager.Ins.OnGameReset += OnGameRest;
    }


    private void OnGetStar(int index)
    {
        Stars[index-1].ShowStar();
    }

    private void OnGameRest()
    {
        for (int i = 0; i < Stars.Count; i++)
        {
            Stars[i].HideStar();
        }
    }

}
