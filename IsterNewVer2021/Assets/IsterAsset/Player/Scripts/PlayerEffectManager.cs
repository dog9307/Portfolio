using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : MonoBehaviour
{
    // test
    // effect manager 자체 다시 설계해보기
    // 현재 : 이펙트들 전부 플레이어에 때려박고 SetActive만 꺼뒀다가 이펙트 필요할 때 켜기
    // => 변경 : ???

    [SerializeField]
    private GameObject[] _effects;

    void Start()
    {
        for (int i = 0; i < _effects.Length; ++i)
            _effects[i].SetActive(false);
    }
    
    public void EffectOn(string name)
    {
        FindEffect(name).SetActive(true);
    }

    public void EffectOff(string name)
    {
        FindEffect(name).SetActive(false);
    }

    public GameObject FindEffect(string name)
    {
        GameObject findingEffect = null;
        foreach (var effect in _effects)
        {
            if (effect.name.Equals(name))
            {
                findingEffect = effect;
                break;
            }
        }
        return findingEffect;
    }
}
