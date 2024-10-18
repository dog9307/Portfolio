using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DongleYeeSlowBullet : MonoBehaviour
{
    Collider2D _collider;

    PlayerMoveController _player;

    public float _duration;
    public float _slowPower;
    private float _slowSpeed = 0.0f;

    private float _currentTime;

    [SerializeField]
    private ParticleSystem _effect;
    // Start is called before the first frame update
    private void OnEnable()
    {
        _currentTime = 0;
        //_collider.enabled = true;
    }

    void Start()
    {
        if (!_player)
        {
            _player = FindObjectOfType<PlayerMoveController>();
        }
        _collider = GetComponent<Collider2D>();
        //_collider.isTrigger = true;

        _currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += IsterTimeManager.deltaTime;

        if(_currentTime > _duration)
        {
            _effect.Stop();
            _collider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("PLAYER"))
        {
            _slowSpeed = collision.GetComponent<DebuffInfo>().slowMultiplier * _slowPower;

            if (_slowSpeed > 0.0f)
            {
                collision.GetComponent<DebuffInfo>().slowDecrease += _slowSpeed;
            }

            if (!_player)
            {
                _player = FindObjectOfType<PlayerMoveController>();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("PLAYER"))
        {
            if (_slowSpeed > 0.0f)
            {
                collision.GetComponent<DebuffInfo>().slowDecrease -= _slowSpeed;
            }
            _player = null;
        }
    }
    private void OnDestroy()
    {
        if (_player)
            _player.GetComponent<DebuffInfo>().RemoveSlow(_slowPower);
    }
}
