using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{
    public float fadeTime = 1;

    public AnimationCurve FadeCure;

    private float timer;

    private bool start = false;

    private Image image;

    private Color c;

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
        gameObject.SetActive(false);
    }

    void Update () {
        if (start)
        {
            timer += Time.deltaTime;
            c = image.color;
            c.a = FadeCure.Evaluate(timer);
            image.color = c;
            if (timer >= fadeTime)
            {
                start = false;
                gameObject.SetActive(false);
                timer = 0;
            }
        }
    }

    public void StartFade()
    {
        start = true;
        timer = 0;
        gameObject.SetActive(true);
    }

}
