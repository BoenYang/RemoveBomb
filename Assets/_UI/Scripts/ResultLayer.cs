using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResultLayer : MonoBehaviour
{
    public List<ResultStar> Stars;

    public float ShowInterval = 0.7f;

    private void OnEnable()
    {
        for (int i = 0; i < Stars.Count; i++)
        {
            Stars[i].HideStar();
        }
        StartCoroutine(ShowStars());
    }

    private IEnumerator ShowStars()
    {
        yield return new WaitForSeconds(ShowInterval);
        for (int i = 0; i < GameManager.Ins.GotStar; i++)
        {
            Stars[i].ShowStar();
            yield return new WaitForSeconds(ShowInterval);
        }
    }

}
