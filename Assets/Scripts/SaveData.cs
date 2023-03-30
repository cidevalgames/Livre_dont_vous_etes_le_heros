using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEngine.SocialPlatforms.Impl;
using System.Security.Cryptography.X509Certificates;

public class SaveData : MonoBehaviour
{
    public string storyFolder;

    List<Chapter> story = new List<Chapter>();
    string storyPath;

    public void SaveChapters()
    {
        storyFolder = FindObjectOfType<CreateStory>().storyName;

        storyPath = "Assets/ScriptableObjects/" + storyFolder;

        story = AssetDatabase.FindAssets("t:Chapter", new string[] { storyPath })
            .Select(guid => AssetDatabase.LoadAssetAtPath<Chapter>(AssetDatabase.GUIDToAssetPath(guid)))
            .ToList();

        for (int i = 0; i < story.Count; i++)
        {
            SaveIntoJson(story[i]);
        }
    }

    public void SaveIntoJson(Chapter _chapter)
    {
        string chapter = JsonUtility.ToJson(_chapter);
        System.IO.File.WriteAllText("Assets/ScriptableObjects/" + storyFolder + "/" + _chapter.name + ".json", chapter);
    }
}