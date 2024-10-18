using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummyAnimationController : AnimController, IObjectCreator
{
    [SerializeField]
    private Damagable _damagable;

    [SerializeField]
    private GameObject _prefab;
    public GameObject effectPrefab { get { return _prefab; } set { _prefab = value; } }

    [SerializeField]
    private GameObject _changeMapEffect;

    [SerializeField]
    private Transform[] _effectPoses;

    void OnEnable()
    {
        int count = PlayerPrefs.GetInt("IllimaEyeOpen", -1);
        if (count >= 100)
            EyeOpen();
    }

    void Update()
    {
        if (!_damagable) return;

        _damagable.currentHP = _damagable.totalHP;

        _anim.SetBool("isHurt", _damagable.isHurt);
    }

    public void Attack()
    {
        _anim.SetTrigger("attack");
    }

    private PlayerMoveController _player;
    public void ChangeMapSignal()
    {
        _sfx.PlaySFX("snap");
        CameraShakeController.instance.CameraShake(10.0f);
        
        if (_changeMapEffect)
        {
            if (_effectPoses != null)
            {
                foreach (var t in _effectPoses)
                {
                    if (!t) continue;

                    GameObject newEffect = Instantiate(_changeMapEffect);
                    newEffect.transform.position = t.position;
                }
            }
        }

        //UndergroundTutorialCutScene cutscene = FindObjectOfType<UndergroundTutorialCutScene>();
        //if (cutscene)
        //    cutscene.PlayerGoToUniverse();

        //AreaMiddleTutoCutscene areaMiddle = FindObjectOfType<AreaMiddleTutoCutscene>();
        //if (areaMiddle)
        //    areaMiddle.PlayerGoToTower();
    }

    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate(effectPrefab);
        if (_player)
            _player = FindObjectOfType<PlayerMoveController>();
        Vector3 newPos = Vector3.Lerp(transform.position, _player.transform.position, 0.9f);
        newBullet.transform.position = newPos;

        //TutorialDummyRoomFlow flow = FindObjectOfType<TutorialDummyRoomFlow>();
        //if (flow)
        //    flow.DestroyAttackTrigger();

        return newBullet;
    }

    public void EyeOpen()
    {
        _anim.SetBool("isFirstAppear", false);
        _anim.SetTrigger("eyeOpen");

        PlayerPrefs.SetInt("IllimaEyeOpen", 100);
    }
}
