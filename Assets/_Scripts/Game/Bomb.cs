using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    public GameObject DestroyEffect;

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.collider.tag == "Hero")
        {
            Debug.Log("Hero collision");
            Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
