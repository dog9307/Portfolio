using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _areas;

    private int _currentArea;

    void Start()
    {
        // player 현재 위치 받아오기
        _currentArea = 0;
        UpdateUI();
    }

    void Update()
    {
        int prev = _currentArea;
        if (KeyManager.instance.IsOnceKeyDown("left", true))
            _currentArea = ((_currentArea - 1) + _areas.Length) % _areas.Length;
        if (KeyManager.instance.IsOnceKeyDown("right", true))
            _currentArea = (_currentArea + 1) % _areas.Length;

        if (prev != _currentArea)
            UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < _areas.Length; ++i)
        {
            if (_currentArea == i)
                _areas[i].SetActive(true);
            else
                _areas[i].SetActive(false);
        }
    }
}
