using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StoryEditor
{
    public class ProbabilityUI : MonoBehaviour
    {
        [SerializeField] private Button linkButton;
        [SerializeField] private Slider probabilitySlider;

        private bool _initialized = false;

        private void Awake()
        {
            linkButton = GetComponentInChildren<ProbabilityLinkButton>().linkButton;

            Initialize();
        }

        public void Initialize()
        {
            if (_initialized)
                return;

            linkButton.onClick.AddListener(OnClick_Link);

            _initialized = true;
        }

        public int GetProbabilitySliderValue()
        { 
            return (int)probabilitySlider.value;
        }

        #region Button events
        private void OnClick_Link() => FindObjectOfType<LinksManager>().LinkButton(linkButton);
        #endregion
    }
}
