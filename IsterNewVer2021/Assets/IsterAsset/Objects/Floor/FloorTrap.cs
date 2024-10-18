using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrap : FootholdBase
{
    [SerializeField]
    protected float _cooltime;

    private float _currentTime;

    [SerializeField]
    bool _noneTargetTrap;

    private bool _tarpActive;
    public bool _isResetTrap;

    public GameObject _trap;
    public GameObject _trapDamager;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        _currentTime = 0;

        if (_noneTargetTrap) TrapOn();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (_tarpActive)
        {
            _collider.enabled = false;
        }
        else
        {
            _collider.enabled = true;
        }

        if (_noneTargetTrap && _isCanActive)
        {
            TrapOn();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_noneTargetTrap)
        {
            if (_isCanActive && collision.tag.Equals("PLAYER"))
            {
                TrapOn();
            }
        }
    }
    public void TrapOn()
    {
        _trap.SetActive(true);
        _isCanActive = false;
        _tarpActive = true;
    }

    public void ResetTrap()
    {
        _trap.SetActive(false);

        if (_isResetTrap)
        {
            StartCoroutine(trapReset());
        }
        else return;
    }

    IEnumerator trapReset()
    {      
        yield return new WaitForSeconds(_cooltime);

        _tarpActive = false;
        _isCanActive = true;
    }

}
