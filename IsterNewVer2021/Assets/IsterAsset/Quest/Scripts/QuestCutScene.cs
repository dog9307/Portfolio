using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCutScene : MonoBehaviour
{
    [SerializeField]
    private QuestBoardTalkFrom _talkFrom;
    [SerializeField]
    private GlowableObject _statueGlow;
    [SerializeField]
    private GlowUpAndDown _statueGlowEffect;
    [SerializeField]
    private AnimationCurve _glowCurve;
    [SerializeField]
    private float _glowTime = 2.0f;

    private PlayerMoveController _player;

    [SerializeField]
    private TutorialToggle _toggleOn;
    [SerializeField]
    private TutorialToggle _toggleOff;

    public bool isAlreadyStart { get; set; }

    void Start()
    {
        int count = PlayerPrefs.GetInt("questOver", 0);
        if (count >= 100)
        {
            Destroy(gameObject);
            return;
        }

        _player = FindObjectOfType<PlayerMoveController>();
        _statueGlow.SetIntensity(0.0f);
        _statueGlowEffect.enabled = false;

        _toggleOn.gameObject.SetActive(false);
        _toggleOff.gameObject.SetActive(false);

        isAlreadyStart = false;
    }

    public void StartCutScene()
    {
        if (isAlreadyStart) return;
        isAlreadyStart = true;

        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        _player.GetComponent<LookAtMouse>().dir = CommonFuncs.CalcDir(_player, transform);
        _player.PlayerMoveFreeze(true);

        StartCoroutine(CutScene());
    }

    IEnumerator CutScene()
    {
        float currentTime = 0.0f;
        while (currentTime < _glowTime)
        {
            float ratio = currentTime / _glowTime;
            float intensity = _glowCurve.Evaluate(ratio);

            _statueGlow.SetIntensity(intensity);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        _statueGlow.SetIntensity(_glowCurve.Evaluate(1.0f));

        _statueGlowEffect.enabled = true;

        _talkFrom.BoardOpen();

        StartCoroutine(WaitQuestOff());
    }

    IEnumerator WaitQuestOff()
    {
        yield return new WaitForEndOfFrame();

        QuestBoardUIController board = FindObjectOfType<QuestBoardUIController>();
        while (board.isOn)
            yield return null;

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        _toggleOn.gameObject.SetActive(true);

        yield return new WaitForEndOfFrame();
        _toggleOn.StartToggle();

        PlayerPrefs.SetInt("questOver", 100);
    }
}
