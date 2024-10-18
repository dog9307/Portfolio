using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitEffectManager : MonoBehaviour
{
    #region SINGLETON
    static private HitEffectManager _instance;
    static public HitEffectManager instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<HitEffectManager>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "KeyManager";
                _instance = container.AddComponent<HitEffectManager>();
            }
        }
    }
    #endregion

    [HideInInspector]
    [SerializeField]
    private FadingGuideUI _fading;

    [HideInInspector]
    [SerializeField]
    private Image _image;

    [SerializeField]
    private HitInfoDictionary _effects = new HitInfoDictionary();

    [SerializeField]
    private float _effectTime = 0.3f;
    [SerializeField]
    private float _effectViewTime = 0.1f;

    void Start()
    {
        _fading.totalFadeTime = _effectTime;
    }

    public void StartEffect(string name)
    {
        if (!_effects.ContainsKey(name)) return;

        _image.sprite = _effects[name].sprite;

        StopAllCoroutines();
        StartCoroutine(Effecting());
    }

    IEnumerator Effecting()
    {
        _fading.StartFading(1.0f, _effectTime);

        yield return new WaitForSeconds(_effectViewTime);

        _fading.StartFading(0.0f, _effectTime);
    }
}

[System.Serializable]
public class HitEffectInfo
{
    public Sprite sprite;
}

[System.Serializable]
public class HitInfoDictionary : SerializableDictionary<string, HitEffectInfo> { };
