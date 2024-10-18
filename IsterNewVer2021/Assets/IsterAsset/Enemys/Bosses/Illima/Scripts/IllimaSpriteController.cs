using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllimaSpriteController : MonoBehaviour
{
    [SerializeField]
    IllimaWarpAndAttack _warpAttack;
    [SerializeField]
    IllimaPillarAttack _pillatAttack;

    [SerializeField]
    SFXPlayer _sfx;
    // Start is called before the first frame update
    public void Clap()
    {
        if (_warpAttack) { 
            _warpAttack.Clap();
            if (_sfx) _sfx.PlaySFX("fingersnap");
        }
    }
    public void DropbulletCreate()
    {
        if (_pillatAttack) { 
            _pillatAttack.DropBulletCreator();
            if (_sfx) _sfx.PlaySFX("drop");
        }
    }
    public void IllimaRunaway()
    {
        GetComponentInParent<Illima>().gameObject.SetActive(false);
    }
}
