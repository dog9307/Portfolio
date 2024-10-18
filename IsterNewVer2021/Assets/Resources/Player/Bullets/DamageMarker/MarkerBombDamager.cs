using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerBombDamager : Damager
{
    public PassiveMarkerUser passiveUser { get; set; }

    public Transform target { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform != target) return;

        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable)
        {
            Vector2 dir = CommonFuncs.CalcDir(this, collision);
            Damage realDamage = _damage;

            if (passiveUser)
            {
                if (passiveUser.isDebuffRemover)
                {
                    bool isRemoveSuccess = false;

                    DebuffInfo debuffInfo = target.GetComponent<DebuffInfo>();
                    if (debuffInfo)
                    {
                        int count = debuffInfo.abnormalCount;

                        if (count != 0)
                        {
                            if (passiveUser.isRemoveAll)
                            {
                                debuffInfo.RemoveDebuffAll();

                                realDamage.additionalDamage += 10.0f * count;
                            }
                            else
                            {
                                debuffInfo.RemoveRandomDebuff();

                                realDamage.additionalDamage += 10.0f;
                            }

                            isRemoveSuccess = true;
                        }
                    }

                    if (isRemoveSuccess)
                    {
                        if (passiveUser.isNewMarker)
                        {
                            passiveUser.targetEnemy = target.gameObject;
                            passiveUser.UseSkill();
                        }
                    }
                }
            }

            damagable.HitDamager(realDamage, dir);
        }
    }
}
