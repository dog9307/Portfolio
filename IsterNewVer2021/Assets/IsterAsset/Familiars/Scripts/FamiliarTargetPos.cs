using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamiliarTargetPos : MonoBehaviour
{
    private FamiliarManager _familiarManager;
    public FamiliarManager familiarManager { get { if (!_familiarManager) _familiarManager = FindObjectOfType<FamiliarManager>(); return _familiarManager; } }
    private FamiliarMoveController _relativeFamiliar;
    public FamiliarMoveController relativeFamiliar { get { if (!_relativeFamiliar) _relativeFamiliar = GetComponentInParent<FamiliarMoveController>(); return _relativeFamiliar; } }
    private BoxCollider2D _collider;
    public BoxCollider2D col { get { if (!_collider) _collider = GetComponent<BoxCollider2D>(); return _collider; } }

    private Rigidbody2D _rigid;
    public Rigidbody2D rigid { get { if (!_rigid) _rigid = GetComponent<Rigidbody2D>(); return _rigid; } }

    private PlayerMoveController _player;
    public PlayerMoveController player { get { if (!_player) _player = FindObjectOfType<PlayerMoveController>(); return _player; } }

    [SerializeField]
    private float _delayTimeMin;
    [SerializeField]
    private float _delayTimeMax;
    private float _currentDelayTime;
    private float _currentTime;

    [SerializeField]
    private LayerMask _collisionLayers;
    private RaycastHit2D[] _collisions;

    // Start is called before the first frame update
    void Start()
    {
        ReleaseParent();
        MoveTarget();
    }

    void ReleaseParent()
    {
        if (!transform.parent) return;

        BoxCollider2D familiar = relativeFamiliar.GetComponentInParent<BoxCollider2D>();

        col.size = familiar.size;
        col.offset = familiar.offset;

        name = transform.parent.name + "TargetPos";

        transform.parent = null;
    }

    void Update()
    {
        if (player)
        {
            float distance = Vector2.Distance(player.center, transform.position);
            if (distance > familiarManager.moveRange)
                MoveTarget();
        }

        _currentTime += IsterTimeManager.deltaTime;
        if (_currentTime >= _currentDelayTime)
            MoveTarget();
    }

    bool IsCollisionLayer(Collider2D col)
    {
        return (col.gameObject.layer == LayerMask.NameToLayer("DontMovable") ||
            col.gameObject.layer == LayerMask.NameToLayer("Gumddak"));
    }

    public void MoveTarget()
    {
        ReleaseParent();

        _currentTime = 0.0f;
        _currentDelayTime = Random.Range(_delayTimeMin, _delayTimeMax);

        Vector2 point = familiarManager.RandomPos();
        Vector2 center = player.center;
        Vector2 dir = CommonFuncs.CalcDir(center, point);
        float minDistance = Vector2.Distance(center, point);
        _collisions = Physics2D.RaycastAll(center, dir, minDistance, _collisionLayers);
        Vector2 targetPos = point;
        foreach (RaycastHit2D hit in _collisions)
        {
            if (hit.collider == col) continue;
            if (hit.collider.isTrigger) continue;

            if (minDistance > hit.distance)
            {
                minDistance = hit.distance;
                targetPos = hit.point;
            }
        }
        rigid.position = targetPos;

        Vector2 newPos = rigid.position;
        newPos.x -= col.offset.x;
        newPos.y -= col.offset.y;
        transform.position = newPos;
        relativeFamiliar.isArrive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FamiliarMoveController fam = collision.GetComponent<FamiliarMoveController>();
        if (fam != relativeFamiliar) return;

        relativeFamiliar.isArrive = true;
    }
}
