using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    static private InteractManager _instance;
    static public InteractManager instance { get { return _instance; } }
     
    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<InteractManager>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "InteractManager";
                _instance = container.AddComponent<InteractManager>();
            }
        }
    }

    [SerializeField] GameObject _uiObject;
    
    //상호작용중에 다른거 못하게 하려고.
    static bool _isInteract;
    static public bool isInterect { get { return _isInteract; } set { _isInteract = value; } }
    
    DialogueManager _dialogueManager;

    private void Start()
    {
        _dialogueManager = DialogueManager.instance;
        _isCanDialogue = true;
    }

    public void SettingUI(bool flag)
    {
        if (_uiObject)
            _uiObject.SetActive(flag);

        isInterect = !flag;
    }

    IEnumerator CallDialogue(string dialogueName)
    {
        yield return null;
        _dialogueManager.ResetDialogue();
        _dialogueManager.StartDialogue(dialogueName);
    }

    private InGameUIFinder _ingameUI;
    private PlayerHPUIController _hpUI;
    public void StartDialogue(GameObject obj)
    {
        if (!_isCanDialogue) return;

        _isCanDialogue = false;

        InteractionEvent evt = obj.GetComponent<InteractionEvent>();
        if (evt)
        {
            PlayerMoveController player = FindObjectOfType<PlayerMoveController>();
            if (player)
                player.PlayerMoveFreeze(true);

            if (!_ingameUI)
                _ingameUI = FindObjectOfType<InGameUIFinder>();

            if (_ingameUI)
            {
                _ingameUI.isDialogueOn = true;
                _ingameUI.GraphicsOff();
            }

            if (!_hpUI)
                _hpUI = FindObjectOfType<PlayerHPUIController>();

            if (_hpUI)
                _hpUI.UIDisappear();

            FindObjectOfType<CameraBlurController>().StartBlur(50.0f);

            _dialogueManager.currentEvt = evt;
            StartCoroutine(CallDialogue(evt.dialogueName));
        }
    }

    public void EndDialogue()
    {
        if (!_ingameUI)
            _ingameUI = FindObjectOfType<InGameUIFinder>();

        if (_ingameUI)
        {
            _ingameUI.isDialogueOn = false;
            _ingameUI.GraphicsOn();
        }

        if (!_hpUI)
            _hpUI = FindObjectOfType<PlayerHPUIController>();

        if (_hpUI)
            _hpUI.UIAppear();

        StartCoroutine(CoolTime());
    }

    [SerializeField]
    private float _dialogueCoolTime = 0.3f;
    private bool _isCanDialogue;
    IEnumerator CoolTime()
    {
        KeyManager.instance.Disable("TalkTo");
        yield return new WaitForSeconds(_dialogueCoolTime);
        _isCanDialogue = true;
        KeyManager.instance.Enable("TalkTo");
    }
}
