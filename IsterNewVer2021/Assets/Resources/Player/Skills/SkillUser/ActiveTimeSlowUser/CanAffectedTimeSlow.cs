using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CanAffectedTimeSlow : MonoBehaviour
{
    private Animator _anim;

    [SerializeField]
    private bool _isBoss = false;
    public bool isBoss { get { return _isBoss; } set { _isBoss = value; } }

    [SerializeField]
    private bool _isChildrenAffect = true;
    public bool isChildrenAffect { get { return _isChildrenAffect; } set { _isChildrenAffect = value; } }

    [SerializeField]
    private List<GameObject> _exceptList;

    void Start()
    {
        Animator[] animators = GetComponentsInChildren<Animator>();
        _anim = animators[0];

        if (isChildrenAffect)
        {
            for (int i = 1; i < animators.Length; ++i)
            {
                if (_exceptList.Contains(animators[i].gameObject)) continue;

                CanAffectedTimeSlow timeSlow = animators[i].GetComponent<CanAffectedTimeSlow>();
                if (!timeSlow)
                    timeSlow = animators[i].gameObject.AddComponent<CanAffectedTimeSlow>();

                timeSlow.isChildrenAffect = false;
            }
        }
    }

    void Update()
    {
        if (!_anim) return;

        float timeMultiplier = 1.0f;

        if (_isBoss)
            timeMultiplier = IsterTimeManager.bossTimeScale;
        else
            timeMultiplier = IsterTimeManager.enemyTimeScale;

        _anim.SetFloat("timeMultiplier", timeMultiplier);
    }
}
