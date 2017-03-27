using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager ins;

    public GameObject levelBtn;

    public Sprite[] LevelButtonSprites;

    public Sprite LockSprite;

    public Vector2 StartPoint = new Vector2(-260f,260f);

    public float HorizontalWidth = 170;

    public float VerticalHeight = 150;

    private List<LevelButton> levelBtns = new List<LevelButton>();

    public GameObject levelList;

    public void Awake()
    {
        ins = this;
        InitLevel();
    }

    private void OnEnable()
    {
        for (int i = 0; i < levelBtns.Count; i++)
        {
            LevelButton level = levelBtns[i];
//            if (i < Client.Ins.Player.PassedLevelNumber)
//            {
//                level.Locked = false;
//                level.Star = Client.Ins.Player.LevelStars[i];
//            }
//            else
//            {
//                level.Locked = true;
//            }
            level.UpdateButton();
        }
    }

    public void InitLevel()
    {
//        for (int i = 0; i < Client.Ins.TotalLevel; i++)
//        {
//            int col = i % 4;
//            int row = i / 4;
//            int package = i/16;
//            row = row%4;
//            GameObject go = Instantiate(levelBtn);
//            go.transform.parent = levelList.transform;
//            go.transform.GetComponent<RectTransform>().localPosition = StartPoint + new Vector2(HorizontalWidth * col + 800*package, -VerticalHeight * row);
//            go.transform.localScale = Vector3.one;
//            LevelButton level = go.GetComponent<LevelButton>();
//            level.Level = i + 1;
//            go.transform.name = "Level" + (i + 1);
//
//            if (i < Client.Ins.Player.PassedLevelNumber)
//            {
//                level.Locked = false;
//                level.Star = Client.Ins.Player.LevelStars[i];
//            }
//            else
//            {
//                level.Locked = true;
//            }
//            levelBtns.Add(level);
//        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < levelBtns.Count; i++)
        {
            LevelButton btn = levelBtns[i];
            GameObject go = btn.gameObject;
            levelBtns.Remove(btn);
            DestroyImmediate(go);
        }
        levelBtns.Clear();
    }

}
