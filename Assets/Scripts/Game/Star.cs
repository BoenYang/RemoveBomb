using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour
{
    public GameObject DestroyEffect;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Hero")
        {
			GameObject destroyGo = Instantiate(DestroyEffect, transform.position, Quaternion.identity) as GameObject;
			Destroy(gameObject);
			Destroy (destroyGo, 2.0f);
            GameScene.Instance.GetGameMode<NormalMode>().GetStar();
        }
    }
}
