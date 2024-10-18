using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinesController : MonoBehaviour
{
    [SerializeField]
    BossMain _liatris;
    [SerializeField]
    BossController _controller;
    [SerializeField]
    List<GameObject> _vines = new List<GameObject>();
    [SerializeField]
    List<Transform> _vinesPos = new List<Transform>();

    [SerializeField]
    BossDamagableCondition _condition;
   //[SerializeField]
   //List<Transform> _attackReadyPos = new List<Transform>();

    public float _attackDelay;

    //bool _isActive;
    bool _isAwake;
    bool _patternStart;

    int _prevVine;
    int _currentVine;

    // Start is called before the first frame update
    private void OnEnable()
    {
        _currentVine = 0;
       // _isActive = true;
        VineSet();
    }
    void Start()
    {
       // _isActive = false;
        _patternStart = false;
        _prevVine = Random.Range(0, _vines.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_liatris.damagable.isDie)
        {         
            if (_condition.isPhaseChanging && !_patternStart)
            {
                VineOn();
            }

            if (!_condition.isPhaseChanging && AllVineOn())
            {
                if (!VineAttacking())
                {
                    VineAttack();
                }
            }
        }
        else
        {
            VineOff();
          //  _isActive = false;
        }
    }
    void VineSet()
    {
        for(int i =0; i< _vines.Count; i++)
        {
            if (i % 2 == 1)
            {
                _vines[i].transform.localScale = new Vector3(-1, 1, 1);
            }
            _vines[i].transform.position = _vinesPos[i].position;
            _vines[i].GetComponent<Vine>()._prevPos = _vinesPos[i];
            //_vines[i].GetComponent<Vine>()._attackPos = _attackReadyPos[i];
            _vines[i].gameObject.SetActive(false);
           // _vines[i].GetComponent<Vine>().isAwake = false;
            _vines[i].GetComponent<Vine>().attackDelay = _attackDelay;
        }
    }
    void VineOn()
    {
        _patternStart = true;
        for (int i = 0; i < _vines.Count; i++)
        {
            _vines[i].gameObject.SetActive(true);
            _vines[i].GetComponent<Vine>().isActive = true;
        }
    }
    void VineOff()
    {
        for (int i = 0; i < _vines.Count; i++)
        {
            _vines[i].GetComponent<Vine>().isActive = false;
        }
    }
    bool AllVineOn()
    {
        bool allOn = true;

        for (int i = 0; i < _vines.Count; i++)
        {
            if (_vines[i].GetComponent<Vine>().isActive) continue;         
            else
            {
                allOn = false;
                break;
            }
        }

        return allOn;

        // bool allOn = true;
        //
        // foreach(GameObject vine in _vines)
        // {
        //     bool isActive = vine.GetComponent<Vine>().isActive;
        //
        //     if (!vine) continue;
        //
        //     if (!isActive)
        //     {
        //         allOn = false;
        //         break;
        //     }
        // }
        //
        // return allOn;
    }

    bool VineAttacking()
    {
        bool Attacking = false;

        for (int i = 0; i < _vines.Count; i++)
        {
            if (_vines[i].GetComponent<Vine>()._isAttacking)
            {
                Attacking = true;
                break;
            }
            else continue;
        }

        return Attacking;
        //foreach (GameObject vine in _vines)
        //{
        //    if (!vine) continue;
        //
        //    bool attacking = vine.GetComponent<Vine>()._isAttacking;
        //
        //    if (!attacking)
        //    {
        //        return false;
        //    }
        //}
        //
        //return true;

    }

    void VineAttack()
    {
        if (_currentVine > (_vines.Count - 1)) _currentVine = 0;

        _vines[_currentVine].GetComponent<Vine>()._isAttacking = true;
        _vines[_currentVine].GetComponent<Vine>()._damageArea.SetActive(true);
    
        _currentVine++;
        //int ranVine = Random.Range(0, _vines.Count);
        //_currentVine = ranVine;
        //
        //if (_prevVine != _currentVine)
        //{
        //    _prevVine = _currentVine;
        //    //_vines[_currentVine].GetComponent<Vine>().gameObject.transform.position = _attackReadyPos[_currentVine].position;
        //    _vines[_currentVine].GetComponent<Vine>()._isAttacking = true;
        //    _vines[_currentVine].GetComponent<Vine>()._damageArea.SetActive(true);
        //}
        //else
        //{
        //    VineAttack();
        //}
    }
}
