using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour
{
    public GameObject DestroyEffect;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Hero")
        {
        
        }
    }
}
