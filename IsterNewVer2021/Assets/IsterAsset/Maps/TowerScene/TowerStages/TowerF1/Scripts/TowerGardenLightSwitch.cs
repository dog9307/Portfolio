using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGardenLightSwitch : TalkFrom
{
    [SerializeField]
    private FlowerType _type;

    [SerializeField]
    private TowerGardenMoonLight _moonLight;

    [SerializeField]
    private ParticleSystem _effect;

    [SerializeField]
    private Collider2D _tempWall;

    [SerializeField]
    private LightPuzzleLayerController[] _puzzles;

    private bool _isFirstSkip = true;
    private void OnEnable()
    {
        if (_isFirstSkip)
        {
            _isFirstSkip = false;
            return;
        }

        if (!_moonLight)
            _moonLight = FindObjectOfType<TowerGardenMoonLight>();

        if (!_moonLight) return;

        if (_tempWall)
        {
            if (FlowerType.MoreDamage <= _moonLight.currentType && _moonLight.currentType <= FlowerType.Slow)
                _tempWall.enabled = false;
        }
    }

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        if (!_moonLight)
            _moonLight = FindObjectOfType<TowerGardenMoonLight>();

        if (!_moonLight) return;

        if (_moonLight.currentType == _type) return;

        _moonLight.LightColorChange(_type);

        if (_tempWall)
            _tempWall.enabled = false;

        if (_sfx)
            _sfx.PlaySFX(_sfxName);

        if (_effect)
            _effect.Play();

        if (_puzzles != null)
        {
            foreach (var p in _puzzles)
                p.LightChange(_type);
        }
    }
}
