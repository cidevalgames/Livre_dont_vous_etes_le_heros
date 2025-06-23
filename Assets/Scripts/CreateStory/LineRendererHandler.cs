using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StoryEditor
{
    public class LineRendererHandler : MonoBehaviour
    {
        private Button _openingButton, _closingButton;

        private Vector2 _openingButtonPosition;
        private Vector2 _closingButtonPosition;
        private LineRenderer _lineRenderer;
        private bool _isLinked = false;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public void OpenLink(Button clickedButton)
        {
            _lineRenderer.GetComponent<LineRenderer>().positionCount = 2;
            _lineRenderer.SetPosition(0, clickedButton.transform.position);

            _openingButton = clickedButton;
        }

        public void CloseLink(Button clickedButton)
        {
            _lineRenderer.GetComponent<LineRenderer>().SetPosition(1, clickedButton.transform.position);
            _closingButton = clickedButton;
            
            _isLinked = true;
        }

        private void Update()
        {
            _openingButtonPosition = _openingButton.transform.position;

            _lineRenderer.SetPosition(0, _openingButtonPosition);

            if (_isLinked)
            {
                _closingButtonPosition = _closingButton.transform.position;

                _lineRenderer.SetPosition(1, _closingButtonPosition);
            }
        }
    }
}
