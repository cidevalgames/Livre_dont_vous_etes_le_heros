using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chapter", menuName = "Chapters/New chapter", order = 1)]
public class Chapter : ScriptableObject
{
    public bool end = false;
    [TextArea(10, 15)]
    public string text;
    [Header("")]
    public List<Choice> choices = new List<Choice>();
}

[System.Serializable]
public class Choice
{
    [TextArea(5, 10)]
    public string text;
    [Min(0)]
    public int index;
}
