using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMirageUser : ActiveUserBase, IObjectCreator
{
    public GameObject effectPrefab { get; set; }

    public bool isMirageDoubleRemover { get; set; }
    public bool isAdditionalMirage { get; set; }
    public bool isMarkerDestroy { get; set; }
    public bool isAdditionalTracing { get; set; }
    public bool isOtherScaleUp { get; set; }
    public bool isAllBuff { get; set; }
    public bool isForcedCounterAttack { get; set; }

    [SerializeField]
    private ParticleSystem _effect;

    protected override void Start()
    {
        base.Start();

        effectPrefab = Resources.Load<GameObject>("Player/Bullets/Mirage/Prefab/ActiveMirage");

        isMarkerDestroy = true;
    }

    public GameObject CreateObject()
    {
        Transform player = transform.parent.GetComponentInParent<Movable>().transform;
        GameObject newBullet = Instantiate(effectPrefab);
        newBullet.transform.position = player.position;

        return newBullet;
    }

    public override void UseSkill()
    {
        if (_effect)
            _effect.Play();

        GameObject mirage = CreateObject();

        SkillInfo info = FindObjectOfType<PlayerSkillUsage>().GetComponent<SkillInfo>();
        ActiveMirage skill = info.FindSkill<ActiveMirage>();
        MirageController controller = mirage.GetComponent<MirageController>();
        controller.damageMultiplier = skill.damageMultiplier;
        controller.dir = info.GetComponent<LookAtMouse>().dir;
        
        float damageMultiplier = _damageMultiplier;
        if (skill.isReversePowerUp)
        {
            // test
            // 흉내쟁이 반전 강화시 데미지 어케??
            //int figure = (int)info.FindSkill<PassiveReversePower>().figure;
            //     if (figure == 0) damageMultiplier *= 1.2f;
            //else if (figure == 1) damageMultiplier *= 1.4f;
            //else if (figure == 2) damageMultiplier *= 1.6f;
            //else if (figure == 3) damageMultiplier *= 2.0f;
        }
    }

    public void KnockbackAssistantTurnOn(bool isTurnOn)
    {
        PassiveKnockbackIncreaseUser knockbackUser = manager.FindUser(typeof(PassiveKnockbackIncrease)) as PassiveKnockbackIncreaseUser;
        if (!knockbackUser) return;

        knockbackUser.KnockbackAssistantTurnOn(isTurnOn);
    }

    public void AllBuffOn()
    {
        PlayerMoveController player = GameObject.FindObjectOfType<PlayerMoveController>();
        PlayerEffectManager em = player.GetComponentInChildren<PlayerEffectManager>();
        GameObject effect = em.FindEffect("AllBuff");
        if (effect)
        {
            AllBuffEffect allBuff = effect.GetComponent<AllBuffEffect>();
            if (!allBuff) return;

            allBuff.totalTime = 1.0f;
            allBuff.figure = 2.0f;

            if (effect.activeSelf)
                allBuff.BuffOn();
            else
                effect.SetActive(true);
        }
    }
}
