using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackTimer : MonoBehaviour, IObjectCreator
{
    public GameObject effectPrefab { get; set; }
    public ActiveRangeAttackUser user { get; set; }

    public float totalTime { get { return user.delayTime; } }
    private float _currentTime;

    private SpriteRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        effectPrefab = Resources.Load<GameObject>("Player/Bullets/RangeAttack/Prefab/RangeAttack");

        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        _currentTime = 0.0f;
    }

    void Update()
    {
        _currentTime += IsterTimeManager.enemyDeltaTime;
        if (_currentTime >= totalTime)
            Destroy();

        if (totalTime != 0.0f)
        {
            float percent = _currentTime / totalTime;
            _renderer.color = new Color(1.0f, 1.0f, 1.0f, percent);
        }
    }
    
    public void Destroy()
    {
        CreateObject();
        Destroy(gameObject);
    }

    public GameObject CreateObject()
    {
        GameObject newBullet = GameObject.Instantiate(effectPrefab);
        newBullet.transform.position = transform.position;

        newBullet.transform.localScale = new Vector3(user.scaleFactor, user.scaleFactor, user.scaleFactor);

        RangeAttackDamager damager = newBullet.GetComponent<RangeAttackDamager>();
        damager.user = user;

        Damage realDamage = damager.damage;
        realDamage.damageMultiplier = totalTime + 1.0f;
        damager.damage = realDamage;

        return newBullet;
    }
}
