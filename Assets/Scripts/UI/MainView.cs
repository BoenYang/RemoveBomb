using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainView : UIBase {

	public Button StartBtn;

	public Toggle AudioToggle;

	public override void OnInit ()
	{
		StartBtn.onClick.AddListener (OnStartClick);
		AudioToggle.onValueChanged.AddListener (OnAudioToggleValChange);
	}

	private void OnStartClick(){
		UIManager.OpenPanel ("LevelView",true);
	}

	private void OnAudioToggleValChange(bool val){
		
	}

}
