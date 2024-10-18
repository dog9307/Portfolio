using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryingStunController : MonoBehaviour
{
    public EnemyInfo enemyInfo { get; set; }
    public bool isBoss { get { if (!enemyInfo) return false; return enemyInfo.isBoss; } }
    
    public bool isStun { get; set; }

    public float totalTime { get; set; }
    private float _currentTime;

    private DamagableStateResetter _resetter;
   // private EnemySightController _sight;
    
    private GameObject _effect;

    void Start()
    {
        _resetter = GetComponent<DamagableStateResetter>();

      //  EnemySightController[] sights = GetComponentsInChildren<EnemySightController>();
      //  if (sights.Length >= 1)
      //      _sight = sights[0];

        _effect = Resources.Load<GameObject>("Player/Skills/Effects/StunEffect/StunEffect");
        if (!_effect) return;

        _effect = Instantiate(_effect);
        _effect.transform.parent = enemyInfo.transform;
        EffectReposition();

        _effect.SetActive(false);
    }

    void EffectReposition()
    {
        SpriteRenderer sprite = _effect.transform.parent.GetComponent<SpriteRenderer>();
        if (!sprite) return;

        float yFactor = sprite.sprite.bounds.extents.y;

        Vector3 localPos = Vector3.zero;
        localPos.y += yFactor;
        _effect.transform.localPosition = localPos;

        _effect.transform.localScale = new Vector3(yFactor, yFactor, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStun) return;

        float deltaTime = IsterTimeManager.enemyDeltaTime;
        float realTotal = totalTime;

        if (isBoss)
        {
            deltaTime = IsterTimeManager.bossDeltaTime;
            realTotal *= 0.5f;
        }

        _currentTime += deltaTime;
        if (_currentTime >= realTotal)
        {
            StunEnd();
            return;
        }

        if (_resetter)
            _resetter.StateReset();
    }

    public void StunStart(float time)
    {
        totalTime = time;
        _currentTime = 0.0f;

        Stun(true);
    }

    public void StunEnd()
    {
        Stun(false);
    }

    void Stun(bool isStun)
    {
        this.isStun = isStun;

       // if (_sight)
       //     _sight.enabled = !isStun;

        if (_effect)
            _effect.SetActive(isStun);
    }
}
