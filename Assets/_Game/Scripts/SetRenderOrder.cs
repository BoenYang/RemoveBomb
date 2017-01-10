using UnityEngine;
using System.Collections;

public class SetRenderOrder : MonoBehaviour
{

    public int SortingOrder = 3;

	void Start ()
	{
	    Renderer renderer = GetComponent<Renderer>();
	    if (renderer != null)
	    {
	        renderer.sortingOrder = SortingOrder;
	    }
	}

}
