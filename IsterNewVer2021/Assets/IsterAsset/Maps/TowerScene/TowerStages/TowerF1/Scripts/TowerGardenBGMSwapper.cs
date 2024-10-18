using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGardenBGMSwapper : MonoBehaviour
{
    [SerializeField]
    private AudioClip _bgm;
    [SerializeField]
    [Range(0.0f, 1.0f)] private float _bgmVolume = 1.0f;

    [SerializeField]
    private AudioClip _amb;
    [SerializeField]
    [Range(0.0f, 1.0f)] private float _ambVolume = 1.0f;

    [SerializeField]
    private Collider2D _col;

    [SerializeField]
    private Damagable _target;

    private bool _isBattleEnd;

    public void BGMChange()
    {
        if (SoundSystem.instance)
            SoundSystem.instance.PlayBGM(_bgm, _bgmVolume, 1.0f, true);

        if (SoundSystem.instance)
            SoundSystem.instance.PlayAmbient(_amb, 0, _ambVolume);
    }

    private void OnEnable()
    {
        if (_isBattleEnd) return;
        if (_col.isActiveAndEnabled) return;

        BGMChange();
    }
    private void Start()
    {
        _isBattleEnd = false;
    }

    void Update()
    {
        if (_isBattleEnd) return;
        if (!_target.isDie) return;

        _isBattleEnd = true;

        if (SoundSystem.instance)
            SoundSystem.instance.PlayBGM(null, _bgmVolume, 0.5f, true);
    }
}
