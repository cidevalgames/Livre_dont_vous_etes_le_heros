using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chapter", menuName = "Chapters/New chapter", order = 1)]
[System.Serializable]
public class ChapterSO : ScriptableObject
{
    public int index;
    public bool end = false;
    [TextArea(10, 15)]
    public string text;
    [Header("")]
    public List<Choice> choices = new List<Choice>();

    public Choice CreateChoice()
    {
        return new Choice();
    }
}
