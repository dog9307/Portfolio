using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceDistanceVolume : MonoBehaviour
{
    [SerializeField]
    private AudioSource _loop;
    [SerializeField]
    private float _maxDistance = 20.0f;
    private PlayerMoveController _player;

    [SerializeField]
    private float _volumeSpeed = 0.3f;

    [SerializeField]
    private float _minVolume = 0.0f;
    [SerializeField]
    private float _maxVolume = 1.0f;

    private float _targetVolume;

    void Start()
    {
        _loop.volume = _minVolume;
    }

    void Update()
    {
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();
        if (!_player) return;

        float distance = Vector2.Distance(transform.position, _player.transform.position);
        if (distance > _maxDistance)
            _targetVolume = 0.0f;
        else
        {
            float ratio = (1.0f - (distance / _maxDistance));
            _targetVolume = Mathf.Lerp(_minVolume, _maxVolume, ratio);
        }

        _loop.volume = Mathf.Lerp(_loop.volume, _targetVolume, _volumeSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _maxDistance / 2.0f);
    }
}
