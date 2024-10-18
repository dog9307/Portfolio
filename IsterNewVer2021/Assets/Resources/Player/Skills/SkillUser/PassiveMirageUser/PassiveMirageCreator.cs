using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMirageCreator : MonoBehaviour, IObjectCreator
{
    public PassiveMirageUser owner { get; set; }
    public GameObject effectPrefab { get; set; }

    private BuffInfo _buff;

    [SerializeField]
    private float _delayTime = 1.0f;
    public float delayTime { get { return _delayTime; } set { _delayTime = value; } }
    public float timeDecrease { get; set; }

    public bool isOneMore { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        effectPrefab = Resources.Load<GameObject>("Player/Bullets/Mirage/Prefab/PassiveMirage");

        _buff = FindObjectOfType<PlayerMoveController>().GetComponent<BuffInfo>();

        EventTimer timer = GetComponent<EventTimer>();
        timer.AddEvent(CreateMirage);
        timer.isEndDestroy = false;
        timer.totalTime = (_delayTime - timeDecrease);
    }

    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate(effectPrefab);
        newBullet.transform.position = transform.position;

        return newBullet;
    }

    public void CreateMirage()
    {
        GameObject mirage = CreateObject();

        SkillInfo info = FindObjectOfType<PlayerSkillUsage>().GetComponent<SkillInfo>();
        PassiveMirage skill = info.FindSkill<PassiveMirage>();
        PlayerMoveController player = FindObjectOfType<PlayerMoveController>();
        PassiveMirageController controller = mirage.GetComponent<PassiveMirageController>();
        controller.damageMultiplier = (skill.damageMultiplier + owner.damageMultiplier);
        controller.dir = CommonFuncs.CalcDir(transform, player);
        controller.isStartBomb = owner.isStartBomb;
        controller.isEndBomb = owner.isEndBomb;
        controller.additionalSpeed = owner.mirageSpeedIncrease;
        controller.isSkillUse = owner.isSkillUse;

        PassiveMirageTarget target = FindObjectOfType<PassiveMirageTarget>();
        controller.target = target;

        //float damageMultiplier = this.damageMultiplier;
        //if (skill.isReversePowerUp)
        //{
        //    // test
        //     흉내쟁이 반전 강화시 데미지 어케??
        //    int figure = (int)info.FindSkill<PassiveReversePower>().figure;
        //         if (figure == 0) damageMultiplier *= 1.2f;
        //    else if (figure == 1) damageMultiplier *= 1.4f;
        //    else if (figure == 2) damageMultiplier *= 1.6f;
        //    else if (figure == 3) damageMultiplier *= 2.0f;
        //}

        float additionalDamage = owner.additionalDamage;

        if (_buff)
            additionalDamage += _buff.additionalSkillDamage;

        Damager damager = mirage.GetComponentInChildren<Damager>();
        damager.damage = DamageCreator.Create(damager.gameObject, skill.damage, additionalDamage, 1.0f, 1.0f);

        if (isOneMore)
        {
            EventTimer timer = GetComponent<EventTimer>();
            timer.AddEvent(CreateMirage);
            timer.isEndDestroy = false;
            timer.totalTime = (_delayTime - timeDecrease);

            isOneMore = false;
        }
        else
            Destroy();
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
