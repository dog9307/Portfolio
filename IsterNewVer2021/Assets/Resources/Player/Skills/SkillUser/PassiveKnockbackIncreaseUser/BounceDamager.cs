using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BOUNCE
{
    NONE = -1,
    DAMAGE,
    DEBUFF,
    END
}

public class BounceDamager : MonoBehaviour, IObjectCreator
{
    private Damagable _damagable;

    [SerializeField]
    private Damage _damage;
    public Damage damage { get { return _damage; } set { _damage = value; } }

    public float damageMultiplier { get; set; }
    public bool isSecondKnockback { get; set; }

    public float totalTime { get; set; }
    public float figure { get; set; }
    
    public BOUNCE mode { get; set; }

    public PassiveKnockbackIncreaseUser user { get; set; }

    public bool isSlowBomb { get; set; }
    public GameObject effectPrefab { get; set; }

    void Start()
    {
        _damagable = GetComponent<Damagable>();

        effectPrefab = Resources.Load<GameObject>("Player/Bullets/SlowBomb/SlowBomb");
    }

    void OnDestroy()
    {
        if (isSlowBomb)
        {
            SlowBomb bomb = CreateObject().GetComponentInChildren<SlowBomb>();
            bomb.figure = figure;
            bomb.totalTime = totalTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_damagable) return;

        EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
        switch (mode)
        {
            case BOUNCE.DAMAGE:
                Damagable other = collision.gameObject.GetComponent<Damagable>();
                if (other && enemy)
                {
                    DebuffInfo otherDebuff = other.GetComponent<DebuffInfo>();
                    if (otherDebuff)
                    {
                        if (otherDebuff.isAbnormal)
                            _damage.damageMultiplier = damageMultiplier;
                    }

                    Vector2 dir = Vector2.zero;
                    if (isSecondKnockback)
                    {
                        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
                        _damage.knockbackFigure = rigid.velocity.magnitude;
                        dir = CommonFuncs.CalcDir(this, collision.transform.position);

                        user.target = collision.gameObject;
                        user.UseSkill();
                    }

                    other.HitDamager(_damage, dir);
                }

                _damage.knockbackFigure = 0.0f;
                _damagable.HitDamager(_damage, Vector2.zero);
            break;
                
            case BOUNCE.DEBUFF:
                // test
                // 조건 검색을 다른 식으로 할 수도 있음
                if (enemy)
                {
                    DebuffSlow slow = new DebuffSlow();
                    slow.totalTime = totalTime;

                    DebuffInfo debuffInfo = enemy.GetComponent<DebuffInfo>();
                    debuffInfo.AddDebuff(slow);
                }
            break;
        }
    }

    public GameObject CreateObject()
    {
        GameObject bomb = Instantiate(effectPrefab);
        bomb.transform.position = transform.position;

        return bomb;
    }
}
