using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChapterComponents : MonoBehaviour
{
    public TextMeshProUGUI chapterNumberText;
    public TextMeshProUGUI chapterText;
    public Transform buttonsParent;
    public List<Button> choicesButtons = new List<Button>();
    public Button reloadButton;
    public Button quitButton;
}
