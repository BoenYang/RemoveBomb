using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GoogleMobileAds.Api;
using GooglePlayGames.OurUtils;

public class MainView : UIBase {

	public Button StartBtn;

    public Button ShopBtn;

	public Toggle AudioToggle;

	private bool musicOn;

    private bool signed;

    private bool signing;

	public override void OnInit ()
	{
		StartBtn.onClick.AddListener (OnStartClick);
		AudioToggle.onValueChanged.AddListener (OnAudioToggleValChange);

	    ShopBtn.interactable = false;

        AdmobTools.Banner.RequestBanner(null,AdPosition.Top);
        AdmobTools.Banner.BannerView.Show();

	    if (PlatformUtils.Supported)
	    {
	        signing = true;
	        GooglePlayTools.Init();
	        GooglePlayTools.Sign((bool success) =>
	        {
	            signed = success;
	            signing = false;
                Debug.Log("登录" + signed);
	        });

	    }
	    else
	    {
            Debug.Log("该平台不支持Google Play 登录");
        }
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
        if (signing)
        {
            return;
        }
        UIManager.OpenPanel ("LevelView",true);
	}

	private void OnAudioToggleValChange(bool val){
		
		GlobalMng.GlobalSingleton<AudioMng> ().MusicOn = !val;
		GlobalMng.GlobalSingleton<AudioMng> ().SoundOn = !val;
		musicOn = !val;
		PlayerPrefs.SetInt ("MusicOn", !val ? 1 : 0);
	}

}
