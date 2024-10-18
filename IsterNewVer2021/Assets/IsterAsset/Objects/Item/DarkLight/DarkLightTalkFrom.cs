using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkLightTalkFrom : ItemTalkFrom, IObjectCreator
{
    [SerializeField]
    private int _figure;
    public int figure { get { return _figure; } set { _figure = value; } }

    [SerializeField]
    private GameObject _effectPrefab;
    public GameObject effectPrefab { get { return _effectPrefab; } set { _effectPrefab = value; } }

    public bool isAutoGain { get; set; }

    [SerializeField]
    private DisposableObject _disposable;

    public GameObject CreateObject()
    {
        GameObject newEffect = Instantiate(effectPrefab);
        newEffect.transform.position = transform.position;

        return newEffect;
    }

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        if (!_inventory)
            _inventory = FindObjectOfType<PlayerInventory>();

        _inventory.GainDarkLight(figure);

        if (_disposable)
            _disposable.UseObject();

        Destroy(gameObject);

        CreateObject();

        _sfx.PlaySFX(_sfxName);
    }
}
