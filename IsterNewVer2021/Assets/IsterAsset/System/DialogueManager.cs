using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    static private DialogueManager _instance;
    static public DialogueManager instance { get { return _instance; } }

    public delegate void DialogueEndEvent();
    private List<DialogueEndEvent> _endEvents = new List<DialogueEndEvent>();

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<DialogueManager>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "DialogueManager";
                _instance = container.AddComponent<DialogueManager>();
            }
        }
    }

    [HideInInspector]
    [SerializeField]
    private DialogueRunner _runner;
    [HideInInspector]
    [SerializeField]
    private CustomLineView _line;
    [HideInInspector]
    [SerializeField]
    private TextLineProvider _currentLineProvider;

    [HideInInspector]
    [SerializeField]
    private DialoguePortraitManager _portrait;

    private InteractManager _interactManager;

    private bool _isDialogue = false; //대화중인가?
    public bool isDialogue { get { return _isDialogue; } }
    public bool isNext { get; set; }  //특정 키 입력 대기. 엔터등등

    public InteractionEvent currentEvt { get; set; }

    public float minImageScale { get { if (!_portrait) return 0.0f; return _portrait.minImageScale; } }
    public float minImageColor { get { if (!_portrait) return 0.0f; return _portrait.minImageColor; } }

    [HideInInspector]
    [SerializeField]
    private SFXPlayer _sfx;

    void Start()
    {
        _interactManager = InteractManager.instance;

        PortraitTurnOn(false);
    }

    public void AddEndEvent(DialogueEndEvent evt)
    {
        _endEvents.Add(evt);
    }

    public void PortraitTurnOn(bool isTurnOn)
    {
        _portrait.PortraitTurnOn(isTurnOn);
    }

    public void ResetDialogue()
    {
        _portrait.ResetPortrait();
    }

    [YarnCommand("InitialPortraitSetting")]
    public void PortraitInitialSetting(string leftCharacter, string leftFace, string rightCharacter, string rightFace)
    {
        _portrait.InitialSetting(leftCharacter, leftFace, rightCharacter, rightFace);
    }

    [YarnCommand("PortraitChange")]
    public void PortraitChange(string characterName, string face, bool isLeft = false)
    {
        _portrait.PortraitChange(characterName, face, isLeft);
    }

    [YarnCommand("DialogueSFX")]
    public void DialogueSFX(string name)
    {
        if (_sfx)
            _sfx.PlaySFX(name);
    }

    [YarnCommand("DialogueChange")]
    public void DialogueChange(string dialogueName)
    {
        if (!currentEvt) return;

        currentEvt.dialogueName = dialogueName;
    }

    [YarnCommand("DisposableSignal")]
    public void DisposableSignal()
    {
        if (!currentEvt) return;

        currentEvt.UseDisposable();
    }

    [YarnCommand("SaveIntValue")]
    public void SaveIntValue(string key, int value)
    {
        if (!SavableDataManager.instance) return;

        SavableNode node = new SavableNode();
        node.key = key;
        node.value = value;

        SavableDataManager.instance.AddSavableObject(node);
    }

    void Update()
    {
        ShowNext();
    }

    public void ShowNext()
    {
        if (_isDialogue)
        {
            if (!isNext)
            {
                if (KeyManager.instance.IsOnceKeyDown("skip", true) && _line.isRunning)
                    _line.SkipLine();
            }
            else
            {
                //대화 넘기는 키. 설정.
                if (KeyManager.instance.IsOnceKeyDown("dialogue", true))
                    _line.UserRequestedViewAdvancement();
            }
        }
    }

    public void StartDialogue(string dialogue)
    {
        _isDialogue = true;
        _interactManager.SettingUI(false);

        PortraitTurnOn(true);

        if (_currentLineProvider)
            _currentLineProvider.textLanguageCode = LocalizationManager.instance.currentLocalCode;

        _runner.StartDialogue(dialogue);
    }

    public void EndDialogue()
    {
        _isDialogue = false;
        isNext = false;

        PortraitTurnOn(false);

        _interactManager.SettingUI(true);
        //SettingUI(false);

        foreach (DialogueEndEvent evt in _endEvents)
            evt();
        _endEvents.Clear();

        FindObjectOfType<CameraBlurController>().StartBlur(1.0f);

        InteractManager.instance.EndDialogue();
        currentEvt = null;
    }

    //void SettingUI(bool flag)
    //{
    //    _dialogurBar.SetActive(flag);

    //    if(flag)
    //    {
    //        //이름부분이 없을땐 네임 박스 필요 x
    //        if(dialogues[lineCount].name == "")
    //        {
    //            _dialogurNameBar.SetActive(false);
    //        }
    //        else //이름부분이 있으니 네임 박스 필요.
    //        {
    //            _dialogurNameBar.SetActive(true);
    //            _name.text = dialogues[lineCount].name;
    //        }
    //    }
    //    else
    //    {
    //        _dialogurNameBar.SetActive(false);
    //    }
    //}
}
