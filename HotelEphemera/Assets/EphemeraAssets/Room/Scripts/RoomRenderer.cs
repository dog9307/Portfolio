using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Michsky.UI.Dark;
using TMPro;
using Yarn.Unity;

public class RoomRenderer : MonoBehaviour
{
    #region SINGLETON
    static private RoomRenderer _instance;
    static public RoomRenderer instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<RoomRenderer>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "RoomRenderer";
                _instance = container.AddComponent<RoomRenderer>();
            }
        }
        else
            Destroy(gameObject);
    }
    #endregion

    //[SerializeField]
    //private UIDissolveEffect _dissolve;

    [SerializeField]
    private Image _background;
    [SerializeField]
    private Image _currentNPC;
    private NPCController _npc;
    //[SerializeField]
    //private FadingUI _portraitFading;
    [SerializeField]
    private TextMeshProUGUI _roomName;
    public string roomName { set { _roomName.text = value; } }
    [SerializeField]
    private int _roomNumber = 0;
    public int roomNumber { get { return _roomNumber; } set { _roomNumber = value; } }
    public RoomDatas currentRoom { get; set; }

    [HideInInspector]
    [SerializeField]
    private Sprite _dummyImage;

    [SerializeField]
    private Button _backButton;
    [SerializeField]
    private Button _ianRoomButton;

    [SerializeField]
    private FadingUI _buttonsBackgroundFading;
    //[SerializeField]
    //private FadingUI _buttonsFading;
    [SerializeField]
    private Selectable[] _chioceButtons;
    public Selectable[] choiceButtons { get { return _chioceButtons; } }

    [SerializeField]
    private BlurManager _blur;

    [SerializeField]
    private Animator _backgroundAnim;
    [SerializeField]
    private Animator _roomNameAnim;
    //[SerializeField]
    //private Animator _buttonsAnim;

    [SerializeField]
    private RoomEffectManager _effectManager;

    [SerializeField]
    private GraphicRaycaster[] _raycasters;

    [SerializeField]
    private SFXPlayer _sfx;

    [SerializeField]
    private FadingUI _portraitFading;
    [SerializeField]
    private Image _portrait;

    private int _currentNPCCount;
    public int npcCount
    {
        get
        {
            int count = 0;
            if (ChapterFlowController.instance.partnerNPC)
                count++;
            if (_npc && (_npc != ChapterFlowController.instance.partnerNPC))
                count++;

            return count;
        }
    }
    private int _currentNPCIndex = 0;
    [SerializeField]
    private GameObject _npcChangeButton;

    public void NextNPC()
    {
        if (isChanging) return;

        _currentNPCIndex = (_currentNPCIndex + 1) % _currentNPCCount;
        SelectNPC();
    }

    public void PrevNPC()
    {
        if (isChanging) return;

        _currentNPCIndex = (_currentNPCIndex - 1 + _currentNPCCount) % _currentNPCCount;
        SelectNPC();
    }

    void SelectNPC()
    {
        string face = "normal";
        // npc가 늘어나면 조건이 늘어날 예정
        NPCController targetNPC = null;
        if (_currentNPCIndex == 0)
        {
            targetNPC = _npc;

            if (roomNumber == 10)
                face = "hospital";
            else if (roomNumber == 13)
                face = "rooftop";
            else if (roomNumber == 15)
                face = "silhouette";
        }
        else if (_currentNPCIndex == _currentNPCCount - 1)
            targetNPC = ChapterFlowController.instance.partnerNPC;

        if (targetNPC)
        {
            nextPortraitSprite = DialogueManager.instance.FindPortrait(targetNPC.npcName, face);
            targetNPC.RoomSelectableSetting(isDialogueAllShow);
            _currentShowingNPC = targetNPC;

            PlayNPCChange();
        }
    }

    public bool isDialogueAllShow { get; set; } = false;
    private NPCController _currentShowingNPC;
    public void ShowModeChange(bool mode)
    {
        isDialogueAllShow = mode;
        SavableNode node = new SavableNode();
        node.key = "DialogueAllShow";
        node.value = (isDialogueAllShow ? 100 : 0);
        SavableDataManager.instance.AddSavableObject(node, true);

        _currentShowingNPC?.RoomSelectableSetting(isDialogueAllShow);
    }

    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private bool _isChanging;
    public bool isChanging { get { return _isChanging; } set { _isChanging = value; } }
    public Sprite nextPortraitSprite { get; set; }

    public void PlayNPCChange()
    {
        if (!_anim) return;
        if (!nextPortraitSprite) return;

        _anim.Play("NPCChange");
    }

    public void SpriteBind()
    {
        _portrait.sprite = nextPortraitSprite;
        nextPortraitSprite = null;
    }

    void Start()
    {
        //_dissolve.location = 1.0f;

        RaycastersController(false);
        CloseRoomSelectableButtons();

        _npcChangeButton.SetActive(false);

        if (!_blur)
            _blur = FindObjectOfType<BlurManager>(true);

        int count = SavableDataManager.instance.FindIntSavableData("DialogueAllShow");
        isDialogueAllShow = (count >= 100);
    }

    //void Update()
    //{
    //    if (_currentNPCCount <= 1) return;
    //}

    public void SetRoomRenderer(Sprite background, NPCController npc)
    {
        if (!npc)
            npc = (ChapterFlowController.instance.partnerNPC ? ChapterFlowController.instance.partnerNPC : ChapterFlowController.instance.defaultNPC);

        _background.sprite = background;
        _currentNPC.sprite = (npc == null ? _dummyImage : npc.npcSprite);
        _npc = npc;

        _npc?.RoomSelectableSetting(isDialogueAllShow);
        _currentShowingNPC = _npc;
    }

    public void EffectPlay(string effectName)
    {
        _effectManager?.TurnOnEffect(effectName);
    }
    void RaycastersController(bool isTurnOn)
    {
        foreach (var r in _raycasters)
            r.enabled = isTurnOn;
    }

    [YarnCommand("IanMemoryButtonOpen")]
    public void IanMemoryButtonOpen()
    {
        _ianRoomButton.gameObject.SetActive(true);
    }

    public void RoomOpen()
    {
        //if (!isPresentationSkip)
        //    _dissolve.DissolveIn();
        //else
        //    _dissolve.location = 0;

        _currentNPCIndex = 0;
        _currentNPCCount = npcCount;

        _backgroundAnim?.Play("Panel In");

        int count = SavableDataManager.instance.FindIntSavableData($"Item_{6}_Gain");
        if (count >= 100)
            _ianRoomButton.gameObject.SetActive(roomNumber == 8 || (roomNumber / 10 == 1));
        else
            _ianRoomButton.gameObject.SetActive(false);

        _backButton.gameObject.SetActive(!(roomNumber / 10 == 1));

        _ianRoomButton.interactable = false;
        _backButton.interactable = false;
        RoomManager.instance.ButtonsFreeze();

        StartCoroutine(CheckNPC());

        RaycastersController(true);

        //_portraitFading?.StartFading(1.0f);
    }

    IEnumerator CheckNPC()
    {
        //yield return new WaitForSeconds(1.0f);
        yield return new WaitForEndOfFrame();
        while (_backgroundAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f)
            yield return null;

        MinimapMovableCharacter.instance.MoveEndInterrupt();

        if (_roomNameAnim)
            _roomNameAnim.Play("Panel In");
        yield return new WaitForEndOfFrame();
        while (_roomNameAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f)
            yield return null;

        yield return new WaitForSeconds(1.5f);

        if (_roomNameAnim)
            _roomNameAnim.Play("Panel Out");
        yield return new WaitForEndOfFrame();
        while (_roomNameAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f)
            yield return null;

        //if (_blur)
        //    _blur.BlurInAnim();


        // 인카운터 체크
        StringBuilder builder = new StringBuilder("");
        if (_npc)
            builder.Append(_npc.npcName);
        else
        {
            if (ChapterFlowController.instance)
            {
                if (ChapterFlowController.instance.partnerNPC)
                    builder.Append(ChapterFlowController.instance.partnerNPC.npcName);
                else
                    builder.Append("Matthew");
            }
            else
                builder.Append("Matthew");
        }
        builder.Append($"_{_roomNumber}_Encounter");

        int count = SavableDataManager.instance.FindIntSavableData(builder.ToString());
        if (count < 100)
        {
            string sign = "";
            if (builder.ToString().Contains("Matthew"))
                sign += "X";
            else
                sign += builder[0];
            StringBuilder dialogueName =
                new StringBuilder($"chapter{ChapterFlowController.instance.targetChapter}_{_roomNumber}_{sign}_0");

            if (DialogueManager.instance.IsExistDialogue(dialogueName.ToString()))
            {
                if (dialogueName.ToString() == "chapter0_3_M_0")
                {
                    count = SavableDataManager.instance.FindIntSavableData("LobbyFirstEnter");
                    if (count < 100)
                    {
                        DialogueManager.instance.StartDialogue(dialogueName.ToString());

                        _blur?.BlurInAnim();

                        //_portraitFading?.StartFading(1.0f);
                        _buttonsBackgroundFading?.StartFading(1.0f);

                        DialogueManager.instance.PortraitTurnOn(true);
                    }
                }
                else
                {
                    DialogueManager.instance.StartDialogue(dialogueName.ToString());

                    _blur?.BlurInAnim();

                    //_portraitFading?.StartFading(1.0f);
                    _buttonsBackgroundFading?.StartFading(1.0f);

                    DialogueManager.instance.PortraitTurnOn(true);

                    if (roomNumber == 0)
                        ItemGainPopup.instance.ItemJustLose(4);
                }
            }
        }

        if (!DialogueManager.instance.isDialogue)
        {
            // 대화 자동실행 체크
            if (currentRoom.roomEventInfoes != null)
            {
                foreach (var evt in currentRoom.roomEventInfoes)
                {
                    count = SavableDataManager.instance.FindIntSavableData(evt.savableKey);
                    if (count >= 100) continue;

                    if (evt.condition.IsCanInteraction() && evt.isAutoRun)
                    {
                        DialogueManager.instance.StartDialogue(evt.interact.dialogueName);

                        _blur?.BlurInAnim();

                        _buttonsBackgroundFading?.StartFading(1.0f);

                        DialogueManager.instance.PortraitTurnOn(true);

                        break;
                    }
                }

                if (!DialogueManager.instance.isDialogue)
                    OpenRoomSelectableButtons();
            }
            else
                OpenRoomSelectableButtons();
        }
        else
            OpenRoomSelectableButtons();
    }

    [YarnCommand("RoomClose")]
    public void RoomAutoClose(int index = 0)
    {
        StartCoroutine(CloseDelay(index));
    }
    IEnumerator CloseDelay(int index)
    {
        yield return new WaitForSeconds(0.5f);
        RoomClose(index);
    }

    public void RoomClose(int roomIndex = 0)
    {
        //_dissolve.DissolveOut();

        MinimapManager.instance.OpenMinimap(roomIndex);

        if (_backgroundAnim)
            _backgroundAnim.Play("Panel Out");

        _ianRoomButton.interactable = false;
        _backButton.interactable = false;
        RoomManager.instance.ButtonsUnfreeze();

        CloseRoomSelectableButtons();

        //_portraitFading?.StartFading(0.0f);
        _buttonsBackgroundFading?.StartFading(0.0f);

        _effectManager?.TurnOffEffect();

        RaycastersController(false);
        if (_sfx)
            _sfx.PlaySFX("minimapOpen");

        MinimapManager.instance.currentMinimap.GetComponent<MinimapController>().BgmChange();

        if (roomNumber == 15)
        {
            SoundSystem.instance.StopAmbient(null, 1);
        }
    }

    [YarnCommand("SetNextRoomPortrait")]
    public void SetNextRoomPortrait(string npcName, string face)
    {
        //Sprite targetSprite = DialogueManager.instance.FindPortrait(npcName, face);
        //_nextRoomTargetSprite = targetSprite;
        Sprite targetSprite = DialogueManager.instance.FindPortrait(npcName, face);
        _portrait.sprite = targetSprite;
    }
    //private Sprite _nextRoomTargetSprite;

    public void OpenRoomSelectableButtons()
    {
        ShowChoices();

        string face = "normal";
        if (_currentNPCIndex == 0)
        {
            if (roomNumber == 10)
                face = "hospital";
            else if (roomNumber == 13)
                face = "rooftop";
            else if (roomNumber == 15)
                face = "silhouette";
        }

        //if (_nextRoomTargetSprite)
        //{
        //    _portrait.sprite = _nextRoomTargetSprite;
        //    _nextRoomTargetSprite = null;
        //}
        //else
        //{
            Sprite targetSprite = DialogueManager.instance.FindPortrait((_npc ? _npc.npcName : "Matthew"), face);
            _portrait.sprite = targetSprite;
        //}
        //DialogueManager.instance.PortraitInitialSetting(
        //    (_npc ? _npc.npcName : "Matthew"),
        //    face);
        DialogueManager.instance.PortraitTurnOn(true);
    }

    public void CloseRoomSelectableButtons()
    {
        CloseChoices();

        _blur?.BlurOutAnim();
    }

    public void ShowChoices()
    {
        //    if (_buttonsAnim)
        //        _buttonsAnim.Play("Panel In");

        if (_currentNPCCount >= 2)
        {
            NPCController targetNPC = null;
            if (_currentNPCIndex == 0)
                targetNPC = _npc;
            else if (_currentNPCIndex == _currentNPCCount - 1)
                targetNPC = ChapterFlowController.instance.partnerNPC;

            targetNPC?.RoomSelectableSetting(isDialogueAllShow);
            _currentShowingNPC = targetNPC;
        }
        else
        {
            _npc?.RoomSelectableSetting(isDialogueAllShow);
            _currentShowingNPC = _npc;
        }

        _blur?.BlurInAnim();

        //_buttonsFading?.StartFading(1.0f);
        _buttonsBackgroundFading?.StartFading(1.0f);

        if (_currentNPCCount >= 2)
            _npcChangeButton.SetActive(true);
        else
            _npcChangeButton.SetActive(false);

        _ianRoomButton.interactable = true;
        _backButton.interactable = true;

        // 세이브 데이터에 따라 다르게 처리
        foreach (var c in _chioceButtons)
            c.interactable = true;

        _portraitFading?.StartFading(1.0f);
    }

    public void CloseChoices()
    {
        //if (_buttonsAnim)
        //    _buttonsAnim.Play("Panel Out");

        //_buttonsFading?.StartFading(0.0f);
        _buttonsBackgroundFading?.StartFading(0.0f);

        _portraitFading?.StartFading(0.0f);

        _npcChangeButton.SetActive(false);

        _ianRoomButton.interactable = false;
        _backButton.interactable = false;

        // 세이브 데이터에 따라 다르게 처리
        foreach (var c in _chioceButtons)
                c.interactable = false;
    }
}
