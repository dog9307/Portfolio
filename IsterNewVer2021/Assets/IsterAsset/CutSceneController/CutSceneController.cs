using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

public class CutSceneController : MonoBehaviour
{
    private static CutSceneController currentCutScene;
    public static bool isAnyCutsceneAlreayDoing { get { return currentCutScene; } }

    [SerializeField]
    private List<CutSceneSqeunceBase> _cutScenes;

    private Queue<CutSceneSqeunceBase> _sequences = new Queue<CutSceneSqeunceBase>();

    public UnityEvent OnPreCutScene;
    public UnityEvent OnPostCutScene;

    private PlayerMoveController _player;
    public PlayerMoveController player { get { return _player; } }

    [SerializeField]
    private bool _isPlayerFreezeCutScene = true;
    [SerializeField]
    private bool _isPlayerEndFreezeCutScene = false;

    private bool _isStart = false;
    public bool isStart { get { return _isStart; } }
    private bool _isPause = false;
    public bool isPause { get { return _isPause; } }

    [SerializeField]
    private float _startDelay = 0.5f;
    [SerializeField]
    private float _endDelay = 0.0f;

    [SerializeField]
    private DisposableCutScene _disposable;

    [SerializeField]
    private bool _isDisposableEndInvoke = false;

    // test
    //[SerializeField]
    //private bool _isTestCutScene = false;
    //void Update()
    //{
    //    if (_isTestCutScene)
    //    {
    //        if (Input.GetKeyDown(KeyCode.F12))
    //            StartCutScene();
    //    }
    //}

    [YarnCommand("CutSceneStart")]
    public void StartCutScene()
    {
        if (!_disposable)
            _disposable = GetComponentInParent<DisposableCutScene>();

        if (_disposable)
        {
            if (!_disposable.IsCanSeeCutScene())
            {
                if (_isDisposableEndInvoke)
                {
                    if (OnPostCutScene != null)
                        OnPostCutScene.Invoke();
                }

                return;
            }
        }

        for (int i = 0; i < _cutScenes.Count; ++i)
            _sequences.Enqueue(_cutScenes[i]);

        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        _isStart = true;

        StartCoroutine(CutScene());
    }

    public void PlayerAttack(Transform target)
    {
        PlayerAttacker player = FindObjectOfType<PlayerAttacker>();
        if (target)
            player.GetComponent<LookAtMouse>().dir = CommonFuncs.CalcDir(player, target);

        player.AttackStart();
    }

    public void PlayerItemRemove(int id)
    {
        FindObjectOfType<PlayerInventory>().RemoveRelicItem(id);
    }

    public void CutScenePause()
    {
        _isPause = true;
    }

    public void CutSceneResume()
    {
        _isPause = false;
    }

    bool IsPause()
    {
        return (_isPause && _isStart);
    }

    IEnumerator CutScene()
    {
        while (isAnyCutsceneAlreayDoing)
            yield return null;

        currentCutScene = this;

        if (OnPreCutScene != null)
            OnPreCutScene.Invoke();

        while (!gameObject.activeSelf)
            yield return null;

        yield return new WaitForSeconds(_startDelay);

        if (_sequences.Count > 0)
        {
            CutSceneSqeunceBase currentSequence = _sequences.Dequeue();
            while (currentSequence)
            {
                currentSequence.StartSequence(this);

                while (currentSequence.isNextTogether)
                {
                    currentSequence = _sequences.Dequeue();
                    currentSequence.StartSequence(this);
                }

                while (!currentSequence.isSequenceEnd)
                {
                    _player.PlayerMoveFreeze(_isPlayerFreezeCutScene);

                    while (IsPause())
                        yield return null;

                    yield return null;
                }

                if (_sequences.Count > 0)
                {
                    currentSequence = _sequences.Dequeue();
                    while (!currentSequence.gameObject.activeSelf)
                        currentSequence = _sequences.Dequeue();
                }
                else
                    currentSequence = null;
            }
        }

        yield return new WaitForSeconds(_endDelay);

        currentCutScene = null;

        _player.PlayerMoveFreeze(_isPlayerEndFreezeCutScene);

        _isStart = false;

        if (OnPostCutScene != null)
            OnPostCutScene.Invoke();
    }
}