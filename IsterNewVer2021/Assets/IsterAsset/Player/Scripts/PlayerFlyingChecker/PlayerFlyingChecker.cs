using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyingChecker : MonoBehaviour
{
    private List<Collider2D> _cols = new List<Collider2D>();

    [SerializeField]
    private PlayerMoveController _move;
    private bool _prevHide = false;

    public float fallingDamage { get; set; } = 10.0f;

    [SerializeField]
    private PlayerAnimController _anim;

    private AmbientController _amb;
    private PlayerHPUIController _hpUI;

    // Update is called once per frame
    void Update()
    {
        if (_prevHide && !_move.isHide)
            Check();

        _prevHide = _move.isHide;
    }

    public void Check()
    {
        Collider2D[] cols = Physics2D.OverlapPointAll(_move.center);
        bool isAlive = false;
        foreach (var col in cols)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Room") && col.enabled)
            {
                isAlive = true;
                break;
            }
        }

        if (isAlive)
        {
            foreach (var col in cols)
            {
                if (col.gameObject.layer == LayerMask.NameToLayer("Gumddak") && col.enabled)
                {
                    isAlive = false;
                    break;
                }
            }
        }

        if (!isAlive)
            Falling();
    }

    public void Falling()
    {
        if (!_amb)
            _amb = FindObjectOfType<AmbientController>();

        float damage = (_amb ? _amb.fallingDamage : fallingDamage);
        _move.Falling(damage);

        if (damage >= float.Epsilon)
        {
            if (!_hpUI)
                _hpUI = FindObjectOfType<PlayerHPUIController>();

            if (_hpUI)
                _hpUI.HPChange();
        }

        if (_anim)
            _anim.Falling();
    }
}
