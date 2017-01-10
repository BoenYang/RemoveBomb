
using System.Collections.Generic;
using System.IO;
using System.Xml;
using CE.iPhone.PList;
using UnityEditor;
using UnityEngine;

public class LevelReader
{
    public static string levelFilePath = "_Level/File";

    [MenuItem("YBW/ClearPlayerInfo")]
    public static void CleanPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("User Data Cleaned");
    }

    [MenuItem("YBW/ReadLevel")]
    public static void ReadLevelFile()
    {
        string path = Application.dataPath;
        var files = Directory.GetFiles(path + @"/" + levelFilePath);
        List<GameObject> levels = new List<GameObject>();
        for (int k = 0; k < files.Length; k++)
        {
            string file = files[k];
            if (!file.EndsWith("meta"))
            {

                int index = file.LastIndexOf(@"\");
                string fileName = file.Substring(index+1);
                int pointIndex = fileName.LastIndexOf(".");
                fileName = fileName.Substring(0,pointIndex);
                var reader = File.OpenRead(file);
                PListRoot root = PListRoot.Load(reader);
                PListDict dict = root.Root as PListDict;
                PListArray array = dict["Level"] as PListArray;
                PListDict metaData = dict["Metadata"] as PListDict;

                GameObject level = new GameObject();
                levels.Add(level);
                Vector3 p = new Vector3(0,0,-10);
                level.transform.position = p;
                level.transform.rotation = Quaternion.identity;
                level.transform.localScale = Vector3.one;
                level.name = fileName;
                EditorUtility.DisplayProgressBar(" 正在读取关卡"+ fileName,"", (k+1)/(float)files.Length);
                for (int i = 0; i < array.Count; i++)
                {
                    
                    PListDict element = array[i] as PListDict;
                    PListString name = element["FileName"] as PListString;
                    int l = name.Value.LastIndexOf("/");
                    string spriteName = name.Value.Substring(l + 1);
                    int point = spriteName.LastIndexOf(".");
                    spriteName = spriteName.Substring(0, point);
                    PListString pos = element["Position"] as PListString;
                    float rotation = 0;
                    if (element.ContainsKey("Rotation"))
                    {
                        PListInteger rot = element["Rotation"] as PListInteger;
                        rotation = rot.Value;

                    }

                    Vector2 position = Vector2.zero;
                    string posStr = pos.Value.Replace("{", "");
                    posStr = posStr.Replace("}", "");
                    string[] posStrs = posStr.Split(',');
                    float x = float.Parse(posStrs[0]);
                    float y = float.Parse(posStrs[1]);
                    position.x = (x - 400)/100;
                    position.y = (y - 640)/100;

                    PListString type = element["Type"] as PListString;
                    Debug.Log(spriteName);
                    GameObject sprite = Resources.Load<GameObject>("elements/" + spriteName);
                    GameObject go = GameObject.Instantiate(sprite);
                  
                    go.transform.parent = level.transform;
                    go.name = spriteName;
                    go.transform.localPosition = new Vector3(position.x,position.y,0);
                    go.transform.localEulerAngles = new Vector3(0,0,rotation);

                    if (type == "star")
                    {
                        go.transform.localScale = Vector3.one*0.3f;
                    }

                }
              
                PrefabUtility.CreatePrefab("Assets/_Level/Resources/level/" + fileName + ".prefab", level);
            }
        }
        EditorUtility.ClearProgressBar();
        AssetDatabase.Refresh();
        foreach (var gameObject in levels)
        {
            GameObject.DestroyImmediate(gameObject);
        }
        
    }
}
