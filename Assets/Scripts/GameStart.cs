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

	    PlayerInfo.ReadPlayerInfo();
		UIManager.OpenPanel ("MainView");

        Destroy(gameObject);
	}
	
}
