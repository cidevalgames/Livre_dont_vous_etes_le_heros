using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace StoryEditor
{
    public class LinksManager : MonoBehaviour
    {
        public static LinksManager Instance;

        [SerializeField] GameObject lineRendererPrefab;

        private bool _isLinking = false;
        private GameObject _currentLineRenderer = null;
        private Vector3 _mouseLinkPosition = Vector3.zero;
        private Button _firstButton;
        private Button _secondButton;

        private void Awake()
        {
            if (!Instance)
                Instance = this;
        }

        public void LinkButton(Button clickedButton)
        {
            if (!_isLinking && clickedButton.CompareTag("OpenLinkButton"))
            {
                LinkedButton linkedButtonToClicked = clickedButton.GetComponent<LinkedButton>();

                if (linkedButtonToClicked.IsLinked)
                {
                    Destroy(linkedButtonToClicked.LineRenderer);

                    linkedButtonToClicked.IsLinked = false;
                    linkedButtonToClicked.LineRenderer = null;

                    LinkedButton clickedButtonLinkedButton = linkedButtonToClicked.LinkButton.GetComponent<LinkedButton>();

                    clickedButtonLinkedButton.IsLinked = false;
                    clickedButtonLinkedButton.LineRenderer = null;
                    clickedButtonLinkedButton.LinkButton = null;

                    linkedButtonToClicked.LinkButton = null;
                }

                _currentLineRenderer = Instantiate(lineRendererPrefab);
                _currentLineRenderer.transform.position = clickedButton.transform.position;

                _currentLineRenderer.GetComponent<LineRendererHandler>().OpenLink(clickedButton);

                _mouseLinkPosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

                _firstButton = clickedButton;

                _isLinking = true;
            }
            else if (_isLinking && clickedButton.CompareTag("CloseLinkButton"))
            {
                _currentLineRenderer.GetComponent<LineRenderer>().SetPosition(1, clickedButton.transform.position);
                _currentLineRenderer.GetComponent<LineRendererHandler>().CloseLink(clickedButton);

                _mouseLinkPosition = Vector3.zero;

                _secondButton = clickedButton;

                LinkedButton firstButtonScript = _firstButton.GetComponent<LinkedButton>();

                firstButtonScript.IsLinked = true;
                firstButtonScript.LinkButton = _secondButton;
                firstButtonScript.LineRenderer = _currentLineRenderer;

                LinkedButton secondButtonScript = _secondButton.GetComponent<LinkedButton>();

                secondButtonScript.IsLinked = true;
                secondButtonScript.LinkButton = _firstButton;
                secondButtonScript.LineRenderer = _currentLineRenderer;

                _currentLineRenderer = null;

                _isLinking = false;
            }
        }

        public void OnBreakLink(InputAction.CallbackContext callback)
        {
            if (callback.performed && _isLinking)
            {
                Destroy(_currentLineRenderer);
                _currentLineRenderer = null;
                _isLinking = false;
            }
        }

        private void Update()
        {
            if (_isLinking)
            {
                _currentLineRenderer.GetComponent<LineRenderer>().SetPosition(1, _mouseLinkPosition);

                _mouseLinkPosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            }
        }
    }
}
