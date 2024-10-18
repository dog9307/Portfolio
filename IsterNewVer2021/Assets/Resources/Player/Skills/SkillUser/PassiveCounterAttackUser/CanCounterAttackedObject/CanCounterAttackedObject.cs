using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanCounterAttackedObject : MonoBehaviour
{
    private PassiveCounterAttackUser _user;

    public EnemyInfo enemyInfo { get; set; }

    // test
    private const float MAX_TIME = 0.5f;
    private float _currentTime;

    public bool isCanCountered
    {
        get
        {
            return (_user != null) && (_currentTime < MAX_TIME);
        }

        set
        {
            if (!value)
            {
                _currentTime = MAX_TIME;

                if (_effect)
                    _effect.SetActive(false);
            }
        }
    }
    
    private GameObject _effect;

    // Start is called before the first frame update
    void Start()
    {
        if (!_effect)
        {
            GameObject prefab = Resources.Load<GameObject>("Player/Skills/SkillUser/PassiveCounterAttackUser/CanCounterAttackedObject/CounterMarker");
            _effect = Instantiate(prefab);
            _effect.transform.parent = transform;

            Vector2 pos = Vector2.zero;
            Collider2D collider = GetComponent<Collider2D>();
            pos.y += collider.bounds.extents.y + 1.0f;
            _effect.transform.localPosition = pos;

            _effect.SetActive(false);
        }
        
        FindRelativeUser();
    }

    public void FindRelativeUser()
    {
        SkillUserManager manager = FindObjectOfType<PlayerMoveController>().GetComponentInChildren<SkillUserManager>();
        _user = manager.FindUser(typeof(PassiveCounterAttack)) as PassiveCounterAttackUser;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTime > MAX_TIME) return;

        _currentTime += IsterTimeManager.enemyDeltaTime;
        if (_currentTime > MAX_TIME)
        {
            if (_effect)
                _effect.SetActive(false);
        }
    }

    public void TimerStart()
    {
        if (!_user) return;

        _currentTime = 0.0f;

        if (_effect)
            _effect.SetActive(true);
    }
}
