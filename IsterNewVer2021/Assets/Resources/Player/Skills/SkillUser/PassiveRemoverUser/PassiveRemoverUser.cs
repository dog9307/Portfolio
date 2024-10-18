using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRemoverUser : SkillUser, IObjectCreator
{
    public GameObject effectPrefab { get; set; }
    public float scaleFactor { get; set; }

    public bool isStackDamaging { get; set; }
    public bool isStackHealing { get; set; }
    public bool isStackOn { get { return (isStackDamaging || isStackHealing); } }
    public int stackCount { get; set; }

    public float timeMultiplier { get; set; }

    public bool isDoubleShoot { get; set; }

    public bool isMirageDoubleRemover
    {
        get
        {
            if (!isMirage) return false;

            ActiveMirageUser mirage = FindObjectOfType<PlayerSkillUsage>().GetComponentInChildren<SkillUserManager>().FindUser(typeof(ActiveMirage)) as ActiveMirageUser;
            if (!mirage) return false;

            return mirage.isMirageDoubleRemover;
        }
    }

    protected override void Start()
    {
        base.Start();

        //if (isMirage) return;
        
        Init();
    }

    public override void Init()
    {
        effectPrefab = Resources.Load<GameObject>("Player/Bullets/BulletRemover/Prefab/BulletRemover");

        scaleFactor = (!isMirage ? 1.0f : (FindObjectOfType<PlayerSkillUsage>().GetComponentInChildren<SkillUserManager>().FindUser(typeof(PassiveRemover)) as PassiveRemoverUser).scaleFactor);
        timeMultiplier = (!isMirage ? 1.0f : (FindObjectOfType<PlayerSkillUsage>().GetComponentInChildren<SkillUserManager>().FindUser(typeof(PassiveRemover)) as PassiveRemoverUser).timeMultiplier);
    }

    public GameObject CreateObject()
    {
        //PlayerMoveController player = FindObjectOfType<PlayerMoveController>();
        //NormalBulletCreator creator = player.GetComponentInChildren<NormalBulletCreator>();
        Movable move = transform.parent.GetComponentInParent<Movable>();

        GameObject newBullet = Instantiate(effectPrefab);
        newBullet.transform.position = move.transform.position;

        if (NormalAttackDamager.isXFlip)
        {
            Transform effect = newBullet.GetComponentInChildren<Animator>().transform;
            Vector3 localAngle = effect.localEulerAngles;
            localAngle.x = 180.0f;
            effect.localEulerAngles = localAngle;
        }

        NormalBulletCreator creator = move.GetComponentInChildren<NormalBulletCreator>();
        Vector2 dir = Vector2.right;
        // 방향 잡기 추가
        dir = CommonFuncs.CalcDir(transform.position, creator);

        float angle = CommonFuncs.DirToDegree(dir);

        RemoverController controller = newBullet.GetComponentInChildren<RemoverController>();
        if (controller)
        {
            controller.user = this;
            controller.dir = dir;
        }
        else
            angle += 30.0f;

        newBullet.transform.Rotate(new Vector3(0.0f, 0.0f, angle));
        newBullet.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1.0f);

        return newBullet;
    }

    public override void UseSkill()
    {
        Movable move = transform.parent.GetComponentInParent<Movable>();
        if (isMirageDoubleRemover && !typeof(PassiveMirageController).IsInstanceOfType(move))
        {
            NormalBulletCreator creator = move.GetComponentInChildren<NormalBulletCreator>();
            for (int i = 0; i < 2; ++i)
            {
                GameObject newBullet = Instantiate(effectPrefab);
                newBullet.transform.position = move.transform.position;

                if (NormalAttackDamager.isXFlip)
                {
                    Transform effect = newBullet.GetComponentInChildren<Animator>().transform;
                    Vector3 localAngle = effect.localEulerAngles;
                    localAngle.x = 180.0f;
                    effect.localEulerAngles = localAngle;
                }
                Vector2 dir = Vector2.right;
                // 방향 잡기 추가
                dir = CommonFuncs.CalcDir(transform.position, creator);

                float angle = CommonFuncs.DirToDegree(dir);
                float angleDiff = 30.0f;
                float realAngle = (angle + angleDiff - angleDiff * 2.0f * i);
                float realRad = realAngle * Mathf.Deg2Rad;

                newBullet.transform.RotateAround(move.transform.position, Vector3.forward, angleDiff - angleDiff * 2.0f * i);

                dir = new Vector2(Mathf.Cos(realRad), Mathf.Sin(realRad));
                angle = CommonFuncs.DirToDegree(dir);

                RemoverController controller = newBullet.GetComponentInChildren<RemoverController>();
                if (controller)
                {
                    controller.user = this;
                    controller.dir = dir;

                    if (typeof(RemoverCircularController).IsInstanceOfType(controller))
                    {
                        newBullet.transform.parent = move.transform;
                        newBullet.transform.localPosition = Vector3.zero;
                    }
                }

                newBullet.transform.Rotate(new Vector3(0.0f, 0.0f, angle));
                newBullet.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1.0f);
            }
        }
        else
        {
            GameObject newBullet = CreateObject();

            BulletRemover remover = newBullet.GetComponentInChildren<BulletRemover>();
            if (remover)
                remover.user = this;
        }
    }
}
