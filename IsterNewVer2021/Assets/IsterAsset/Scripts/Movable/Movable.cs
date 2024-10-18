using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Movable : MonoBehaviour
{
    protected Rigidbody2D _rigid;
    protected Collider2D _collider;

    protected Damagable _damagable;

    protected Vector2 _targetVelocity;
    public Vector2 targetVelocity { get { return _targetVelocity; } set { _targetVelocity = value; } }
    protected Vector2 _prevPosition;
    public Vector2 prevPosition { get { return _prevPosition; } set { _prevPosition = value; } }

    public Vector3 center { get { return _collider.bounds.center; } }

    protected DebuffInfo _debuffInfo;

    [SerializeField]
    protected float _speed;
    public virtual float speed
    {
        get
        {
            if (!_debuffInfo) return _speed;

            return _speed * _debuffInfo.slowMultiplier;
        }

        set { _speed = value; }
    }
    public float GetSpeed() { return _speed; }

    public virtual bool isHide { get; set; }

    protected const float MIN_MOVE_DISTANCE = 0.01f;

    protected float _currentTime;

    [SerializeField]
    protected bool isTrigerringCollider = false;

    [SerializeField]
    protected bool _isCanAffectedGravity = true;
    public virtual bool isCanAffectedGravity { get { return _isCanAffectedGravity; } set { _isCanAffectedGravity = value; } }
    public Vector2 additionalForce { get; set; }
    [SerializeField]
    protected bool _isBoss = false;
    public bool isBoss { get { return _isBoss; } set { _isBoss = value; } }

    [SerializeField]
    protected bool _isStaticMovable = false;

    protected void ComponentSetting()
    {
        _collider = GetComponent<Collider2D>();
        if (!_collider)
            _collider = gameObject.AddComponent<BoxCollider2D>();
        _collider.isTrigger = isTrigerringCollider;

        _rigid = GetComponent<Rigidbody2D>();
        if (!_rigid)
            _rigid = gameObject.AddComponent<Rigidbody2D>();

        if (!_isStaticMovable)
            _rigid.bodyType = RigidbodyType2D.Dynamic;
        else
            _rigid.bodyType = RigidbodyType2D.Static;

        _rigid.gravityScale = 0.0f;
        _rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        _rigid.sharedMaterial = Resources.Load<PhysicsMaterial2D>("Scripts/Movable/MovableMaterial");
    }

    void OnEnable()
    {
        ComponentSetting();

        _damagable = GetComponent<Damagable>();
        _debuffInfo = GetComponent<DebuffInfo>();

        _currentTime = 0.0f;
    }

    void Start()
    {
        isHide = false;
    }
    
    public bool IsCanMoveWithDamagable()
    {
        if (!_damagable) return true;

        return (!_damagable.isHurt && !_damagable.isDie && !_damagable.isKnockback);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCanMoveWithDamagable())
        {
            _targetVelocity = Vector2.zero;
            ComputeVelocity();

            ParryingStunController stun = GetComponentInChildren<ParryingStunController>();
            if (stun)
            {
                if (stun.isStun)
                    _targetVelocity = Vector2.zero;
            }

            _rigid.velocity = _targetVelocity;

            if (isCanAffectedGravity)
            {
                Vector2 realForce = (isBoss ? additionalForce * 0.5f : additionalForce);
                _rigid.velocity += realForce;
            }
        }
        _prevPosition = _rigid.position;
    }

    protected abstract void ComputeVelocity();
}
