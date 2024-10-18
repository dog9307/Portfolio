using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherSpiritAttackController : MonoBehaviour
{
    MotherSpiritController _motherSpirit;
    MeleeConnecting _meleeConnecting;
   // public BulletCreator _creator;
   // public BulletCreator _meleeBulletCreateor;

    public float _attackRange;

    //패턴 진입 ------------------------------
    public bool _patternStart;
    public bool _healStart;
    public bool _attackStart;
    //---------------------------------------
    //---------------------------------------

    //중복 패턴 방지용 ------ 애니메이션 스크립트에서 증가 시킴
    protected float _attackCount;
    protected float _healCount;
    //------------------------------------------------------

    MotherSpirit _mother;
    // Start is called before the first frame update
    void Start()
    {
        _mother = GetComponent<MotherSpirit>();
        _patternStart = false;
        _healStart = false;
        _attackStart = false;
        _motherSpirit = GetComponent<MotherSpiritController>();
    }

    // Update is called once per frame
    void Update()
    {
       if(_motherSpirit._attackable)
       {
            _patternStart = _motherSpirit._attackable;


            float patternState = Random.Range(0, 10);
           
            // 공격
            if (_motherSpirit._sauronDown.Count != 0 && _motherSpirit._sauronUp.Count != 0 &&  _motherSpirit._sauronLeft.Count != 0 && _motherSpirit._sauronRight.Count != 0)
            {
                if (patternState < 3)
                {
                    _healStart = false;
                    _attackStart = true;
                }
                // 힐
                else
                {
                    _healStart = true;
                    _attackStart = false;
                }
            }

            else _attackStart = true;
        }
    }
    public void FireYangSanBullet()
    {
       // _creator.FireBullets();
    }
    public void FireMeleeBullet()
    {
      //  _meleeBulletCreateor.FireBullets();
    }
    public void RazerActivate()
    {     
        if (_mother._phaseCount == 0)
        {
            int phase1right = Random.Range(2, 6);
            int phase1left = Random.Range(2, 6);
            int phase1up = Random.Range(2, 6);
            int phase1down = Random.Range(2, 6);
            _motherSpirit._sauronRight[phase1right].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronLeft[phase1left].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronUp[phase1up].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronDown[phase1down].gameObject.GetComponent<SauronTimer>().StartLaser();
        }
        else if (_mother._phaseCount == 1)
        {
            int phase1right = Random.Range(1, 4);
            int phase1left = Random.Range(1, 4);
            int phase1up = Random.Range(1, 4);
            int phase1down = Random.Range(1, 4);
            int phase2right = Random.Range(4, 8);
            int phase2left = Random.Range(4, 8);
            int phase2up = Random.Range(4, 8);
            int phase2down = Random.Range(4, 8);
            _motherSpirit._sauronRight[phase1right].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronLeft[phase1left].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronUp[phase1up].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronDown[phase1down].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronRight[phase2right].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronLeft[phase2left].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronUp[phase2up].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronDown[phase2down].gameObject.GetComponent<SauronTimer>().StartLaser();
        }
        else if (_mother._phaseCount == 2)
        {
            int phase1right = Random.Range(0, 2);
            int phase1left = Random.Range(0, 2);
            int phase1up = Random.Range(0, 2);
            int phase1down = Random.Range(0, 2);
            int phase2right = Random.Range(2, 4);
            int phase2left = Random.Range(2, 4);
            int phase2up = Random.Range(2, 4);
            int phase2down = Random.Range(2, 4);
            int phase3right = Random.Range(4, 6);
            int phase3left = Random.Range(4, 6);
            int phase3up = Random.Range(4, 6);
            int phase3down = Random.Range(4, 6);
            int phase3right2 = Random.Range(6, 8);
            int phase3left2 = Random.Range(6, 8);
            int phase3up2 = Random.Range(6, 8);
            int phase3down2 = Random.Range(6, 8);
            _motherSpirit._sauronRight[phase1right].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronLeft[phase1left].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronUp[phase1up].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronDown[phase1down].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronRight[phase2right].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronLeft[phase2left].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronUp[phase2up].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronDown[phase2down].gameObject.GetComponent<SauronTimer>().StartLaser();       
            _motherSpirit._sauronRight[phase3right].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronLeft[phase3left].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronUp[phase3up].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronDown[phase3down].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronRight[phase3right2].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronLeft[phase3left2].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronUp[phase3up2].gameObject.GetComponent<SauronTimer>().StartLaser();
            _motherSpirit._sauronDown[phase3down2].gameObject.GetComponent<SauronTimer>().StartLaser();
        }
    }
    public void IncreaseHealCount()
    {
        _healCount += 1;
    }
    public void IncreaseAttackCount()
    {
        _attackCount += 1;
    }
    public void StateReset()
    {
        _patternStart = false;
        _healStart = false;
        _attackStart = false;
    }
}
