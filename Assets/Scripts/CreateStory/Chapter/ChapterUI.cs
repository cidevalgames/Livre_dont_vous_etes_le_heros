using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StoryEditor
{
    public class ChapterUI : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button linkButton;
        [SerializeField] private Button addChoiceButton;

        [Header("Input fields")]
        [SerializeField] private TMP_InputField chapterTextInputField;

        private bool _initialized = false;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (_initialized)
                return;

            linkButton.onClick.AddListener(OnClick_Link);
            addChoiceButton.onClick.AddListener(OnClick_AddChoice);

            _initialized = true;
        }

        public string GetChapterText()
        {
            return chapterTextInputField.text;
        }

        #region Button events
        private void OnClick_Link() => LinksManager.Instance.LinkButton(linkButton);

        private void OnClick_AddChoice() => GetComponentInChildren<Choices>().AddChoice();
        #endregion
    }
}
