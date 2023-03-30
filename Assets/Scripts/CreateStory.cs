using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.IO;
using UnityEditor;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class CreateStory : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] Transform chaptersParent;
    public string storyName;

    public List<Transform> chapters = new List<Transform>();

    public void InstantiateNewChapter(int index, bool end, string text, Dictionary<string, int> choices)
    {
        Chapter newChapter = ScriptableObject.CreateInstance<Chapter>();

        AssetDatabase.CreateAsset(newChapter, "Assets/ScriptableObjects/" + storyName + "/" + index.ToString() + ".asset");

        newChapter.end = end;
        newChapter.text = text;

        for (int i = 0; i < choices.Count; i++)
        {
            newChapter.choices.Add(newChapter.CreateChoice());

            newChapter.choices[i].text = choices.ElementAt(i).Key;
            newChapter.choices[i].index = choices.ElementAt(i).Value;
        }
    }

    public void OnCreation()
    {
        storyName = titleText.text;

        if (storyName == null)
        {
            storyName = "New story";
        }

        Directory.CreateDirectory("Assets/ScriptableObjects/" + storyName);
        ChapterStorage actualChapterStorage;

        for (int i = 0; i < chaptersParent.childCount; i++)
        {
            chapters.Add(chaptersParent.GetChild(i));
            actualChapterStorage = chapters[i].GetComponent<ChapterStorage>();
            actualChapterStorage.index = i;
        }


        for (int i = 0; i < chapters.Count; i++)
        {
            actualChapterStorage = chapters[i].GetComponent<ChapterStorage>();

            if (i > 0)
            {
                actualChapterStorage.StoreChapter();
            }
            else
            {
                actualChapterStorage.StoreFirstChapter();
            }

            InstantiateNewChapter(i, actualChapterStorage.end, actualChapterStorage.text, actualChapterStorage.choices);
        }
    }
}
