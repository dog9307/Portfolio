using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllimaPillarAttack : BossAttackBase
{
    public float _attackCount;
    public int _fireCounter;

    public float _attackDelay;

    public float _attackEndDelay;

    public float _hideTime;

    float _currentCount;

    float _currentTime;
   // float _dropTime; 

    Coroutine _coroutine;

    public float _areaWidth, _areaHeight;

    [SerializeField]
    Transform _bulletCreateCenter;
    [SerializeField]
    GameObject _illimaDropBullet;
    [SerializeField]
    Transform _dropBulletCreateCenter;
    [SerializeField]
    GameObject _tracyingObject;
    [SerializeField]
    GameObject _dropPrevEffect;
    [SerializeField]
    float _tracyingSpeed;
    [SerializeField]
    float _playerTracyingDelay;
    float _currentTracyingDelay;
    [SerializeField]
    float _dropDelay;
    float _currentDropDelay;

    float posX;
    float posY;
    float createX;
    float createY;

    Coroutine _subCoroutine;

    [SerializeField]
    private CutSceneController _cameraZoomOut;
    [SerializeField]
    private CutSceneController _cameraZoomIn;

    //[SerializeField]
    //float _cameraShakingFigure;

    public override void SetPatternId()
    {
    }
    public override void PatternStart()
    {
        base.PatternStart();
        posX = _bulletCreateCenter.position.x;
        posY = _bulletCreateCenter.position.y;
        createX = _areaWidth / 2;
        createY = _areaHeight / 2;
        _currentTracyingDelay = 0;
        _currentDropDelay = 0;
        if (_dropPrevEffect)
        {
            _dropPrevEffect.GetComponent<AttackScaleUp>()._delayCount = _dropDelay;
        }
    }
    public override void PatternOn()
    {
        _currentCount = 0;
        _currentTime = 0;
        _coroutine = StartCoroutine(Jump());
    }
    public override void PatternOff()
    {
        _owner.attackPatternEnd = true;
        StopCoroutine(_coroutine);

    }
    public override void PatternEnd()
    {
        base.PatternEnd();

        _owner._isHide = false;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

    }

    //public void JumpUpCoroutine()
    //{
    //    _subCoroutine = StartCoroutine(JumpUp());
    //}
    //public void StopJump()
    //{
    //    if (_subCoroutine != null) {
    //        _dropTime = _currentTime;
    //        _currentTime = 0;
    //        StopCoroutine(_subCoroutine);
    //    }
    //} 
    //점프하기
    IEnumerator Jump()
    {
        //카메라 줌 아웃
        if (_cameraZoomOut)
            _cameraZoomOut.StartCutScene();

        //if(_sfx) _sfx.PlaySFX("disappear");

        _attacker._attackStart = false;
        _owner._isHide = true;
        _owner.GetComponent<Collider2D>().isTrigger = true;

        yield return null;

        _currentTime = 0;

        _coroutine = StartCoroutine(JumpAttack());
    }
    
   // IEnumerator JumpUp()
   // {
   //     _owner._isHide = true;
   //
   //     Vector3 newPos = transform.localPosition;
   //     newPos.y = Mathf.Lerp(0.0f, 40.0f, 0.0f);
   //     while (_currentTime < _hideTime)
   //     {
   //         _currentTime += IsterTimeManager.bossDeltaTime;
   //
   //         float ratio = _currentTime / _hideTime;
   //
   //         newPos.y = Mathf.Lerp(0.0f, 40.0f, ratio);
   //
   //         yield return null;
   //
   //         transform.localPosition = newPos;
   //     }
   //
   //     _currentTime = 0;
   //
   //     StopCoroutine(_subCoroutine);
   // }
    //점프 후 공격하는곳.
    IEnumerator JumpAttack()
    {
        while (_currentCount < _attackCount)
        {
            while (_currentTime < _attackDelay)
            {
                _currentTime += IsterTimeManager.bossDeltaTime;
                yield return null;
            }

            //불릿 생성.
            PillarBulletCreator();

            _currentTime = 0;
            _currentCount++;

            yield return null;
        }


        yield return new WaitForSeconds(_attackEndDelay);

        _currentTime = 0;

        _coroutine = StartCoroutine(JumpEndAndDropDelay());
    }
    //점프 끝
    IEnumerator JumpEndAndDropDelay()
    {
        if(_tracyingObject) _tracyingObject.SetActive(true);

        while (_currentTracyingDelay < _playerTracyingDelay)
        {
            var targetDir = (_owner.player.center - transform.position).normalized;
            Vector3 newPos = _tracyingObject.transform.position;
            _tracyingObject.transform.position = Vector3.Lerp(newPos, _owner.player.center, _tracyingSpeed * IsterTimeManager.bossDeltaTime);
            _dropPrevEffect.transform.localPosition = _tracyingObject.transform.localPosition;
            yield return null;

            _currentTracyingDelay += IsterTimeManager.bossDeltaTime;
        }

        yield return null;
        //카메라 줌인.
        if (_cameraZoomIn)
            _cameraZoomIn.StartCutScene();

        _currentTracyingDelay = 0;

        _coroutine = StartCoroutine(DropEffectOn());
    }
    IEnumerator DropEffectOn()
    {
        _dropPrevEffect.SetActive(true);

        Vector3 newPos = (_tracyingObject.transform.localPosition - _dropBulletCreateCenter.localPosition);
        this.transform.localPosition = newPos;
        
        while (_currentDropDelay < _dropDelay)
        {
            yield return null;

            _currentDropDelay += IsterTimeManager.bossDeltaTime;
        }

        yield return null;

        _currentDropDelay = 0;
        _currentTime = 0;

        if (_tracyingObject.activeSelf) _tracyingObject.SetActive(false);
      
        _coroutine = StartCoroutine(Drop());
    }
    IEnumerator Drop()
    {
        _dropPrevEffect.SetActive(false);

        while (_currentTime < _hideTime)
        {
            _currentTime += IsterTimeManager.bossDeltaTime;

            yield return null;
        }

        _owner.GetComponent<Collider2D>().isTrigger = false;
        _owner._isHide = false;

        //DropBulletCreator();

        PatternOff();
    }

    //기둥 불렛 생성하는 함수.
    void PillarBulletCreator()
    {
        Vector3 newPos = Vector3.zero;
        newPos = new Vector3(posX - Random.Range(-createX, createX), posY - Random.Range(-createY,createY), transform.position.z);
           
        GameObject newBullet = CreateObject();
        newBullet.transform.position = newPos;                 
    }

    public GameObject DropBulletCreator()
    {       
        GameObject newBullet = Instantiate<GameObject>(_illimaDropBullet);
        newBullet.transform.position = _dropBulletCreateCenter.position;

       // CameraShakeController.instance.CameraShake(_cameraShakingFigure);
        return newBullet;
    }
}
