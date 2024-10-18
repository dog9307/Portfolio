using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Calcatz.ArrivalGUI;

public class TabUI : MonoBehaviour
{
    [SerializeField] List<Canvas> _pages;
    public List<Canvas> pages { get { return _pages; } }

    [SerializeField]
    private Canvas _minimapCanvas;

    private int _pageNum;
    private int _beforePageNum;

    private void OnEnable()
    {
        _pageNum = 0;
        _pages[_pageNum].gameObject.SetActive(true);

        PlayerMoveController player = FindObjectOfType<PlayerMoveController>();
        if (!player) return;

        player.PlayerMoveFreeze(true);
    }
    private void OnDisable()
    {
        _pages[_pageNum].gameObject.SetActive(false);

        PlayerMoveController player = FindObjectOfType<PlayerMoveController>();
        if (!player) return;

        player.PlayerMoveFreeze(false);
    }
    void Update()
    {
        if(this.gameObject.activeInHierarchy)
        {
           if(KeyManager.instance.IsOnceKeyDown("ui_page_right", true))
           {
                _beforePageNum = _pageNum;
                _pageNum++;
                if(_pageNum > _pages.Count -1)
                {
                    _pageNum = 0;
                }
                SetPage();
           }
           else if(KeyManager.instance.IsOnceKeyDown("ui_page_left", true))
           {
                _beforePageNum = _pageNum;
                _pageNum--;
                if (_pageNum < 0)
                {
                    _pageNum = _pages.Count - 1;
                }
                SetPage();
            }
            else if (KeyManager.instance.IsOnceKeyDown("menu", true) || KeyManager.instance.IsOnceKeyDown("tabUI", true))
            {
                UIManager.instance.tabOn = false;
            }
        }
    }

    public void MinimapOpen()
    {
        for (int i = 0; i < _pages.Count; ++i)
        {
            if (_pages[i] == _minimapCanvas)
            {
                _beforePageNum = _pageNum;
                _pageNum = i;
                SetPage();
                break;
            }
        }
    }

    public void SetPage()
    {
        FadingMenuBase fading = _pages[_beforePageNum].gameObject.GetComponentInChildren<FadingMenuBase>();
        if (fading)
            fading.isShow = false;
        else
            _pages[_beforePageNum].gameObject.SetActive(false);

        fading = _pages[_pageNum].gameObject.GetComponentInChildren<FadingMenuBase>();
        if (fading)
            fading.isShow = true;
        else
            _pages[_pageNum].gameObject.SetActive(true);
    }
}
