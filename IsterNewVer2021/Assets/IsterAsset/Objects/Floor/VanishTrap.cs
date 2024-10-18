using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishTrap : FootholdBase
{
    [SerializeField]
    protected float _cooltime;

    [SerializeField]
    private float _vanishingTime;

    //[SerializeField]
    //private Transform _resetPos;

    private SpriteRenderer _image;

    private float _currentTimer;
    private float _currentCoolTimer;

    private bool _vanishingEnd;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _currentTimer = 0;
        _currentCoolTimer = 0;

        _image = GetComponent<SpriteRenderer>();       
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (_vanishingEnd)
        {
            _collider.enabled = false;
        }
        else
        {
            _collider.enabled = true;
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isCanActive && collision.tag.Equals("PLAYER"))
        {
            StartCoroutine(vanishing());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerFlyingChecker player = collision.GetComponent<PlayerFlyingChecker>();
        if (_vanishingEnd && player)
        {
            player.Falling();
        }
    }

    IEnumerator vanishing()
    {
        _isCanActive = false;

        Color color = _image.color;
        color.a = Mathf.Lerp(1.0f, 0.0f, 0.0f);

        while (_currentTimer < _vanishingTime)
        {
            _currentTimer += IsterTimeManager.deltaTime;

            float ratio = _currentTimer / _vanishingTime;

            color.a = Mathf.Lerp(1.0f, 0.0f, ratio);

            _image.color = color;

            yield return null;
        }

        StartCoroutine(ResetFloor());
        _vanishingEnd = true;
    }
    IEnumerator ResetFloor()
    {

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(TrapReset());

    }

    IEnumerator TrapReset()
    {
        Color color = _image.color;

        color.a = Mathf.Lerp(0.0f, 1.0f, 0.0f);

        while (_currentCoolTimer < _vanishingTime)
        {
            _currentCoolTimer += IsterTimeManager.deltaTime;

            float ratio = _currentCoolTimer / _vanishingTime;

            color.a = Mathf.Lerp(0.0f, 1.0f, ratio);

            _image.color = color;

            yield return null;
        }

        _currentCoolTimer = 0.0f;
        _currentTimer = 0.0f;

        _vanishingEnd = false;
        _isCanActive = true;
        //Color color = _image.color;

        //yield return new WaitForSeconds(_cooltime);

        //color.a = 1.0f;

        //_image.color = color;

        //_currentCoolTimer = 0;
        //_currentTimer = 0.0f;

        //_vanishingEnd = false;
        //_isCanActive = true;
    }
}
