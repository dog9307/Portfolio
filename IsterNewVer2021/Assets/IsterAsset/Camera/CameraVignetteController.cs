using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraVignetteController : MonoBehaviour
{
    [SerializeField]
    private Volume _vol;
    private Vignette _vig;

    [SerializeField]
    private float _intensity = 2.0f;

    private PlayerMoveController _player;
    private BuffInfo _buff;

    void OnEnable()
    {
        if (_vig == null)
        {
            if (_vol)
                _vol.profile.TryGet<Vignette>(out _vig);
        }

        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();
        if (!_buff)
            _buff = _player.GetComponent<BuffInfo>();

        float intensity = _intensity / _buff.darknessLightMultiplier;
        _vig.intensity.max = intensity;
        _vig.intensity.value = intensity;
    }

    void Update()
    {
        if (!_player) return;

        Vector2 screePlayerPos = Camera.main.WorldToScreenPoint(_player.transform.position);

        float centerX = screePlayerPos.x / Screen.width;
        float centerY = screePlayerPos.y / Screen.height;

        _vig.center.value = new Vector2(centerX, centerY);
    }
}
