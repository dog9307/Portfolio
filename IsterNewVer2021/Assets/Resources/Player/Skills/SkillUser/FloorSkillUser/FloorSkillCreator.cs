using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSkillCreator : MonoBehaviour, IObjectCreator
{
    [SerializeField]
    protected GameObject _skillPos;

    [SerializeField]
    protected FloorSkillUser _owner;
    
    public GameObject effectPrefab { get; set; }

    private PlayerMoveController _player;

    private bool _isFollowMouse = false;

    private Transform _parent;

    void Start()
    {
        _parent = transform.parent;
    }

    public void ScaleStart()
    {
        ScaleUp su;
        Vector3 scale = scale = new Vector3(_owner.skill.scale, _owner.skill.scale, 1.0f);
        su = _skillPos.GetComponent<ScaleUp>();
        su.targetScale = scale;
        su.StartEffect();

        _player = FindObjectOfType<PlayerMoveController>();

        transform.parent = _parent;
        _isFollowMouse = true;

        Update();
    }

    public void ScaleEnd()
    {
        ScaleUp su;
        Vector3 scale = new Vector3(_owner.skill.scale, _owner.skill.scale, 1.0f);
        su = _skillPos.GetComponent<ScaleUp>();
        su.TimeReset();

        transform.parent = null;
        _isFollowMouse = false;
    }

    public void EffectPos(Vector3 pos)
    {
        _skillPos.transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isFollowMouse) return;

        transform.position = _player.center;
        
        Vector2 creatorPos = _player.center;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = CommonFuncs.CalcDir(creatorPos, mousePos);
        
        float minDistance = Vector2.Distance(_player.center, mousePos);
        Vector3 fallowPos = creatorPos + dir.normalized * minDistance;
        RaycastHit2D[] hits = Physics2D.RaycastAll(_player.center, dir, minDistance);
        foreach (RaycastHit2D hit in hits)
        {
            if (IsHitLayer(hit))
            {
                if (hit.distance < minDistance)
                {
                    fallowPos = hit.point;
                    minDistance = hit.distance;
                }
            }
        }

        _skillPos.transform.position = fallowPos;
    }

    bool IsHitLayer(RaycastHit2D hit)
    {
        return hit.transform.gameObject.layer == LayerMask.NameToLayer("DontMovable") ||
            hit.transform.gameObject.layer == LayerMask.NameToLayer("Gumddak");
    }

    public virtual GameObject CreateObject()
    {
        GameObject newBullet = GameObject.Instantiate(effectPrefab);
        newBullet.transform.position = _skillPos.transform.position;
        newBullet.transform.localScale = new Vector3(_owner.skill.scale, _owner.skill.scale, _owner.skill.scale);

        return newBullet;
    }
}
