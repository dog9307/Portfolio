using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPUIController : MonoBehaviour
{
    [SerializeField]
    private PlayerAttacker _attack;
    private bool _prevBattle = true;

    private FadingGuideUI _hpUI;

    private IntHPPanelController _panel;

    //void Start()
    //{
    //    if (!_hpUI)
    //    {
    //        PlayerHPUIFinder finder = FindObjectOfType<PlayerHPUIFinder>();
    //        if (finder)
    //            _hpUI = finder.GetComponent<FadingGuideUI>();
    //    }

    //    UIAppear();
    //}

    void Update()
    {
        //if (!_hpUI)
        //{
        //    PlayerHPUIFinder finder = FindObjectOfType<PlayerHPUIFinder>();
        //    if (finder)
        //        _hpUI = finder.GetComponent<FadingGuideUI>();
        //}

        //if (!_hpUI) return;

        //if (_prevBattle && !_attack.isBattle)
        //    UIDisappear();
        //if (!_prevBattle && _attack.isBattle)
        //    UIAppear();

        //_prevBattle = _attack.isBattle;
    }

    public void UIAppear()
    {
        if (!_panel)
            _panel = FindObjectOfType<IntHPPanelController>();

        if (!_hpUI)
        {
            PlayerHPUIFinder finder = FindObjectOfType<PlayerHPUIFinder>();
            if (finder)
                _hpUI = finder.GetComponent<FadingGuideUI>();
        }

        _panel.UpdateUI();
        _hpUI.StartFading(1.0f / 0.3f);
    }

    public void UIDisappear()
    {
        _hpUI.StartFading(0.0f);
    }

    public void HPChange()
    {
        if (!_hpUI)
            _hpUI = FindObjectOfType<PlayerHPUIFinder>().GetComponent<FadingGuideUI>();

        if (!_hpUI) return;

        UIAppear();

        //if (!_attack.isBattle)
        //{
        //    EventTimer timer = gameObject.AddComponent<EventTimer>();
        //    timer.totalTime = 2.0f;
        //    timer.isEndDestroy = true;

        //    timer.AddEvent(UIDisappear);
        //    timer.TimerStart();
        //}
    }
}
