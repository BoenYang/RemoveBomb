using UnityEngine;
using System.Collections;

public class DestroyDelay : MonoBehaviour
{

    public float Delay;

	// Use this for initialization
	void Start ()
	{
	    Destroy(gameObject, Delay);
	}
}
