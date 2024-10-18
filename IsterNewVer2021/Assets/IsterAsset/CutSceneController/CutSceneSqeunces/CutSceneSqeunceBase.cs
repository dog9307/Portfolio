using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CutSceneSqeunceBase : MonoBehaviour
{
    [SerializeField]
    protected bool _isNextTogether = false;
    public bool isNextTogether { get { return _isNextTogether; } }

    protected CutSceneController _master;
    public bool isSequenceEnd { get; set; }

    protected bool _isDuringSequence;
    public UnityEvent OnPreSequence;
    public UnityEvent OnDuringSequence;
    public UnityEvent OnPostSequence;

    [SerializeField]
    protected float _startDelay = 0.0f;
    [SerializeField]
    protected float _endDelay = 0.0f;

    [SerializeField]
    protected float _sequenceTime = 3.0f;

    [Header("사운드")]
    [SerializeField]
    protected SFXPlayer _sfx;
    protected AudioSource _loop;

    protected PlayerMoveController player { get { return _master.player; } }
    protected LookAtMouse playerLook { get { return _master.player.GetComponent<LookAtMouse>(); } }

    public virtual void StartSequence(CutSceneController master)
    {
        _master = master;
        isSequenceEnd = false;
        StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
        yield return new WaitForSeconds(_startDelay);

        if (OnPreSequence != null)
            OnPreSequence.Invoke();

        _isDuringSequence = true;
        StartCoroutine(DuringSequence());
        while (_isDuringSequence)
        {
            if (OnDuringSequence != null)
                OnDuringSequence.Invoke();

            yield return null;
        }

        yield return new WaitForSeconds(_endDelay);

        EndSequence();
    }

    protected abstract IEnumerator DuringSequence();

    public virtual void EndSequence()
    {
        isSequenceEnd = true;

        if (OnPostSequence != null)
            OnPostSequence.Invoke();
    }

    public void PlaySFX(string name)
    {
        if (_sfx)
            _sfx.PlaySFX(name);
    }

    public void PlayLoopSFX(string name)
    {
        if (_sfx)
            _loop = _sfx.PlayLoopSFX(name);
    }

    public void StopLoopSFX()
    {
        if (_sfx && _loop)
        {
            SoundSystem.instance.StopLoopSFX(_loop);
            _loop = null;
        }
    }

    public void PlayerLookTarget(Transform target)
    {
        PlayerAnimController playerAnim = FindObjectOfType<PlayerAnimController>();
        if (playerAnim)
            playerAnim.CharacterSetDir(CommonFuncs.CalcDir(playerAnim, target), 0.0f);
    }

    public void CameraShake(float figure)
    {
        CameraShakeController.instance.CameraShake(figure);
    }
}
