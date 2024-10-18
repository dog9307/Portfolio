using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherSpiritSphereController : MonoBehaviour
{
    MotherSpiritAttackController _attackController;

    [SerializeField]
    GameObject _sphere;

    GlowableObject _glowColor;

    [SerializeField]
    private ParticleSystem _healOrb;
    [SerializeField]
    private ParticleSystem _normalOrb;
    [SerializeField]
    private GameObject _attackEffect;

    private void Start()
    {
        if (_sphere) { 
        _glowColor = _sphere.GetComponent<GlowableObject>();
        }

        _attackController = GetComponent<MotherSpiritAttackController>();
    }
    public void PatternSphereOn()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("isHeal", _attackController._healStart);
        anim.SetBool("isAttack", _attackController._attackStart);

        if (_attackController._attackStart && !_attackController._healStart)
        {
            //_sphere.SetActive(true);
            //_glowColor.color = new Color(1, 0, 0, 1);
            anim.SetTrigger("attack");
            _normalOrb.Play();
        }
        if (_attackController._healStart && !_attackController._attackStart)
        {
            //_sphere.SetActive(true);
            //_glowColor.color = new Color(0, 1, 0, 1);
            anim.SetTrigger("heal");
            _healOrb.Play();
        }
    }
    public void SpawnSphereOn()
    {
        _normalOrb.Play();

        _sphere.SetActive(true);
        _glowColor.color = new Color(0, 1, 1, 1);
    }
    public void SphereOff()
    {
        //if (_sphere.activeSelf) _sphere.SetActive(false);
        _healOrb.Stop();

        if (_normalOrb.isPlaying)
        {
            GameObject effect = Instantiate(_attackEffect);
            effect.transform.position = _sphere.transform.position;
        }

        _normalOrb.Stop();
    }
}
