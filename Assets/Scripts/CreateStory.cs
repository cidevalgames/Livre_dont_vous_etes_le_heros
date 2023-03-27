using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.IO;
using UnityEditor;

public class CreateStory : MonoBehaviour
{
    [SerializeField] string storyName;

    private void Awake()
    {
        Directory.CreateDirectory("Assets/ScriptableObjects/" + storyName);

        InstantiateNewChapter(0);
        InstantiateNewChapter(1);
        InstantiateNewChapter(2);
        InstantiateNewChapter(3);
    }

    public void InstantiateNewChapter(int index)
    {
        Chapter newChapter = ScriptableObject.CreateInstance<Chapter>();

        AssetDatabase.CreateAsset(newChapter, "Assets/ScriptableObjects/" + storyName + "/" + index.ToString() + ".asset");
    }
}
