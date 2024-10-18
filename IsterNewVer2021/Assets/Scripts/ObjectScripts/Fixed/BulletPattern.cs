using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct PATTERNINFO
{
    public string _bulletName;
    public Vector3 _direction;
    public Vector3 _position;
    public int _attackCount;
}
public class BulletPattern : MonoBehaviour
{
    static private BulletPattern _instance;
    static public BulletPattern instance { get { return _instance; } }
    
    PATTERNINFO _patternInfo;

    [Multiline(5)] public string AttackList;

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<BulletPattern>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "BulletPattern";
                _instance = container.AddComponent<BulletPattern>();
            }
        }

        DontDestroyOnLoad(BulletPattern.instance);
    }

    public void BasicMeleeAttack(PATTERNINFO patterninfo)
    {
        //GameObject bullet = new GameObject();
        var bullet = Instantiate(Resources.Load<GameObject>("Enemy/Bullets/" + patterninfo._bulletName));
        bullet.name = patterninfo._bulletName;
        bullet.transform.parent = null;
        var direction = (patterninfo._direction).normalized;
        bullet.transform.position = patterninfo._position;
        bullet.SendMessage("BasicMeleeAttack", direction);
    }
    public void BasicRangeAttack(PATTERNINFO patterninfo)
    {
        //GameObject bullet = new GameObject();
        var bullet = Instantiate(Resources.Load<GameObject>("Enemy/Bullets/" + patterninfo._bulletName));
        bullet.name = patterninfo._bulletName;
        bullet.transform.parent = null;
        var direction = (patterninfo._direction).normalized;
        bullet.transform.position = patterninfo._position;
        bullet.SendMessage("BasicRangeAttack", direction);

    }
    public void BomberRangeAttack(PATTERNINFO patterninfo)
    {
        GameObject bullet = new GameObject();
        bullet = Instantiate(Resources.Load<GameObject>("Enemy/Bullets/" + patterninfo._bulletName));
        bullet.transform.parent = null;
        bullet.name = patterninfo._bulletName;
        var direction = (patterninfo._direction).normalized;
        bullet.transform.position = patterninfo._position;
        bullet.SendMessage("BomberRangeAttack", direction);
    }
    public void SemiCircleRangeAttack(PATTERNINFO patterninfo)
    {
        int count = 0;
        int halfAttackcount = patterninfo._attackCount / 2;

        float dot = Vector3.Dot(Vector2.right, patterninfo._direction);
        float angleOrigin = Mathf.Acos(dot);
        if (patterninfo._direction.y < 0.0f) angleOrigin = Mathf.PI *2-angleOrigin;

        GameObject[] bullet = new GameObject[patterninfo._attackCount];

        while (count < patterninfo._attackCount )
        {
            var direction = (patterninfo._direction).normalized;
            float angle = angleOrigin - 15 * halfAttackcount * Mathf.Deg2Rad + 15 * count * Mathf.Deg2Rad;

            bullet[count] = Instantiate(Resources.Load<GameObject>("Enemy/Bullets/" + patterninfo._bulletName), null);
            bullet[count].name = patterninfo._bulletName;
            bullet[count].transform.position = patterninfo._position;
            direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f).normalized;
            bullet[count].SendMessage("BasicRangedAttack", direction);

            count++;
        }
    }
    //최대 36개가 이쁘다.
    public void CircleRangeAttack(PATTERNINFO patterninfo)
    {
        int count = 0;
        GameObject[] bullet = new GameObject[patterninfo._attackCount];

        while (count < patterninfo._attackCount)
        {
            bullet[count] = Instantiate(Resources.Load<GameObject>("Enemy/Bullets/" + patterninfo._bulletName), null);
            bullet[count].transform.parent = null;
            bullet[count].name = patterninfo._bulletName;
            var direction = new Vector3(Mathf.Cos(Mathf.PI * 2 * count / patterninfo._attackCount),
                Mathf.Sin(Mathf.PI * 2 * count / patterninfo._attackCount), 0.0f);

            direction = direction.normalized;

            bullet[count].transform.position = patterninfo._position;
            bullet[count].SendMessage("BasicRangedAttack", direction);

            count++;
        }
    }
    public void BomberBullet (string name, Vector3 pos)
    {
        int count = 0;
        int attackcount = 10;
        GameObject[] bullet = new GameObject[attackcount];

        while (count < attackcount)
        {
            bullet[count] = Instantiate(Resources.Load<GameObject>("Enemy/Bullets/" + name));
            bullet[count].transform.parent = null;
            bullet[count].name = name;
            var direction = new Vector3(Mathf.Cos(Mathf.PI * 2 * count / 10),
                Mathf.Sin(Mathf.PI * 2 * count / 10), 0.0f);

            bullet[count].transform.position = pos;
            bullet[count].SendMessage("BasicRangedAttack", direction);

            count++;
        }
    }
    public void BomberMeleeAttack(PATTERNINFO patterninfo)
    {
        //GameObject bullet = new GameObject();
        var bullet = Instantiate(Resources.Load<GameObject>("Enemy/Bullets/" + patterninfo._bulletName));
        bullet.name = patterninfo._bulletName;
        bullet.transform.parent = null; 
        var direction = (patterninfo._direction).normalized;
        bullet.transform.position = patterninfo._position;
        bullet.SendMessage("BomberMeleeAttack", direction);
    }
    public void RazerAttack(string name, Vector3 dir, Vector3 pos,int attackcount)
    {       
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * _turnSpeed);
    }
}
