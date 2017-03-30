using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainView : UIBase {

	public Button StartBtn;

	public Toggle AudioToggle;

	private bool musicOn;

	public override void OnInit ()
	{
		StartBtn.onClick.AddListener (OnStartClick);
		AudioToggle.onValueChanged.AddListener (OnAudioToggleValChange);

	}

    public override void OnRefresh()
    {
		musicOn = PlayerPrefs.GetInt ("MusicOn",1) == 1;
		AudioToggle.isOn = !musicOn;
		GlobalMng.GlobalSingleton<AudioMng> ().MusicOn = musicOn;
		GlobalMng.GlobalSingleton<AudioMng> ().SoundOn = musicOn;
        GlobalMng.GlobalSingleton<AudioMng>().PlayMusic(MusicPath.Background);
    }

    private void OnStartClick(){
        GlobalMng.GlobalSingleton<AudioMng>().PlaySound(MusicPath.Click);
        UIManager.OpenPanel ("LevelView",true);
	}

	private void OnAudioToggleValChange(bool val){
		
		GlobalMng.GlobalSingleton<AudioMng> ().MusicOn = !val;
		GlobalMng.GlobalSingleton<AudioMng> ().SoundOn = !val;
		musicOn = !val;
		PlayerPrefs.SetInt ("MusicOn", !val ? 1 : 0);
	}

}
