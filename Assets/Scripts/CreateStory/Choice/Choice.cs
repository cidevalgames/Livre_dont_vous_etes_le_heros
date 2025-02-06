using StoryEditor;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{
    [TextArea(5, 10)]
    public string text;
    public List<ChoiceProbability> probabilities = new List<ChoiceProbability>();

    private ChoiceUI _choiceUI;
    public ChoiceUI ChoiceUI { get { return _choiceUI; } }

    private void Awake()
    {
        _choiceUI = GetComponent<ChoiceUI>();
    }
}
