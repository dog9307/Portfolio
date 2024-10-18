using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackDragDownner : MonoBehaviour
{
    public PassiveKnockbackIncreaseUser user { get; set; }

    private DebuffInfo _parentDebuff;

    // Start is called before the first frame update
    void Start()
    {
        if (!user) return;

        _parentDebuff = GetComponentInParent<DebuffInfo>();
        if (!_parentDebuff) return;

        _parentDebuff.knockbackDragDecrease = user.totalFigure;
    }

    public void Destroy()
    {
        _parentDebuff.knockbackDragDecrease = 0.0f;
        Destroy(gameObject);
    }
}
