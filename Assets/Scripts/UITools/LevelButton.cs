using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour{

    public Text LevelText;

    private Image image;

    private Button button;

	public void Init(){
		image = GetComponent<Image>();
		button = GetComponent<Button>();
        button.onClick.AddListener(OnBtnClick);
    }

	public void UpdateBtnState(int levelIndex,bool locked = true,int star = 0)
	{
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
        SceneManager.LoadScene("GameScene");
    }

}
