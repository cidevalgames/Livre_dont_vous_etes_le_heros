using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chapter", menuName = "Chapters/New chapter", order = 1)]
[System.Serializable]
public class Chapter : ScriptableObject
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

[System.Serializable]
public class Choice
{
    [TextArea(5, 10)]
    public string text;
    public List<ChoiceProbability> probabilities = new List<ChoiceProbability>();
}

[System.Serializable]
public class ChoiceProbability
{
    [Range(0, 100)]
    public int probability = 100;
    [Min(0)]
    public int index;
}
