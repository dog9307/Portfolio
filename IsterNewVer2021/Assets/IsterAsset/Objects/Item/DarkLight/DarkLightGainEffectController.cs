using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkLightGainEffectController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystemForceField _force;

    [SerializeField]
    private float _delayTime = 0.5f;

    private static PlayerMoveController _player;

    // Start is called before the first frame update
    void Start()
    {
        _force.endRange = 0.0f;

        StartCoroutine(Delay());
    }

    void Update()
    {
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        _force.transform.position = _player.transform.position;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Delay()
    {
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        yield return new WaitForSeconds(_delayTime);

        _force.endRange = 1000.0f;
    }
}
