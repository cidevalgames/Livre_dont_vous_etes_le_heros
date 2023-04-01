using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineRendererManager : MonoBehaviour
{
    Transform openingButton;
    Transform closingButton;
    LineRenderer lineRenderer;
    bool isLinked = false;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void OpenLink(Button clickedButton)
    {
        lineRenderer.GetComponent<LineRenderer>().positionCount = 2;
        lineRenderer.SetPosition(0, clickedButton.transform.position);
        openingButton = clickedButton.transform;
    }

    public void CloseLink(Button clickedButton)
    {
        lineRenderer.GetComponent<LineRenderer>().SetPosition(1, clickedButton.transform.position);
        closingButton = clickedButton.transform;
        isLinked = true;
    }

    private void FixedUpdate()
    {
        lineRenderer.SetPosition(0, openingButton.position);

        if (isLinked)
        {
            lineRenderer.SetPosition(1, closingButton.position);
        }
    }
}
