using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour{

    public int Level = 1;

    public int Star;

    public bool Locked;

    public Text LevelText;

    private Image image;

    private Button button;

	public void Init(){
		image = GetComponent<Image>();
		button = GetComponent<Button>();
	}

	public void UpdateBtnState(int levelIndex,bool locked = true,int star = 0){
		Level = levelIndex;
	}


}
