using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTorchBattleWall : MonoBehaviour
{
    [SerializeField]
    private DissolveApplier[] _dissolves;

    [SerializeField]
    private Collider2D[] _cols;

    [SerializeField]
    private float _dissolveDuration = 0.5f;

    void Start()
    {
        foreach (var d in _dissolves)
            d.currentFade = 0.0f;

        foreach (var c in _cols)
            c.enabled = false;
    }

    public void BattleStart()
    {
        StartCoroutine(Dissolve(0.0f, 1.0f));

        foreach (var c in _cols)
            c.enabled = true;
    }

    public void BattleEnd()
    {
        StartCoroutine(Dissolve(1.0f, 0.0f));

        foreach (var c in _cols)
            c.enabled = false;
    }

    IEnumerator Dissolve(float startFade, float endFade)
    {
        float currentTime = 0.0f;
        while (currentTime < _dissolveDuration)
        {
            float ratio = currentTime / _dissolveDuration;

            foreach (var d in _dissolves)
                d.currentFade = Mathf.Lerp(startFade, endFade, ratio);

            yield return null;

            currentTime += IsterTimeManager.enemyDeltaTime;
        }

        foreach (var d in _dissolves)
            d.currentFade = endFade;
    }
}
