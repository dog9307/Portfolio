using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;
using Michsky.UI.Dark;

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

    [SerializeField]
    private FadingUI _cameraActionFading;

    [SerializeField]
    private ItemGainPopup _itemPopup;

    [SerializeField]
    private BlurManager _blur;

    [SerializeField]
    private FadingUI _dialogueBoxUI;
    [SerializeField]
    private FadingUI _portraitFading;

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
    public void PortraitInitialSetting(/*string leftCharacter, string leftFace,*/ string rightCharacter, string rightFace)
    {
        _portrait.InitialSetting(/*leftCharacter, leftFace, */rightCharacter, rightFace);
    }

    [YarnCommand("PortraitChange")]
    public void PortraitChange(string characterName, string face/*, bool isLeft = false*/)
    {
        _portrait.PortraitChange(characterName, face/*, isLeft*/);
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

    private Queue<string> _dialogueQueue = new Queue<string>();
    [YarnCommand("AddNextDialogue")]
    public void AddNextDialogue(string dialogueName)
    {
        if (dialogueName == null) return;

        _dialogueQueue.Enqueue(dialogueName);
    }

    [SerializeField]
    private Animator _roomCameraActionAnim;
    [YarnCommand("DialogueCameraAction")]
    public void DialogueCameraAction(string actionName)
    {
        if (!_roomCameraActionAnim) return;
        if (actionName == null) return;

        _blur.BlurOutAnim();

        _roomCameraActionAnim.ForceStateNormalizedTime(0.0f);
        _roomCameraActionAnim.Play(actionName);

        _isPrevCameraAction = true;
        _cameraActionFading?.StartFading(0.0f, 0.25f);

        _portraitFading?.StartFading(0.0f);

        if (_dialogueBoxUI)
        {
            _dialogueBoxUI.fadeDuration = _line.fadeOutTime;
            _dialogueBoxUI.StartFading(0.0f);
        }
    }
    public bool isCameraActionRunning
    {
        get
        {
            if (!_roomCameraActionAnim)
                return false;

            if (_roomCameraActionAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f)
                return true;

            return false;
        }
    }
    private bool _isPrevCameraAction = false;

    public void CheckShowChoices()
    {
        StartCoroutine(Check());
    }

#if UNITY_EDITOR
    public bool isAutoSave = false;
#endif

    IEnumerator Check()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        while (isCameraActionRunning)
            yield return null;

        _blur.BlurInAnim();

        if (!_isDialogue)
        {
            if (_dialogueQueue.Count > 0)
                StartDialogue(_dialogueQueue.Dequeue());
            else
            {
                RoomRenderer.instance.ShowChoices();

                _portraitFading?.StartFading(0.0f);

                if (_dialogueBoxUI)
                {
                    _dialogueBoxUI.fadeDuration = _line.fadeOutTime;
                    _dialogueBoxUI.StartFading(0.0f);
                }

#if UNITY_EDITOR
                if (isAutoSave)
#endif
                SavableDataManager.instance?.SaveList();
            }
        }
    }

    [YarnCommand("ItemGain")]
    public void ItemGain(int itemID, bool isItemLose = false)
    {
        StartCoroutine(ItemPopup(itemID, isItemLose));
    }

    IEnumerator ItemPopup(int itemID, bool isItemLose)
    {
        while (!isNext)
            yield return null;

        _itemPopup?.ItemGain(itemID, isItemLose);
    }

    [YarnCommand("ItemJustLose")]
    public void ItemJustLose(int itemID)
    {
        _itemPopup?.ItemJustLose(itemID);
    }

    [YarnCommand("AddSavable")]
    public void AddSavableData(string key)
    {
        SavableNode node = new SavableNode();
        node.key = key;
        node.value = 100;

        SavableDataManager.instance.AddSavableObject(node);
    }

    [YarnCommand("DelaySetting")]
    public void DelaySetting(float delay)
    {
        _delay = delay;
    }
    private float _delay = 0.0f;

    //[YarnCommand("DisposableSignal")]
    //public void DisposableSignal()
    //{
    //    if (!currentEvt) return;

    //    currentEvt.UseDisposable();
    //}

    //[YarnCommand("SaveIntValue")]
    //public void SaveIntValue(string key, int value)
    //{
    //    if (!SavableDataManager.instance) return;

    //    SavableNode node = new SavableNode();
    //    node.key = key;
    //    node.value = value;

    //    SavableDataManager.instance.AddSavableObject(node);
    //}

    public bool isPause { get; set; }
    [YarnCommand("Pause")]
    public void Pause() { isPause = true; }
    public void Resume() { isPause = false; }
    [YarnCommand("PauseTime")]
    public void Pause(float time)
    {
        StartCoroutine(PauseTime(time));
    }
    IEnumerator PauseTime(float time)
    {
        Pause();
        yield return new WaitForSeconds(time);
        Resume();
    }

    private InMemoryVariableStorage _yarnStorage;
    [YarnCommand("SavableCheck")]
    public void SavableCheck(string savableKey, string key)
    {
        if (!_yarnStorage)
            _yarnStorage = FindObjectOfType<InMemoryVariableStorage>();
        if (!_yarnStorage) return;

        _yarnStorage.SetValue(key, (SavableDataManager.instance.FindIntSavableData(savableKey) >= 100));
    }

    void Update()
    {
        if (!_isDialogue) return;
        if (isPause) return;

        ShowNext();
    }

    public void ShowNext()
    {
        if (_isDialogue)
        {
            if (!isNext)
            {
                if (KeyManager.instance.IsOnceKeyDown("dialogue", true) && _line.isRunning)
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
        if (isDialogue) return;

        StartCoroutine(Dialogue(dialogue));
    }

    IEnumerator Dialogue(string dialogue)
    {
        _isDialogue = true;
        _interactManager.SettingUI(false);

        yield return new WaitForSeconds(_delay);
        _delay = 0.0f;

        //PortraitTurnOn(true);

        //if (_currentLineProvider)
        //    _currentLineProvider.textLanguageCode = LocalizationManager.instance.currentLocalCode;

        if (_isPrevCameraAction)
            _cameraActionFading?.StartFading(1.0f, 0.25f);

        if (_dialogueBoxUI)
        {
            _dialogueBoxUI.fadeDuration = _line.fadeInTime;
            _dialogueBoxUI.StartFading(1.0f);
        }
        _portraitFading?.StartFading(1.0f);

        _runner.StartDialogue(dialogue);

        RoomRenderer.instance.CloseChoices();
    }

    public void EndDialogue()
    {
        _isDialogue = false;
        isNext = false;

        //PortraitTurnOn(false);

        _interactManager.SettingUI(true);
        //SettingUI(false);

        foreach (DialogueEndEvent evt in _endEvents)
            evt();
        _endEvents.Clear();

        //FindObjectOfType<CameraBlurController>().StartBlur(1.0f);

        InteractManager.instance.EndDialogue();
        currentEvt = null;

        CheckShowChoices();
    }

    public bool IsExistDialogue(string dialogueName)
    {
        YarnProject proj = _runner.yarnProject;
        foreach (var n in proj.NodeNames)
        {
            if (n == dialogueName)
                return true;
        }

        return false;
    }

    public Sprite FindPortrait(string key, string face)
    {
        return _portrait.FindSprite(key, face);
    }

    public UnityEvent onSkip;

    public void SkipDialogue()
    {
        _dialogueQueue.Clear();

        _runner.Stop();

        _runner.onDialogueComplete.Invoke();

        StartCoroutine(Skip());
    }

    IEnumerator Skip()
    {
        yield return new WaitForEndOfFrame();

        onSkip?.Invoke();
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
