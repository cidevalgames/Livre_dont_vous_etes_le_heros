using TMPro;
using UnityEngine;

namespace StoryEditor
{
    public class ChoiceUI : MonoBehaviour
    {
        [Header("Input fields")]
        [SerializeField] private TMP_InputField chapterTextInputField;

        public string GetChoiceText()
        {
            return chapterTextInputField.text;
        }
    }
}