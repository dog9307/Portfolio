using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FieldFlower : TowerBattleEventController, IObjectCreator
{
    public GameObject effectPrefab { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    [SerializeField]
    private ParticleSystem _effect;
    [SerializeField]
    private DisposableDestroyedObject _disposable;

    public GameObject CreateObject()
    {
        _effect.Play();

        _disposable.UseObject();

        return null;
    }

    [SerializeField]
    private ParticleSystem _shield;

    public void BattleIng()
    {
        if (!_isBattleStart)
            BattleStart();
        else
            _sequence.SpawnSignal();
    }
}
