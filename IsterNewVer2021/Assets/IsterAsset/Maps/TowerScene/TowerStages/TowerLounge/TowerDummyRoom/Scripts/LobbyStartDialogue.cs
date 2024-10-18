using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LobbyStartDialogue : MonoBehaviour
{
    private bool _isDialogueAlreadyStart = false;

    [SerializeField]
    private InteractionEvent _dialogue;

    public UnityEvent onTalkEnd;

    [SerializeField]
    private GameObject _items;
    [SerializeField]
    private Transform _activeItemTutorial;
    [SerializeField]
    private FadingGuideUI _fade;

    //[SerializeField]
    //private GameObject _healItem;
    //[SerializeField]
    //private FadingGuideUI _fade;

    private void Start()
    {
        int playerSeeScript = PlayerPrefs.GetInt("isPlayerAlreadyTalk", 0);

        if (playerSeeScript == 0)
            _isDialogueAlreadyStart = false;

        StageBGMPlayer bgm = FindObjectOfType<StageBGMPlayer>();
        if (bgm)
            bgm.PlayBGM();
    }
    
    public void StartDialogue()
    {
        if (_isDialogueAlreadyStart) return;

        _isDialogueAlreadyStart = true;

        PlayerMoveController currentPlayer = FindObjectOfType<PlayerMoveController>();
        currentPlayer.PlayerMoveFreeze(true);

        //LookAtMouse look = currentPlayer.GetComponent<LookAtMouse>();
        //if (look)
        //    look.dir = Vector2.up;

        if (_dialogue)
        {
            DialogueManager.instance.AddEndEvent(OnTalkEnd);

            _dialogue.StartDialogue();
        }
    }

    public void OnTalkEnd()
    {
        if (onTalkEnd != null)
            onTalkEnd.Invoke();

        Invoke("PlayerUnfreeze", 0.5f);

        int playerSeeScript = PlayerPrefs.GetInt("isPlayerAlreadyTalk", 0);
        playerSeeScript++;
        PlayerPrefs.SetInt("isPlayerAlreadyTalk", playerSeeScript);
    }

    public void PlayerUnfreeze()
    {
        PlayerMoveController player = FindObjectOfType<PlayerMoveController>();
        if (player)
            player.PlayerMoveFreeze(false);
    }

    public void ItemAppear()
    {
        _items.SetActive(true);

        GameObject item = _items;
        if (item)
        {
            item.transform.parent = transform;
            item.transform.localPosition = Vector3.zero;
            item.SetActive(true);

            if (_activeItemTutorial)
            {
                _activeItemTutorial.gameObject.SetActive(true);
                _activeItemTutorial.parent = item.transform.GetChild(0).GetChild(0);

                if (_fade)
                    _fade.StartFading(1.0f, true);
            }
        }
        //if (_fade)
        //{
        //    _fade.StartFading(1.0f);
        //    _fade.transform.parent = _healItem.transform.GetChild(0).GetChild(0);
        //}
    }
}
