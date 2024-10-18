using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMarker : MonoBehaviour, IObjectCreator
{
    public float figure { get; set; }
    public GameObject effectPrefab { get; set; }

    public ActiveMarkerUser activeUser { get; set; }
    public bool isStack { get { if (!activeUser) return false; return activeUser.isStack; } }

    [SerializeField]
    private Transform _stackImageSet;
    [SerializeField]
    private GameObject[] _images;

    private int _currentStack;
    public int currentStack
    {
        get { return _currentStack; }

        set
        {
            if (value > _images.Length) return;

            Vector2 newPos = _stackImageSet.transform.localPosition;
            if (value > _currentStack)
                _images[_currentStack].SetActive(true);
            else if (value < _currentStack)
                _images[_currentStack - 1].SetActive(false);

            newPos.x = 0.1f - 0.1f * value;
            _stackImageSet.transform.localPosition = newPos;
            
            _currentStack = value;
        }
    }

    public PassiveMarkerUser passiveUser { get; set; }
    public GameObject marker { get; set; }
    public float triggeredDamageFigure { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if (!transform.parent) return;

        Vector3 newPos = transform.parent.position;

        //Sprite parentSprite = GetComponentInParent<SpriteRenderer>().sprite;
        //float height = parentSprite.bounds.extents.y;

        newPos.y += 1.5f;

        transform.position = newPos;

        effectPrefab = Resources.Load<GameObject>("Player/Bullets/DamageMarker/Prefab/MarkerBomb");

        if (activeUser)
        {
            if (!activeUser.isStack)
                _stackImageSet.gameObject.SetActive(false);
        }

        if (passiveUser)
        {
            marker = Resources.Load<GameObject>("Player/Bullets/DamageMarker/Prefab/MarkerBomb");
            if (passiveUser.isTimeBomb)
            {
                EventTimer timer = gameObject.AddComponent<EventTimer>();
                timer.totalTime = 3.0f;
                timer.AddEvent(DestroyMarker);
            }

            if (passiveUser.isRageMode)
                RageMarker();
        }
    }

    private void OnDestroy()
    {
        if (passiveUser)
        {
            if (passiveUser.isNewMarker)
            {
                if (!transform.parent) return;

                GameObject newMarker = Instantiate(marker);
                newMarker.transform.parent = transform.parent;
                newMarker.transform.localPosition = Vector3.zero;
            }
        }
    }

    public void RageMarker()
    {
        DebuffInfo debuffInfo = GetComponentInParent<DebuffInfo>();
        if (!debuffInfo) return;

        DebuffRage rage = new DebuffRage();
        rage.totalTime = 1.0f;

        debuffInfo.AddDebuff(rage);

        EventTimer timer = gameObject.AddComponent<EventTimer>();
        timer.totalTime = 1.0f;
        timer.AddEvent(RageMarker);
        timer.AddEvent(timer.DestroyTimer);
    }

    public void DestroyMarker()
    {
        GameObject bomb = CreateObject();
        MarkerBombDamager damager = bomb.GetComponent<MarkerBombDamager>();
        damager.target = transform.parent;

        Damage realDamage = damager.damage;

        if (activeUser)
        {
            if (isStack)
            {
                for (int i = 0; i < currentStack; ++i)
                    realDamage.additionalDamage += realDamage.damage * 0.2f;
            }
            realDamage.damageMultiplier = activeUser.damageMultiplier;
        }

        if (passiveUser)
        {
            damager.passiveUser = passiveUser;
            realDamage.additionalDamage = passiveUser.additionalDamage;

            if (passiveUser.isDamageProportion)
                realDamage.damageMultiplier *= ((int)(triggeredDamageFigure / 10.0f));
        }

        damager.damage = realDamage;

        Destroy(gameObject);
    }

    public GameObject CreateObject()
    {
        GameObject bomb = Instantiate(effectPrefab);
        bomb.transform.position = transform.parent.position;

        return bomb;
    }
}
