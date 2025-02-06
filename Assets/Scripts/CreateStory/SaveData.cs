using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using TMPro;

public class SaveData : MonoBehaviour
{
    public string storyFolder;

    [SerializeField] GameObject registeringCanvas;

    List<ChapterSO> story = new List<ChapterSO>();
    string storyPath;

    bool registering = false;

    public void SaveChapters()
    {
        if (!registering)
        {
            storyFolder = FindObjectOfType<CreateStory>().storyName;

            storyPath = "Assets/ScriptableObjects/" + storyFolder;

            story = AssetDatabase.FindAssets("t:Chapter", new string[] { storyPath })
                .Select(guid => AssetDatabase.LoadAssetAtPath<ChapterSO>(AssetDatabase.GUIDToAssetPath(guid)))
                .ToList();

            for (int i = 0; i < story.Count; i++)
            {
                SaveIntoJson(story[i]);
            }

            registeringCanvas.SetActive(true);

            registering = true;
        }
    }

    public void SaveIntoJson(ChapterSO _chapter)
    {
        string chapter = JsonUtility.ToJson(_chapter);
        System.IO.File.WriteAllText("Assets/ScriptableObjects/" + storyFolder + "/" + _chapter.name + ".json", chapter);
    }

    private void Update()
    {
        if (registering)
        {
            if (AssetDatabase.FindAssets("t:TextAsset", new string[] { storyPath })
            .Select(guid => AssetDatabase.LoadAssetAtPath<TextAsset>(AssetDatabase.GUIDToAssetPath(guid)))
            .ToList().Count == story.Count)
            {
                StartCoroutine(OnRegistered());

                registering = false;
            }
        }
    }

    IEnumerator OnRegistered()
    {
        TextMeshProUGUI registeringCanvasText = registeringCanvas.GetComponentInChildren<TextMeshProUGUI>();

        registeringCanvasText.text = "Enregistré avec succès";

        yield return new WaitForSeconds(3);

        registeringCanvas.SetActive(false);
        registeringCanvasText.text = "Enregistrement en cours...";
    }
}