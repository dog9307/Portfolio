using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBoardUIController : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private GameObject _masterPanel;

    private PlayerMoveController _player;

    [SerializeField]
    private FadingGuideUI _fading;

    public bool isOn { get; set; }

    void Start()
    {
        isOn = false;
    }

    void Update()
    {
        if (!_masterPanel.activeSelf) return;

        if (KeyManager.instance.IsOnceKeyDown("menu", true))
            BoardOff();
    }

    public void BoardOn()
    {
        isOn = true;
        BoardController(true);
        StartCoroutine(Fading());
    }

    public void BoardOff()
    {
        isOn = false;
        BoardController(false);
        //_fading.ApplyAlpha(0.0f);
    }

    void BoardController(bool isOn)
    {
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        _masterPanel.SetActive(isOn);

        StartCoroutine(PlayerDelay(isOn));
    }

    IEnumerator Fading()
    {
        yield return new WaitForEndOfFrame();

        //_fading.ApplyAlpha(0.0f);
        _fading.StartFading(1.0f, true);
    }

    IEnumerator PlayerDelay(bool isOn)
    {
        yield return new WaitForEndOfFrame();

        _player.PlayerMoveFreeze(isOn);
    }
}
