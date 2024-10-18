using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingIllustButtonManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _panelList;
    private GameObject _currentPanel;

    void Start()
    {
        _currentPanel = null;
    }

    private void OnEnable()
    {
        foreach (var p in _panelList)
            p.gameObject.SetActive(false);

        if (_currentPanel)
            _currentPanel.SetActive(true);
    }

    public void OnClick(GameObject clickedPanel)
    {
        if (_currentPanel)
            _currentPanel.SetActive(false);

        _currentPanel = clickedPanel;

        if (_currentPanel)
            _currentPanel.SetActive(true);
    }
}
