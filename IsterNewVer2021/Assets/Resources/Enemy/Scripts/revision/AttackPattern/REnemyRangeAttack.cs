using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RANGETYPE
{
    NONE = -1,
    NORMAL = 0,
    CIRCLE = 1,
    SEMICIRCLE = 2,
    RANDOM = 3,
    WAVE = 4,
    END = 5
}

[System.Serializable]
public class REnemyRangeAttack : REnemyAttackPatternBase
{
    public RANGETYPE _type;

    private float _angleCut;

    Vector2 dir;

    public override void Init()
    {
        base.Init();
       
        PatternStart();
        attackType = AP.rangebasic;

    }

    public override void Update()
    {
        if (isPatternEnd) return;

        if (_isShoot) 
            PatternEnd();

        base.Update();
    }
    public override void Reload()
    {
        if (_attacker)
        {
            _fireCount = _attacker._rangeAttackSetter._fireCount;
        }
        _isShoot = false;
    }
    public override void FireBullet()
    {
        GameObject _targetPos;
        _targetPos = _owner._owner.Target;
        if (_targetPos.GetType() == typeof(PlayerMoveController))
        {
            dir = -CommonFuncs.CalcDir(_targetPos.GetComponent<PlayerMoveController>().center, _owner.transform.position);
        }
        else
        {
            dir = -CommonFuncs.CalcDir(_targetPos.transform.position, _owner.transform.position);
        }
        // Vector2 dir = CommonFuncs.CalcDir(transform.position, _player);
        float dot = Vector3.Dot(Vector2.right, dir);
        float startAngle = Mathf.Acos(dot);

        if (dir.y < 0.0f)
            startAngle = Mathf.PI * 2 - startAngle;
       
            switch (_type)
            {
                case RANGETYPE.NONE:

                    break;
                case RANGETYPE.NORMAL:
                    NormalAttack(_attacker._rangeAttackSetter, startAngle);
                    break;
                case RANGETYPE.SEMICIRCLE:               
                    SemiCircleAttack(_attacker._rangeAttackSetter,startAngle);                  
                    break;
                case RANGETYPE.CIRCLE:
                    CircleAttack(_attacker._rangeAttackSetter,startAngle);
                    break;
                case RANGETYPE.RANDOM:
                    break;
                case RANGETYPE.WAVE:
                    WaveAttack(_attacker._rangeAttackSetter, startAngle);
                break;
                case RANGETYPE.END:

                    break;
            }
        
    }

