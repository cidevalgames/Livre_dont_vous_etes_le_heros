using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StoryEditor
{
    public class ChoiceUI : MonoBehaviour
    {
        [SerializeField] private Button addProbabilityButton;

        [Header("Input fields")]
        [SerializeField] private TMP_InputField chapterTextInputField;

        private bool _initialized = false;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (_initialized)
                return;

            addProbabilityButton.onClick.AddListener(OnClick_AddProbability);

            _initialized = true;
        }

        public string GetChoiceText()
        {
            return chapterTextInputField.text;
        }

        #region Button events
        private void OnClick_AddProbability()
        {
            Probabilities probabilities = GetComponentInChildren<Probabilities>(true);
            probabilities.gameObject.SetActive(true);
            probabilities.AddProbability();
        }
        #endregion
    }
}