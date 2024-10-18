using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDummyRoomTutorialCutScene : MonoBehaviour
{
    private PlayerMoveController _player;

    [SerializeField]
    private BossHPBarController _relativeBossHPBar;

    [SerializeField]
    private float _HPBarTime = 3.0f;

    [SerializeField]
    private LobbyStartDialogue _dialogue;

    private bool _isAlreadyStart = false;

    void Start()
    {
        _player = FindObjectOfType<PlayerMoveController>();
    }

    public void StartCutScene()
    {
        if (_isAlreadyStart) return;

        _player.PlayerMoveFreeze(true);

        KeyManager.instance.Disable("tabUI");
        KeyManager.instance.Disable("menu");

        _isAlreadyStart = true;

        StartCoroutine(CutScene());
    }
    IEnumerator CutScene()
    {
        if (_relativeBossHPBar)
        {
            _relativeBossHPBar.StartBattle();
            while (!_relativeBossHPBar.isBattleStart)
                yield return null;
        }

        yield return new WaitForSeconds(_HPBarTime);

        if (_relativeBossHPBar)
        {
            _relativeBossHPBar.EndBattle();
            while (_relativeBossHPBar.isBattleStart)
                yield return null;
        }

        _dialogue.StartDialogue();

        KeyManager.instance.Enable("tabUI");
        KeyManager.instance.Enable("menu");

        PlayerPrefs.SetInt("DummyroomTutorial", 1);
    }

    void ApplyAlpha(Graphic g, float alpha)
    {
        Color color = g.color;
        color.a = alpha;
        g.color = color;
    }
}
