using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBulletCreator : MonoBehaviour, IObjectCreator
{
    [SerializeField] private GameObject _prefab;
    public GameObject effectPrefab { get { return _prefab; } set { _prefab = value; } }

    private LookAtMouse _look;
    public Vector2 dir
    {
        get
        {
            if (_look)
                return _look.dir;
            else
            {
                Movable move = GetComponentInParent<Movable>();
                if (move)
                    return move.targetVelocity.normalized;
                else
                    return Vector2.right;
            }
        }
    }
    [SerializeField]
    private float _distance;
    public float distance { get { return _distance; } }

    private Movable _move;

    //private PlayerBuff _buff;
    //public bool isTracingArrowOn { get { return _buff.isTracingArrowOn; } set { _buff.isTracingArrowOn = value; } }
    //public bool isPassiveReversePowerOn { get { return _buff.isPassiveReversePowerOn; } set { _buff.isPassiveReversePowerOn = value; } }
    //public bool isCoolTimePassiveOn { get { return _buff.isCoolTimePassiveOn; } set { _buff.isCoolTimePassiveOn = value; } }

    public float damageMultiplier { get; set; }
    private TestPlayerEquipment _playerEquip;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        SkillInfo info = GetComponentInParent<SkillInfo>();
        // 흉내쟁이
        if (typeof(MirageSkillInfo).IsInstanceOfType(info))
            damageMultiplier = transform.parent.GetComponentInParent<MirageController>().damageMultiplier;
        // 플레이어
        else
        {
            _look = GetComponentInParent<LookAtMouse>();

            damageMultiplier = 1.0f;

            _playerEquip = _look.GetComponentInChildren<TestPlayerEquipment>();
        }
        _move = transform.GetComponentInParent<Movable>();
    }

    void Update()
    {
        transform.position = _move.transform.position + (Vector3)dir * _distance;
    }

    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate(_prefab);
        newBullet.transform.position = transform.position;
        
        float angle = CommonFuncs.DirToDegree(dir);
        newBullet.transform.Rotate(new Vector3(0.0f, 0.0f, angle));

        MagicArrowController movable = newBullet.GetComponent<MagicArrowController>();
        if (movable)
            movable.dir = dir;

        NormalAttackDamager damager = newBullet.GetComponentInChildren<NormalAttackDamager>();
        if (damager)
        {
            // NormalBullet 데미지 계산
            float realDamage = damager.damage.realDamage;
            PlayerInfo playerInfo = FindObjectOfType<PlayerInfo>();
            BuffInfo buff = FindObjectOfType<BuffInfo>();
            DebuffInfo debuff = playerInfo.GetComponent<DebuffInfo>();

            Damage newDamage = damager.damage;
            newDamage.damage = playerInfo.attackFigure;
            newDamage.additionalDamage += buff.additionalDamage;
            newDamage.damageMultiplier *= buff.damageMultiplier * debuff.damageDecreaseMultiplier;
            damager.damage = newDamage;

            // NormalBullet의 버프 관련
            SkillUserManager manager = transform.parent.GetComponentInChildren<SkillUserManager>();
            if (!manager) return newBullet;

            damager.skillUserManager = manager;

            //PlayerSkillUsage skill = FindObjectOfType<PlayerSkillUsage>();
            //PlayerAttacker attack = FindObjectOfType<PlayerAttacker>();
            //MirageController controller = GetComponentInParent<MirageController>();
            //HAND currentHand = (_look ? attack.currentHand : controller.currentHand);

            //if (isTracingArrowOn)
            //{
            //    if (skill.GetSkillHand(typeof(TracingArrow)) == currentHand)
            //        damager.isTracingArrowOn = true;
            //}

            //if (isCoolTimePassiveOn)
            //{
            //    if (skill.GetSkillHand(typeof(PassiveCoolTimeDown)) == currentHand)
            //        damager.isCoolTimePassiveOn = true;
            //}

            //if (skill.GetSkillHand(typeof(PassiveReversePower)) == currentHand)
            //    damager.isPassiveReversePowerOn = isPassiveReversePowerOn;
        }

        if (_playerEquip)
        {
            GlowableObject[] glows = newBullet.GetComponentsInChildren<GlowableObject>();
            foreach (var glow in glows)
            {
                if (glow)
                    glow.ApplyColor(_playerEquip.currentAttackColor);
            }
        }

        return newBullet;
    }
}
