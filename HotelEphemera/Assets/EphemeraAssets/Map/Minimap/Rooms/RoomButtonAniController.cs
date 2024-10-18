using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Coffee.UIExtensions;

public class RoomButtonAniController : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler 
{
    RoomSelector _roomSelector;
    Animator _buttonAnimator;

    //하이라이트 이펙트
    public GameObject _highlightEffectObject;
    public ParticleSystem _highlightEffect;
    public Vector3 _highlightEffectScale;

    List<GameObject> _EffectObj;

    // Start is called before the first frame update
    void Start()
    {
        _roomSelector = GetComponentInParent<RoomSelector>();
        _buttonAnimator = this.GetComponent<Animator>();
    }
    void Update()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_roomSelector._minimap._roomSelectorEffectorOn)
        {
            if (_highlightEffectObject && _roomSelector._thisRoom._isCanSelect)
            {
                _highlightEffectObject.transform.position = this.transform.position;
                _highlightEffectObject.GetComponent<UIParticle>().scale3D = _highlightEffectScale;

                if (_highlightEffect) _highlightEffect.Play();
            }
        }

        if (!_roomSelector._isClick && _roomSelector._thisRoom._isCanSelect)
        {
            _buttonAnimator.SetTrigger("highlighted");

            //if (_roomSelector._minimap._roomSelectorEffectorOn)
            //{
            //    if (_highlightEffectObject)
            //    {
            //        _highlightEffectObject.transform.position = this.transform.position;
            //        _highlightEffectObject.GetComponent<UIParticle>().scale3D = _highlightEffectScale;

            //        if (_highlightEffect) _highlightEffect.Play();
            //    }
            //}
        }
        else return;
    }   
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_highlightEffect.isPlaying && _highlightEffect) _highlightEffect.Stop();

        if (!_roomSelector._isClick && _roomSelector._thisRoom._isCanSelect)
        {
            _buttonAnimator.SetTrigger("disable");
        }
        else return;


    }
}
