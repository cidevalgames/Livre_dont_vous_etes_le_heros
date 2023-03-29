using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject firstChapterPanelPrefab;
    [SerializeField] GameObject chapterPanelPrefab;
    [SerializeField] Transform canvas;
    [SerializeField] GameObject choicePrefab;
    [SerializeField] GameObject lineRendererPrefab;

    public void AddChapter()
    {
        GameObject chapter = Instantiate(chapterPanelPrefab, canvas);

        chapter.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(() => AddChoice(chapter.transform));
    }

    public void AddChoice(Transform choicesParent)
    {
        GameObject choice = Instantiate(choicePrefab, choicesParent);

        Button linkChoiceButton = choice.transform.GetChild(1).GetChild(1).GetComponent<Button>();

        linkChoiceButton.onClick.AddListener(() => GetComponent<LinksManager>().LinkButton(linkChoiceButton));
    }
}
