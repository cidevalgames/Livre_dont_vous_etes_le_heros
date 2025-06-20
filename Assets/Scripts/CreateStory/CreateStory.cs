using UnityEngine;
using System.IO;
using UnityEditor;
using TMPro;
using System.Linq;

public class CreateStory : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    
    public string storyName;

    public void InstantiateNewChapter(ChapterSO chapter)
    {
        ChapterSO newChapter = ScriptableObject.CreateInstance<ChapterSO>();

        AssetDatabase.CreateAsset(newChapter, "Assets/ScriptableObjects/" + storyName + "/" + chapter.index.ToString() + ".asset");

        newChapter.index = chapter.index;
        newChapter.end = chapter.choices.Count == 0;
        newChapter.text = chapter.text;
        newChapter.choices = chapter.choices;
    }

    public void OnCreation()
    {
        storyName = titleText.text;

        if (storyName == "")
        {
            storyName = "New story";
        }

        Directory.CreateDirectory("Assets/ScriptableObjects/" + storyName);

        foreach (ChapterSO chapter in FindObjectOfType<Chapters>().StoreChapters())
        {
            InstantiateNewChapter(chapter);
        }
    }
}
