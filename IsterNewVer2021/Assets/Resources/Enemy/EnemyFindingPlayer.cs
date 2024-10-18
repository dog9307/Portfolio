using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyFindingPlayer : MonoBehaviour
{
    [SerializeField]
    bool _fieldEnemy;


    public bool _findingPlayer;
    private Collider2D _collider;
    //일단 방울이 한정
    
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
        _findingPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_fieldEnemy)
        {
            _findingPlayer = true;
        }
        else return;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("PLAYER") && !_findingPlayer)
        {
            _findingPlayer = true;
            _collider.enabled = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("ENEMY") && collision.gameObject.GetComponent<EnemyFindingPlayer>()._findingPlayer)
        {
            this._findingPlayer = true;
            _collider.enabled = false;
        }
    }
}
