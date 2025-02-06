using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StoryEditor
{
    public class LineRendererHandler : MonoBehaviour
    {
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

            _openingButtonPosition = clickedButton.transform.position;
        }

        public void CloseLink(Button clickedButton)
        {
            _lineRenderer.GetComponent<LineRenderer>().SetPosition(1, clickedButton.transform.position);
            _closingButtonPosition = clickedButton.transform.position;
            _isLinked = true;
        }

        private void Update()
        {
            _lineRenderer.SetPosition(0, _openingButtonPosition);

            if (_isLinked)
            {
                _lineRenderer.SetPosition(1, _closingButtonPosition);
            }
        }
    }
}
