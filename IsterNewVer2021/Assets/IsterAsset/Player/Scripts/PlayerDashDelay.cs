using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashDelay : MonoBehaviour
{
    // test
    // 대시딜레이 컴포넌트를 animator의 attack상태 밑에 붙는 놈으로 수정할까 생각중
    // 짜놓은거 수정하기가 커--찮긴 한데 굳이 DashDelay 애니메이션 상태가 있어야하나 의문

    private PlayerAttacker _attack;
    private PlayerSkillUsage _skill;
    private SkillUserManager _manager;

    [SerializeField] private float _totalDelayTime;
    private float _leftCurrentTime;
    private float _rightCurrentTime;

    public float totalDelayTime { get { return _totalDelayTime; } set { _totalDelayTime = value; } }

    public bool isLeftDelay { get; set; }
    public bool isRightDelay { get; set; }
    public bool isDelay { get { return isLeftDelay || isRightDelay; } }

    void Start()
    {
        _attack = GetComponent<PlayerAttacker>();
        _skill = GetComponent<PlayerSkillUsage>();
        _manager = GetComponentInChildren<SkillUserManager>();
    }

    public void DelayStart()
    {
        _attack.speedMultiplier = 0.0f;
        _attack.AttackEnd();

        AdditionalAttackUser additionalAttack = (AdditionalAttackUser)_manager.FindUser(typeof(AdditionalAttack));
        if (additionalAttack)
        {
            if (additionalAttack.isCorrectHand)
                additionalAttack.UseSkill();

            if (additionalAttack.isSkip)
            {
                additionalAttack.isSkip = false;
                return;
            }
        }

        if (_attack.currentHand == HAND.LEFT)
        {
            isLeftDelay = true;
            _leftCurrentTime = 0.0f;
        }
        else if (_attack.currentHand == HAND.RIGHT)
        {
            isRightDelay = true;
            _rightCurrentTime = 0.0f;
        }
    }

    void Update()
    {
        if (!isLeftDelay && !isRightDelay) return;

        if (isLeftDelay)
        {
            _leftCurrentTime += IsterTimeManager.deltaTime;

            if (IgnoreDelay(HAND.LEFT))
                return;

            if (_leftCurrentTime >= _totalDelayTime && !_attack.isAttacking)
                isLeftDelay = false;
            else
                IgnoreDelay_DiffHand(isRightDelay, HAND.RIGHT);
        }

        if (isRightDelay)
        {
            _rightCurrentTime += IsterTimeManager.deltaTime;

            if (IgnoreDelay(HAND.RIGHT))
                return;

            if (_rightCurrentTime >= _totalDelayTime && !_attack.isAttacking)
                isRightDelay = false;
            else
                IgnoreDelay_DiffHand(isLeftDelay, HAND.LEFT);
        }
    }

    bool IgnoreDelay(HAND hand)
    {
        if (_attack.IsTriggered())
        {
            if (_attack.currentHand == hand)
            {
                _attack.AttackStart();
                return true;
            }
        }

        return false;
    }

    void IgnoreDelay_DiffHand(bool isDelay, HAND hand)
    {
        if (!isDelay)
            IgnoreDelay(hand);
    }
}
