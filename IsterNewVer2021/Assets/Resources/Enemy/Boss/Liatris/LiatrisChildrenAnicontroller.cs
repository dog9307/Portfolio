using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiatrisChildrenAnicontroller : MonoBehaviour
{
    public LiatrisSphereController _shpereCon;

    public GameObject _dieEffect;

    // Start is called before the first frame update
    void Start()
    {
        if (!_shpereCon) _shpereCon = GetComponentInParent<LiatrisSphereController>();

    }

    public void SizeUp()
    {
        _shpereCon.SizeUp();
    }
    public void SizeDown()
    {
        _shpereCon.SizeDown();
    }

    public void DieEffectOn()
    {
        if (_dieEffect)
        {
            _dieEffect.SetActive(true);
            _dieEffect.GetComponent<ParticleSystem>().Play();
        }
    }
    public void DieEffectOff()
    {
        if (_dieEffect)
        {
            _dieEffect.GetComponent<ParticleSystem>().Stop();
        }
    }
}
