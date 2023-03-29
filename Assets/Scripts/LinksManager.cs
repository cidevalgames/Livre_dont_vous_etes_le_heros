using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LinksManager : MonoBehaviour
{
    [SerializeField] GameObject lineRendererPrefab;

    bool isLinking = false;
    GameObject actualLineRenderer = null;
    Vector3 mouseLinkPosition = Vector3.zero;

    public void LinkButton(Button clickedButton)
    {
        if (!isLinking)
        {
            actualLineRenderer = Instantiate(lineRendererPrefab);
            actualLineRenderer.transform.position = clickedButton.transform.position;

            actualLineRenderer.GetComponent<LineRendererManager>().OpenLink(clickedButton);

            mouseLinkPosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

            isLinking = true;
        }
        else
        {
            actualLineRenderer.GetComponent<LineRenderer>().SetPosition(1, clickedButton.transform.position);
            actualLineRenderer.GetComponent<LineRendererManager>().CloseLink(clickedButton);
            actualLineRenderer = null;

            mouseLinkPosition = Vector3.zero;

            isLinking = false;
        }
    }

    public void OnBreakLink(InputAction.CallbackContext callback)
    {
        if (callback.performed && isLinking)
        {
            Destroy(actualLineRenderer);
            actualLineRenderer = null;
            isLinking = false;
        }
    }

    private void FixedUpdate()
    {
        if (isLinking)
        {
            actualLineRenderer.GetComponent<LineRenderer>().SetPosition(1, mouseLinkPosition);

            mouseLinkPosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
    }
}