    protected void NormalAttack(RangeAttackSetter _Setter, float startAngle)
    {
        if (_attacker._intervalCount > _Setter._repeatCount)
        {
            _isShoot = true;
        }
        else
        {
            if(_Setter._repeatCount ==0)
            {
                GameObject newBullet = _creator.CreateObject();
                EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();

                if (controller)
                {
                    float angle = startAngle;

                    if (angle > Mathf.PI * 2)
                        angle -= Mathf.PI * 2;

                    controller.dir = CommonFuncs.CalcDir(this, _owner._owner.Target.GetComponent<PlayerMoveController>().center);
                }
                _isShoot = true;
            }
            else
            {
                GameObject newBullet = _creator.CreateObject();
                EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();

                if (controller)
                {
                    float angle = startAngle;

                    if (angle > Mathf.PI * 2)
                        angle -= Mathf.PI * 2;

                    controller.dir = CommonFuncs.CalcDir(this, _owner._owner.Target.GetComponent<PlayerMoveController>().center);
                }
                _isShoot = true;
            }          
        }
    }
    protected void CircleAttack(RangeAttackSetter _Setter, float startAngle)
    {
        _angleCut = (Mathf.PI * 2) / _fireCount;

        if (_attacker._intervalCount > _Setter._repeatCount)
        {
            _isShoot = true;
        }
        else
        {
            if (_Setter._repeatCount == 0)
            {
                for (int i = 0; i < _fireCount; ++i)
                {
                    GameObject newBullet = _creator.CreateObject();
                    EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();

                    if (controller)
                    {
                        float angle = startAngle + (i * _angleCut) * Mathf.Deg2Rad;

                        if (angle > Mathf.PI * 2)
                            angle -= Mathf.PI * 2;

                        controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                    }
                }
                _isShoot = true;
            }
            else
            {
                for (int i = 0; i < _fireCount; ++i)
                {
                    GameObject newBullet = _creator.CreateObject();
                    EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();

                    if (controller)
                    {
                        float angle = startAngle + (i * _angleCut) * Mathf.Deg2Rad + i * _attacker._rangeAttackSetter._turnAngle * Mathf.Deg2Rad;

                        if (angle > Mathf.PI * 2)
                            angle -= Mathf.PI * 2;

                        controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                    }
                }
                _isShoot = true;
            }
        }
    }
    protected void SemiCircleAttack(RangeAttackSetter _Setter, float startAngle)
    {
        _angleCut = _Setter._angleMax / _fireCount;

        if (_attacker._intervalCount > _Setter._repeatCount)
        {
            _isShoot = true;
        }
        else
        {
            if (_Setter._repeatCount == 0)
            {
                if (_fireCount % 2 != 0)
                {
                    for (int i = 0; i < _fireCount; ++i)
                    {
                        GameObject newBullet = _creator.CreateObject();
                        EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();
                        if (controller)
                        {
                            float angle = startAngle - _angleCut * (_fireCount / 2) * Mathf.Deg2Rad + _angleCut * i * Mathf.Deg2Rad;
                            if (angle > Mathf.PI * 2)
                                angle -= Mathf.PI * 2;
                            controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < _fireCount; ++i)
                    {
                        GameObject newBullet = _creator.CreateObject();
                        EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();
                        if (controller)
                        {
                            float angle = (startAngle + (_angleCut / 2 * Mathf.Deg2Rad)) - _angleCut * (_fireCount / 2) * Mathf.Deg2Rad + _angleCut * i * Mathf.Deg2Rad;
                            if (angle > Mathf.PI * 2)
                                angle -= Mathf.PI * 2;
                            controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                        }
                    }
                }

                _isShoot = true;
            }
            else
            {
                if (_fireCount % 2 != 0)
                {
                    if (_attacker._intervalCount % 2 == 0)
                    {
                        for (int i = 0; i < _fireCount; ++i)
                        {
                            GameObject newBullet = _creator.CreateObject();
                            EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();
                            if (controller)
                            {
                                float angle = startAngle - _angleCut * (_fireCount / 2) * Mathf.Deg2Rad + _angleCut * i * Mathf.Deg2Rad - _Setter._turnAngle * Mathf.Deg2Rad;
                                if (angle > Mathf.PI * 2)
                                    angle -= Mathf.PI * 2;
                                controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < _fireCount; ++i)
                        {
                            GameObject newBullet = _creator.CreateObject();
                            EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();
                            if (controller)
                            {
                                float angle = startAngle - _angleCut * (_fireCount / 2) * Mathf.Deg2Rad + _angleCut * i * Mathf.Deg2Rad + _Setter._turnAngle * Mathf.Deg2Rad;
                                if (angle > Mathf.PI * 2)
                                    angle -= Mathf.PI * 2;
                                controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                            }
                        }
                    }
                }
                else
                {
                    if (_attacker._intervalCount % 2 == 0)
                    {
                        for (int i = 0; i < _fireCount; ++i)
                        {
                            GameObject newBullet = _creator.CreateObject();
                            EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();
                            if (controller)
                            {
                                float angle = (startAngle + (_angleCut / 2 * Mathf.Deg2Rad)) - _angleCut * (_fireCount / 2) * Mathf.Deg2Rad + _angleCut * i * Mathf.Deg2Rad - _attacker._rangeAttackSetter._turnAngle * Mathf.Deg2Rad; ;
                                if (angle > Mathf.PI * 2)
                                    angle -= Mathf.PI * 2;
                                controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < _fireCount; ++i)
                        {
                            GameObject newBullet = _creator.CreateObject();
                            EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();
                            if (controller)
                            {
                                float angle = (startAngle + (_angleCut / 2 * Mathf.Deg2Rad)) - _angleCut * (_fireCount / 2) * Mathf.Deg2Rad + _angleCut * i * Mathf.Deg2Rad + _Setter._turnAngle * Mathf.Deg2Rad; ;
                                if (angle > Mathf.PI * 2)
                                    angle -= Mathf.PI * 2;
                                controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                            }
                        }
                    }
                }
                _isShoot = true;
            }
        }
    }
    protected void RandomAttack(float startAngle)
    {

    }
    protected void WaveAttack(RangeAttackSetter _Setter, float startAngle)
    {
        if (_attacker._intervalCount > _Setter._repeatCount)
        {
            _isShoot = true;
        }
        else
        {
            if (_Setter._repeatCount == 0)
            {
                NormalAttack(_attacker._rangeAttackSetter, startAngle);
                _isShoot = true;
            }
            else
            {
                _angleCut = (_Setter._angleMax / _Setter._repeatCount * _Setter._waveCount);

                //ex 45/3 = 15;
                int count = _Setter._repeatCount / _Setter._waveCount;

                for (int i = 0; i < _fireCount; ++i)
                {
                    GameObject newBullet = _creator.CreateObject();
                    EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();

                    if (controller)
                    {
                        if (_attacker._intervalCount / count == 0)
                        {
                            float angle = startAngle - (count / 2 * _angleCut) * Mathf.Deg2Rad + (_attacker._intervalCount % count) * _angleCut * Mathf.Deg2Rad;

                            if (angle > Mathf.PI * 2)
                                angle -= Mathf.PI * 2;

                            controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                        }
                        else
                        {
                            if ((_attacker._intervalCount / count) % 2 == 0)
                            {
                                float angle = startAngle - (count / 2 * _angleCut) * Mathf.Deg2Rad + (_attacker._intervalCount % count) * _angleCut * Mathf.Deg2Rad;

                                if (angle > Mathf.PI * 2)
                                    angle -= Mathf.PI * 2;

                                controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                            }
                            else
                            {
                                float angle = startAngle + (count / 2 * _angleCut) * Mathf.Deg2Rad - (_attacker._intervalCount % count) * _angleCut * Mathf.Deg2Rad;

                                if (angle > Mathf.PI * 2)
                                    angle -= Mathf.PI * 2;

                                controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                            }
                        }
                    }
                }
                _isShoot = true;
            }
        }
    }
}


//Circle
/*_angleCut = (Mathf.PI * 2) / _fireCount;

        if (_attacker._rangeAttackSetter._repeatCount == 0)
        {
            for (int i = 0; i < _fireCount; ++i)
            {
                GameObject newBullet = _creator.CreateObject();
                EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();

                if (controller)
                {
                    float angle = startAngle + (i * _angleCut) * Mathf.Deg2Rad;

                    if (angle > Mathf.PI * 2)
                        angle -= Mathf.PI * 2;

                    controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                }
            }
            _isShoot = true;
        }
        else
        {
            if (_attacker._intervalCount < _attacker._rangeAttackSetter._repeatCount)
            {
                _attacker._timer += IsterTimeManager.deltaTime;
                _attacker._intervalCount ++;
                if (_attacker._timer > _attacker._rangeAttackSetter._intervalTimer)
                {
                    _attacker._timer = 0;
                    for (int i = 0; i < _fireCount; ++i)
                    {
                        GameObject newBullet = _creator.CreateObject();
                        EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();

                        if (controller)
                        {
                            float angle = startAngle + (i * _angleCut) * Mathf.Deg2Rad + i * _attacker._rangeAttackSetter._turnAngle * Mathf.Deg2Rad;

                            if (angle > Mathf.PI * 2)
                                angle -= Mathf.PI * 2;

                            controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                        }
                    }
                }
            }
            _isShoot = true;
        }
 */

//semicircle
/*_angleCut = _attacker._rangeAttackSetter._angleMax / _fireCount;
    //
    //if (_attacker._rangeAttackSetter._repeatCount == 0)
    //{
    //    _angleCut = _attacker._rangeAttackSetter._angleMax / _fireCount;
    //    if (_fireCount % 2 != 0)
    //    {
    //        for (int i = 0; i < _fireCount; ++i)
    //        {
    //            GameObject newBullet = _creator.CreateObject();
    //            EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();
    //            if (controller)
    //            {
    //                float angle = startAngle - _angleCut * (_fireCount / 2) * Mathf.Deg2Rad + _angleCut * i * Mathf.Deg2Rad;
    //                if (angle > Mathf.PI * 2)
    //                    angle -= Mathf.PI * 2;
    //                controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    //            }
    //        }
    //    }
    //    else
    //    {
    //        for (int i = 0; i < _fireCount; ++i)
    //        {
    //            GameObject newBullet = _creator.CreateObject();
    //            EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();
    //            if (controller)
    //            {
    //                float angle = (startAngle + (_angleCut / 2 * Mathf.Deg2Rad)) - _angleCut * (_fireCount / 2) * Mathf.Deg2Rad + _angleCut * i * Mathf.Deg2Rad;
    //                if (angle > Mathf.PI * 2)
    //                    angle -= Mathf.PI * 2;
    //                controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    //            }
    //        }
    //    }
    //    _isShoot = true;
    //}
    ////반복될때.
    //else
    //{
    //    _angleCut = _attacker._rangeAttackSetter._angleMax / _fireCount;
    //
    //    if (_attacker._intervalCount < _attacker._rangeAttackSetter._repeatCount)
    //    {
    //        if (_attacker._timer > _attacker._rangeAttackSetter._intervalTimer)
    //        {
    //            _attacker._timer = 0;
    //            _attacker._intervalCount++;
    //            if (_fireCount % 2 != 0)
    //            {
    //                if (_attacker._intervalCount % 2 == 0)
    //                {
    //                    for (int i = 0; i < _fireCount; ++i)
    //                    {
    //                        GameObject newBullet = _creator.CreateObject();
    //                        EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();
    //                        if (controller)
    //                        {
    //                            float angle = startAngle - _angleCut * (_fireCount / 2) * Mathf.Deg2Rad + _angleCut * i * Mathf.Deg2Rad - _attacker._rangeAttackSetter._turnAngle * Mathf.Deg2Rad;
    //                            if (angle > Mathf.PI * 2)
    //                                angle -= Mathf.PI * 2;
    //                            controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    for (int i = 0; i < _fireCount; ++i)
    //                    {
    //                        GameObject newBullet = _creator.CreateObject();
    //                        EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();
    //                        if (controller)
    //                        {
    //                            float angle = startAngle - _angleCut * (_fireCount / 2) * Mathf.Deg2Rad + _angleCut * i * Mathf.Deg2Rad + _attacker._rangeAttackSetter._turnAngle * Mathf.Deg2Rad;
    //                            if (angle > Mathf.PI * 2)
    //                                angle -= Mathf.PI * 2;
    //                            controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    //                        }
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                if (_attacker._intervalCount % 2 == 0)
    //                {
    //                    for (int i = 0; i < _fireCount; ++i)
    //                    {
    //                        GameObject newBullet = _creator.CreateObject();
    //                        EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();
    //                        if (controller)
    //                        {
    //                            float angle = (startAngle + (_angleCut / 2 * Mathf.Deg2Rad)) - _angleCut * (_fireCount / 2) * Mathf.Deg2Rad + _angleCut * i * Mathf.Deg2Rad - _attacker._rangeAttackSetter._turnAngle * Mathf.Deg2Rad; ;
    //                            if (angle > Mathf.PI * 2)
    //                                angle -= Mathf.PI * 2;
    //                            controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    for (int i = 0; i < _fireCount; ++i)
    //                    {
    //                        GameObject newBullet = _creator.CreateObject();
    //                        EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();
    //                        if (controller)
    //                        {
    //                            float angle = (startAngle + (_angleCut / 2 * Mathf.Deg2Rad)) - _angleCut * (_fireCount / 2) * Mathf.Deg2Rad + _angleCut * i * Mathf.Deg2Rad + _attacker._rangeAttackSetter._turnAngle * Mathf.Deg2Rad; ;
    //                            if (angle > Mathf.PI * 2)
    //                                angle -= Mathf.PI * 2;
    //                            controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    //                        }
    //                    }
    //                }
    //            }
    //            //_isShoot = true;
    //        }
    //    }
    //}
    */

//wave
/*
 if (_attacker._rangeAttackSetter._repeatCount == 0)
        {
            NormalAttack(_attacker._rangeAttackSetter, startAngle);
        }
        else
        {
            _angleCut = (_attacker._rangeAttackSetter._angleMax / _attacker._rangeAttackSetter._repeatCount * _attacker._rangeAttackSetter._waveCount);

            //ex 45/3 = 15;
            int count = _attacker._rangeAttackSetter._repeatCount / _attacker._rangeAttackSetter._waveCount;

            if (_attacker._intervalCount < _attacker._rangeAttackSetter._repeatCount)
            {
                _attacker._timer += IsterTimeManager.deltaTime;
                _attacker._intervalCount++;
                if (_attacker._timer > _attacker._rangeAttackSetter._intervalTimer)
                {
                    _attacker._timer = 0;

                    for (int i = 0; i < _fireCount; ++i)
                    {
                        GameObject newBullet = _creator.CreateObject();
                        EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();

                        if (controller)
                        {
                            if (_attacker._intervalCount / count == 0)
                            {
                                float angle = startAngle - (count / 2 * _angleCut) * Mathf.Deg2Rad + (_attacker._intervalCount % count) * _angleCut * Mathf.Deg2Rad;

                                if (angle > Mathf.PI * 2)
                                    angle -= Mathf.PI * 2;

                                controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                            }
                            else
                            {
                                if ((_attacker._intervalCount / count) % 2 == 0)
                                {
                                    float angle = startAngle - (count / 2 * _angleCut) * Mathf.Deg2Rad + (_attacker._intervalCount % count) * _angleCut * Mathf.Deg2Rad;

                                    if (angle > Mathf.PI * 2)
                                        angle -= Mathf.PI * 2;

                                    controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                                }
                                else
                                {
                                    float angle = startAngle + (count / 2 * _angleCut) * Mathf.Deg2Rad - (_attacker._intervalCount % count) * _angleCut * Mathf.Deg2Rad;

                                    if (angle > Mathf.PI * 2)
                                        angle -= Mathf.PI * 2;

                                    controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                                }
                            }
                        }
                    }
                }
            }
            _isShoot = true;
        }
    }
    */
