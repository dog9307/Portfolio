using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkLightDropEnemy : MonoBehaviour, IObjectCreator
{
    [SerializeField]
    private GameObject _darkLightPrefab;
    [SerializeField]
    private Transform _dropPos;
    [SerializeField]
    private int _darkLightFigure = 100;

    public GameObject effectPrefab { get { return _darkLightPrefab; } set { _darkLightPrefab = value; } }

    [SerializeField]
    private Damagable _damagable;

    [SerializeField]
    private DropRewardCondition _condition;

    void Start()
    {
        if (!_condition) return;

        if (_condition.IsChangeDropItem())
            effectPrefab = _condition.newReward;
    }

    void Update()
    {
        if (!_damagable) return;

        if (_damagable.isDie)
        {
            GameObject newDark = CreateObject();

            DarkLightTalkFrom talkFrom = newDark.GetComponent<DarkLightTalkFrom>();
            if (talkFrom)
            {
                talkFrom.figure = _darkLightFigure;
                //talkFrom.Talk(null);
                talkFrom.Talk();
            }

            Destroy(this);
        }
    }

    public GameObject CreateObject()
    {
        GameObject newDark = Instantiate(_darkLightPrefab);
        newDark.transform.position = _dropPos.position;

        return newDark;
    }
}
