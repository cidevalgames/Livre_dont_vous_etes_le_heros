using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProbabilitySlider : MonoBehaviour
{
    public Slider slider;
    [SerializeField] TextMeshProUGUI text;

    public void ChangeSliderText()
    {
        text.text = slider.value.ToString() + " %";
    }
}
