using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderTrigger : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;

    [SerializeField]
    private ColliderTriggerCondition _condition;

    [SerializeField]
    private bool _isEnalbedOnEnable = false;
    [SerializeField]
    private Collider2D _relativeCol;

    private void OnEnable()
    {
        if (!_relativeCol)
            _relativeCol = GetComponent<Collider2D>();

        if (_relativeCol)
        {
            if (!_relativeCol.enabled)
                _relativeCol.enabled = _isEnalbedOnEnable;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_condition)
        {
            if (!_condition.IsCanTrigger())
                return;
        }

        PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
        if (!player) return;

        if (onTriggerEnter != null)
            onTriggerEnter.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_condition)
        {
            if (!_condition.IsCanTrigger())
                return;
        }

        PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
        if (!player) return;

        if (onTriggerExit != null)
            onTriggerExit.Invoke();
    }
}
