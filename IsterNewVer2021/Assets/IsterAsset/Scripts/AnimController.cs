using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    [SerializeField]
    protected Animator _anim;
    public Animator anim { get { return _anim; } }

    [SerializeField]
    protected SFXPlayer _sfx;

    protected virtual void Awake()
    {
        if (!_anim)
            _anim = GetComponent<Animator>();

        if (!_anim.runtimeAnimatorController)
        {
            _anim.runtimeAnimatorController = GetComponent<CharacterInfo>().animController;
        }
    }

    public void CharacterSetDir(Vector2 dir, float velocity)
    {
        _anim.SetFloat("velocity", velocity);
        _anim.SetFloat("dirX", dir.x);
        _anim.SetFloat("dirY", dir.y);
    }

    public virtual void Die()
    {
        _anim.SetBool("isDie", true);

        if (_sfx)
            _sfx.PlaySFX("die");

        //_anim.Update(Time.deltaTime);

        //if (_anim.GetFloat("dirX") < 0.0f)
        //{
        //    SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        //    sprite.flipX = true;
        //}
    }
}
