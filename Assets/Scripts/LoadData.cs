using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class LoadData : MonoBehaviour
{
    public List<TextAsset> datas = new List<TextAsset>();
    public List<ChapterSO> chapters = new List<ChapterSO>();

    private void Awake()
    {
        string storyFolder = FindObjectOfType<RunStory>().storyFolder;

        string storyPath = "Assets/ScriptableObjects/_Zelda_"/* + storyFolder*/;

        datas = AssetDatabase.FindAssets("t:TextAsset", new string[] { storyPath })
            .Select(guid => AssetDatabase.LoadAssetAtPath<TextAsset>(AssetDatabase.GUIDToAssetPath(guid)))
            .ToList();

        chapters = AssetDatabase.FindAssets("t:Chapter", new string[] { storyPath })
            .Select(guid => AssetDatabase.LoadAssetAtPath<ChapterSO>(AssetDatabase.GUIDToAssetPath(guid)))
            .ToList();
    }

    private void Start()
    {
        Load(datas, chapters);
    }

    public void Load(List<TextAsset> datas, List<ChapterSO> chapters)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            JsonUtility.FromJsonOverwrite(datas[i].text, chapters[i]);
        }
    }
}
