using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

	void Start () {

#if Release
        GameObject report = GameObject.Find("Reporter");

	    if (report != null)
	    {
            report.SetActive(false);
        }
#endif


		UIManager.OpenPanel ("MainView");

        Destroy(gameObject);
	}
	
}
