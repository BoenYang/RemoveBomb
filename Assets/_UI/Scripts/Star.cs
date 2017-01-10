using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour
{

    public GameObject DestroyEffect;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Hero")
        {
            GameManager.Ins.AddStar();
            Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            AudioManager.ins.PlayCatchEffect(GameManager.Ins.GotStar - 1);
            Destroy(gameObject);
        }
    }
}
