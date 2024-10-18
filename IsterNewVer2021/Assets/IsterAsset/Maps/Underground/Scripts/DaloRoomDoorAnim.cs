using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaloRoomDoorAnim : ConditionalDoorAnimBase, IObjectCreator
{
    [SerializeField]
    private GameObject _hitEffect;
    public GameObject effectPrefab { get { return _hitEffect; } set { _hitEffect = value; } }

    [SerializeField]
    private Transform _mask;

    public override void CloseAnim()
    {
        if (_sfx)
            _sfx.PlaySFX("close");

        _anim.SetTrigger("shake");
    }

    public override void OpenAnim()
    {
        if (_sfx)
            _sfx.PlaySFX("open");

        _anim.SetBool("isOpen", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            CameraShakeController.instance.CameraShake(5.0f);

            GameObject newEffect = CreateObject();

            if (newEffect)
            {
                Vector3 newPos = transform.position;
                newPos.x = collision.transform.position.x;
                newEffect.transform.position = newPos;
            }

            if (_sfx)
                _sfx.PlaySFX("hit");

            if (_anim)
                _anim.SetTrigger("hit");
        }
    }

    public GameObject CreateObject()
    {
        if (!effectPrefab) return null;
        GameObject newEffect = Instantiate(effectPrefab);
        newEffect.transform.position = transform.position;

        return newEffect;
    }

    public override void ReCloseAnim()
    {
    }

    public void DaloRoomAlreadyOpen()
    {
        _mask.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
