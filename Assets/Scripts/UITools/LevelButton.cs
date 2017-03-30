using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour{

    public Text LevelText;

    private Image image;

    private Button button;

    private int levelIndex;

	public void Init(){
		image = GetComponent<Image>();
		button = GetComponent<Button>();
        button.onClick.AddListener(OnBtnClick);
    }

	public void UpdateBtnState(int levelIndex)
	{
	    this.levelIndex = levelIndex;
	    bool locked = levelIndex > PlayerInfo.CurrentPlayer.CurrentLevelIndex;
	    int star = PlayerInfo.CurrentPlayer.LevelStars[levelIndex - 1];
	    if (locked)
	    {
	        image.sprite = Resources.Load<Sprite>("Textures/UI/level_lock");
	        LevelText.text = "";
	    }
	    else
	    {
            image.sprite = Resources.Load<Sprite>("Textures/UI/level_star" + star);
            LevelText.text = levelIndex.ToString();
        }
	}

    private void OnBtnClick()
    {
        GlobalMng.GlobalSingleton<AudioMng>().PlaySound(MusicPath.Click);
        PlayerInfo.CurrentPlayer.SelectedLevelIndex = levelIndex;
        SceneManager.LoadScene("GameScene");
    }

}
