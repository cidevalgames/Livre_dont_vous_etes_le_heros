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

        private void Awake()
        {
            linkButton = GetComponentInChildren<ProbabilityLinkButton>().linkButton;
        }

        public void Initialize()
        {
            linkButton.onClick.AddListener(OnClick_Link);
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
