using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GoogleMobileAds.Api;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.OurUtils;

public class MainView : UIBase {

	public Button StartBtn;

    public Button ShopBtn;

	public Toggle AudioToggle;

    public Image TittleEn;

    public Image TittleZh;

	private bool musicOn;

    private bool signed;

    private bool signing;

	public override void OnInit ()
	{
        PlayerInfo.ReadPlayerInfo();

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

	            if (success)
	            {
                    GooglePlayTools.OpendSavedGame("AutoSave",DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime,
                        (data) =>
                        {
                            Debug.Log(data);
                            Debug.Log(data.Length);
                            if (data.Length != 0)
                            {
                                PlayerInfo.ReadFromBytes(data);
                            }
                           
                        });
                }
	        });

	    }
	    else
	    {
            Debug.Log("该平台不支持Google Play 登录");
        }


#if EN
       
        TittleEn.gameObject.SetActive(true);
        TittleZh.gameObject.SetActive(false);
#else
        TittleEn.gameObject.SetActive(false);
        TittleZh.gameObject.SetActive(true);
#endif
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
