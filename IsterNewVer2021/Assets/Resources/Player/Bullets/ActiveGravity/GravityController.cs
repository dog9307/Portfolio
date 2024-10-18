using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour, IObjectCreator
{
    [SerializeField]
    private Transform _center;

    [SerializeField]
    private GameObject _rageField;
    [SerializeField]
    private GameObject _electricField;

    private bool _isEnd;

    public ActiveGravityUser user { get; set; }

    [SerializeField]
    private GameObject _prefab;
    public GameObject effectPrefab { get { return _prefab; } set { _prefab = value; } }

    private List<Movable> _affectedObjects = new List<Movable>();
    private EventTimer _timer;

    [SerializeField]
    private ParticleSystem _effect;

    void Start()
    {
        _timer = GetComponentInParent<EventTimer>();
        _timer.totalTime += user.additionalTime;
        _timer.AddEvent(this.Destroy);

        _rageField.SetActive(user.isRageMode);
        _electricField.SetActive(user.isElectricMode);
    }

    private void OnDestroy()
    {
        if (user.isEndBomb)
        {
            GameObject newBomb = CreateObject();
            GravityEndBombDamager damager = newBomb.GetComponent<GravityEndBombDamager>();
            damager.user = user;
        }
    }

    public void Destroy()
    {
        //Animator anim = GetComponent<Animator>();
        //anim.SetTrigger("skillEnd");

        foreach (Movable move in _affectedObjects)
        {
            if (!move) continue;

            move.additionalForce = Vector2.zero;
        }

        _affectedObjects.Clear();

        if (_effect)
            _effect.Stop();
    }

    private void LateUpdate()
    {
        foreach (Movable move in _affectedObjects)
        {
            Vector2 dir = CommonFuncs.CalcDir(move, _center.position);
            move.additionalForce = dir * user.totalForce;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Movable movable = collision.GetComponent<Movable>();
        if (!movable)
        {
            movable = collision.GetComponentInChildren<Movable>();
            if (!movable) return;
        }

        if (!movable.isCanAffectedGravity) return;
        if (_affectedObjects.Contains(movable)) return;

        _affectedObjects.Add(movable);

        DebuffInfo debuffInfo = collision.GetComponent<DebuffInfo>();
        if (debuffInfo)
        {
            if (user.isRageMode)
            {
                DebuffRage debuff = new DebuffRage();
                debuff.totalTime = _timer.leftTime;

                debuffInfo.AddDebuff(debuff);
            }

            if (user.isElectricMode)
            {
                DebuffElectric debuff = new DebuffElectric();
                debuff.totalTime = _timer.leftTime;

                debuffInfo.AddDebuff(debuff);
            }
        }
    }

    void ObjectRelease(Movable move)
    {
        if (!move) return;
        if (!_affectedObjects.Contains(move)) return;

        move.additionalForce = Vector2.zero;
        _affectedObjects.Remove(move);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Movable movable = collision.GetComponent<Movable>();
        if (!movable)
        {
            movable = collision.GetComponentInChildren<Movable>();
            if (!movable) return;
        }

        ObjectRelease(movable);
    }

    public GameObject CreateObject()
    {
        GameObject bomb = Instantiate(effectPrefab);
        bomb.transform.position = transform.position;

        return bomb;
    }
}
