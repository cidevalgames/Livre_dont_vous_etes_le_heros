using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LinksManager : MonoBehaviour
{
    public static LinksManager Instance;

    [SerializeField] GameObject lineRendererPrefab;

    bool isLinking = false;
    GameObject actualLineRenderer = null;
    Vector3 mouseLinkPosition = Vector3.zero;
    Button firstButton;
    Button secondButton;

    private void Awake()
    {
        Instance = this;
    }

    public void LinkButton(Button clickedButton)
    {
        if (!isLinking && clickedButton.CompareTag("OpenLinkButton"))
        {
            LinkedButton linkedButtonToClicked = clickedButton.GetComponent<LinkedButton>();

            if (linkedButtonToClicked.isLinked)
            {
                Destroy(linkedButtonToClicked.lineRenderer);

                linkedButtonToClicked.isLinked = false;
                linkedButtonToClicked.lineRenderer = null;

                LinkedButton clickedButtonLinkedButton = linkedButtonToClicked.linkedButton.GetComponent<LinkedButton>();

                clickedButtonLinkedButton.isLinked = false;
                clickedButtonLinkedButton.lineRenderer = null;
                clickedButtonLinkedButton.linkedButton = null;

                linkedButtonToClicked.linkedButton = null;
                
            }

            actualLineRenderer = Instantiate(lineRendererPrefab);
            actualLineRenderer.transform.position = clickedButton.transform.position;

            actualLineRenderer.GetComponent<LineRendererManager>().OpenLink(clickedButton);

            mouseLinkPosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

            firstButton = clickedButton;

            isLinking = true;
        }
        else if (isLinking && clickedButton.CompareTag("CloseLinkButton"))
        {
            actualLineRenderer.GetComponent<LineRenderer>().SetPosition(1, clickedButton.transform.position);
            actualLineRenderer.GetComponent<LineRendererManager>().CloseLink(clickedButton);

            mouseLinkPosition = Vector3.zero;

            secondButton = clickedButton;

            LinkedButton firstButtonScript = firstButton.GetComponent<LinkedButton>();
            LinkedButton secondButtonScript = secondButton.GetComponent<LinkedButton>();

            firstButtonScript.isLinked = true;
            firstButtonScript.linkedButton = secondButton;
            firstButtonScript.lineRenderer = actualLineRenderer;

            secondButtonScript.isLinked = true;
            secondButtonScript.linkedButton = firstButton;
            secondButtonScript.lineRenderer = actualLineRenderer;

            actualLineRenderer = null;

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
