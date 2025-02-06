using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkedButton : MonoBehaviour
{
    private bool _isLinked = false;
    private Button _linkButton;
    private GameObject _lineRenderer;

    public bool IsLinked { get { return _isLinked; } set { _isLinked = value; } }
    public Button LinkButton { get { return _linkButton; } set { _linkButton = value; } }
    public GameObject LineRenderer { get { return _lineRenderer; } set { _lineRenderer = value; } }
}
