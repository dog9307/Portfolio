using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region singleton
    static private UIManager _instance;
    static public UIManager instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<UIManager>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "UIManager";
                _instance = container.AddComponent<UIManager>();
            }
        }
        //DontDestroyOnLoad(UIManager.instance);
    }

    #endregion singleton

    [SerializeField] GameObject _keyInfo;
    [SerializeField] GameObject _tabUI;
    [SerializeField] GameObject _pauseUI;
    [SerializeField] GameObject _dialogueBox;

    private InGameUIFinder _ingameUI;

    private bool _syetemUIOn;
    public bool systemUIOn { get { return _syetemUIOn; } set { _syetemUIOn = value; } }
    private bool _tabOn;
    public bool tabOn { get { return _tabOn; } set { _tabOn = value; } }
    private bool _pauseOn;
    public bool pauseOn
    {
        get { return _pauseOn; }
        set
        {
            _pauseOn = value;
            PlayerStop(value);
        }
    }
    private bool _dialogueOn;
    public bool dialogueOn { get { return _dialogueOn; } set { _dialogueOn = value; } }

    private bool _prevPlayerStop;
    public bool prevPlayerStop { get { return _prevPlayerStop; } }

    void Update()
    {
        //_keyInfoOn = _keyInfo.gameObject.;
        if (!_keyInfo)
        {
            //시스템 UI가 만들어 질 수 있을 때, (다얄로그, Tap, Pause 다 없을때)
            if (IsCanSystemUIOn())
            {
                if (KeyManager.instance.IsOnceKeyDown("tabUI", _prevPlayerStop))
                {
                    tabOn = true;
                }
            }
            SetUIState();
        }
        if (_pauseOn || _tabOn || _dialogueOn)
        {
            PlayerStop(true);
        }
    }
    public void MinimapOpen()
    {
        _tabUI.GetComponent<TabUI>().MinimapOpen();

        tabOn = true;
        SetUIState();
    }
    public void PlayerStop(bool isStop)
    {
        if (isStop == _prevPlayerStop) return;

        if (!isStop)
        {
            TutorialSkillUse skillTuto = FindObjectOfType<TutorialSkillUse>();
            if (skillTuto)
            {
                if (!skillTuto.isAlreadyStart)
                {
                    PlayerSkillUsage playerSkill = FindObjectOfType<PlayerSkillUsage>();
                    if (playerSkill)
                    {
                        int count = 0;
                        foreach (var skill in playerSkill.activeSkills)
                        {
                            if (skill != null)
                            {
                                count++;
                                break;
                            }
                        }

                        if (count >= 1)
                        {
                            skillTuto.StartTutorial();
                            _prevPlayerStop = isStop;
                            return;
                        }
                    }
                }
            }

            TutorialSkillChange skillChangeTuto = FindObjectOfType<TutorialSkillChange>();
            if (skillChangeTuto)
            {
                if (!skillChangeTuto.isAlreadyStart)
                {
                    PlayerSkillUsage playerSkill = FindObjectOfType<PlayerSkillUsage>();
                    if (playerSkill)
                    {
                        int count = 0;
                        foreach (var skill in playerSkill.activeSkills)
                        {
                            if (skill != null)
                                count++;
                        }

                        if (count >= 2)
                        {
                            skillChangeTuto.StartTutorial();
                            _prevPlayerStop = isStop;
                            return;
                        }
                    }
                }
            }
        }

        PlayerMoveController player = FindObjectOfType<PlayerMoveController>();
        if (player)
            player.PlayerMoveFreeze(isStop);

        _prevPlayerStop = isStop;
    }
    //시스템 UI가 만들어 질 수 있을 때, (다얄로그, Tap, Pause 다 없을때)
    public bool IsCanSystemUIOn()
    {
        if (!DialogueOn() && !pauseOn && !tabOn) systemUIOn = true;
        else systemUIOn = false;

        return systemUIOn;
    }
    public bool DialogueOn()
    {
        if (!_dialogueBox) return false;

        if (_dialogueBox.activeSelf) dialogueOn = true;
        else dialogueOn = false;

        if (!_ingameUI)
            _ingameUI = FindObjectOfType<InGameUIFinder>();

        if (_ingameUI)
        {
            if (dialogueOn)
                _ingameUI.GraphicsOff();
            else
                _ingameUI.GraphicsOn();
        }

        return dialogueOn;
    }

    public void SetUIState()
    {
        if (!_tabUI || !_pauseUI) return;

        if (!_ingameUI)
            _ingameUI = FindObjectOfType<InGameUIFinder>();

        bool isGraphicOff = tabOn || pauseOn;
        if (_ingameUI)
        {
            if (!_ingameUI.isDialogueOn)
            {
                if (isGraphicOff)
                    _ingameUI.GraphicsOff();
                else
                    _ingameUI.GraphicsOn();
            }
        }

        _tabUI.SetActive(tabOn);
        _pauseUI.SetActive(pauseOn);
    }
}
