using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject chapterPanelPrefab;
    [SerializeField] Transform canvas;
    [SerializeField] GameObject choicePrefab;
    [SerializeField] GameObject lineRendererPrefab;

    bool isLinking = false;
    GameObject actualLineRenderer;

    public void AddChapter()
    {
        GameObject chapter = Instantiate(chapterPanelPrefab, canvas);

        chapter.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => AddChoice(chapter.transform));
    }

    public void AddChoice(Transform choicesParent)
    {
        GameObject choice = Instantiate(choicePrefab, choicesParent);

        Button linkChoiceButton = choice.transform.GetChild(1).GetChild(1).GetComponent<Button>();

        linkChoiceButton.onClick.AddListener(() => LinkButton(linkChoiceButton));
    }

    public void LinkButton(Button clickedButton)
    {
        if (!isLinking)
        {
            GameObject lineRenderer = Instantiate(lineRendererPrefab);
            lineRenderer.transform.position = clickedButton.transform.position;

            lineRenderer.GetComponent<LineRendererManager>().OpenLink(clickedButton);
            actualLineRenderer = lineRenderer;

            isLinking = true;
        }
        else
        {
            actualLineRenderer.GetComponent<LineRenderer>().SetPosition(1, clickedButton.transform.position);
            actualLineRenderer.GetComponent<LineRendererManager>().CloseLink(clickedButton);
            isLinking = false;
        }
    }

    private void FixedUpdate()
    {
        if (isLinking)
        {
            actualLineRenderer.GetComponent<LineRenderer>().SetPosition(1, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0));
        }
    }
}
