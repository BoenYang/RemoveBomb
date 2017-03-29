using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    public GameObject DestroyEffect;

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.collider.tag == "Hero")
        {
			GameObject destroyGo = Instantiate(DestroyEffect, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
			Destroy (destroyGo, 2.0f);
        }
    }
}
