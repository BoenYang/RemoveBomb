using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

	void Start () {
        PlayerInfo.ReadPlayerInfo();
		UIManager.OpenPanel ("MainView");
	}
	
}
