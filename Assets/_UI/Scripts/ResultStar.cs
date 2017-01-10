using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultStar : MonoBehaviour
{

    public GameObject ShowEffect;

    public Image StarImage;

    [ContextMenu("ShowStar")]
    public void ShowStar()
    {
        if (ShowEffect != null)
        {
            GameObject go = Instantiate(ShowEffect, Vector3.zero, Quaternion.identity) as GameObject;
            go.transform.parent = gameObject.transform;
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
        }
        StarImage.gameObject.SetActive(true);
    }

    public void HideStar()
    {
        StarImage.gameObject.SetActive(false);
    }
}
