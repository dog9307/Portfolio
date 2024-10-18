using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingStackTimer : MonoBehaviour
{
    private DebuffInfo _debuffInfo;

    public TracingArrowUser user { get; set; }
    public int maxStack { get { return user.maxStack; } }

    private int _currentStack;
    public int currentStack
    {
        get { return _currentStack; }

        set
        {
            if (value <= 0)
            {
                DestroyTimer();
                return;
            }
            else if (value > maxStack)
            {
                _currentTime = 0.0f;
                return;
            }

            Vector2 newPos = _stackImageSet.transform.localPosition;
            if (value > _currentStack)
                _images[_currentStack].SetActive(true);
            else if (value < _currentStack)
                _images[_currentStack - 1].SetActive(false);

            newPos.x = 0.1f - 0.1f * value;
            _stackImageSet.transform.localPosition = newPos;

            _currentTime = 0.0f;
            _currentStack = value;
        }
    }

    [SerializeField]
    private Damage _damage;

    public Damage damage { get { return _damage; } set { _damage = value; } }

    [SerializeField]
    private float _removeTime;
    private float _currentTime;

    [HideInInspector]
    [SerializeField]
    private Transform _stackImageSet;
    [HideInInspector]
    [SerializeField]
    private GameObject[] _images;

    void Start()
    {
        _currentTime = 0.0f;
        currentStack = 1;
    }

    void Update()
    {
        _currentTime += IsterTimeManager.enemyDeltaTime;
        if (_currentTime >= _removeTime)
            --currentStack;
    }

    public void DestroyTimer()
    {
        Destroy(gameObject);
    }
}
