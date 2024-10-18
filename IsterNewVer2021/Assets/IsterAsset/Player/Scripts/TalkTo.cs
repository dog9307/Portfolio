using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class TalkTo : MonoBehaviour
{
    private BoxCollider2D _collider;

    [SerializeField]
    [Range(0.1f, 1.0f)]private float _talkRange;
    public float talkRange { get { return _talkRange; } set { _talkRange = value; } }

    [SerializeField]
    private PlayerInteractiveIcon _interactionIcon;
    [SerializeField]
    private Image _cover;

    [SerializeField]
    private LayerMask _wallLayers;
    private ContactFilter2D _fillter;

    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();

        _fillter.layerMask = _wallLayers;
        _fillter.useTriggers = false;
        _fillter.useDepth = false;
        _fillter.useOutsideDepth = false;
        _fillter.useNormalAngle = false;
        _fillter.useOutsideNormalAngle = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = _collider.bounds.center;
        Vector2 size = _collider.bounds.size;
        size.x += _talkRange * transform.localScale.x;
        size.y += _talkRange * transform.localScale.y;
        Collider2D[] objects = Physics2D.OverlapCircleAll(point, size.x);

        bool isOn = false;
        foreach (Collider2D obj in objects)
        {
            TalkFrom talk = obj.GetComponent<TalkFrom>();
            if (talk)
            {
                if (talk.enabled && obj.enabled)
                {
                    float distance = float.MaxValue;
                    RaycastHit2D[] hits = Physics2D.RaycastAll(_collider.bounds.center, CommonFuncs.CalcDir(_collider, obj), talkRange, _wallLayers);
                    bool isHit = true;
                    if (hits != null)
                    {
                        foreach (var h in hits)
                        {
                            if (!h) continue;
                            if (h.collider.isTrigger) continue;

                            if (h.collider == obj)
                            {
                                distance = h.distance;
                                break;
                            }
                        }

                        foreach (var h in hits)
                        {
                            if (!h) continue;
                            if (h.collider.isTrigger) continue;

                            if (h.collider != obj)
                            {
                                if (h.distance < distance)
                                {
                                    isHit = false;
                                    break;
                                }
                            }
                        }
                    }

                    if (!isHit) continue;

                    isOn = true;

                    if (typeof(TalkFromStayKey).IsInstanceOfType(talk))
                    {
                        _cover.enabled = true;
                        TalkFromStayKey stay = talk as TalkFromStayKey;
                        if (KeyManager.instance.IsStayKeyDown("TalkTo"))
                            stay.UpdateRatio(IsterTimeManager.deltaTime);
                        else
                            stay.ResetRatio();

                        _cover.fillAmount = 1.0f - stay.ratio;

                        if (stay.ratio >= 1.0f)
                            //talk.Talk(GetComponent<PlayerInfo>());
                            talk.Talk();
                    }
                    else
                    {
                        _cover.enabled = false;
                        if (KeyManager.instance.IsOnceKeyDown("TalkTo"))
                            //talk.Talk(GetComponent<PlayerInfo>());
                            talk.Talk();
                    }

                    break;
                }
            }
        }

        _interactionIcon.isShow = isOn;

        //if (KeyManager.instance.IsOnceKeyDown("TalkTo"))
        //{
        //    foreach (Collider2D obj in objects)
        //    {
        //        TalkFrom talk = obj.GetComponent<TalkFrom>();
        //        if (talk)
        //        {
        //            if (talk.enabled && obj.enabled)
        //            {
        //                talk.Talk(GetComponent<PlayerInfo>());
        //                return;
        //            }
        //        }
        //    }
        //}
    }

    private void OnDrawGizmosSelected()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        Vector2 point = col.bounds.center;
        Vector2 size = col.bounds.size;
        size.x += _talkRange * transform.localScale.x;
        size.y += _talkRange * transform.localScale.y;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(point, size.x);
    }
}
