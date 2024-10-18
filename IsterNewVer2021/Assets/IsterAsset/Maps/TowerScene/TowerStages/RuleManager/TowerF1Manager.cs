using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerF1Manager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _flowers;

    [SerializeField]
    private List<GlowableObject> _patterGlows;

    [SerializeField]
    private List<GlowSync> _entrances;

    private int _keyFlower;
    [SerializeField]
    private GlowableObject[] _doors;

    [SerializeField]
    private Transform _effectTargetPos;
    public Transform effectTargetPos { get { return _effectTargetPos; } }

    public TowerF1Flower keyFlower { get; set; }

    public static float flowerSValue = 0.3f;

    [SerializeField]
    private Transform[] _sessackGroups;

    // Start is called before the first frame update
    void Start()
    {
        ShuffleFlowers();
        SyncPatterns();

        _keyFlower = Random.Range(0, _flowers.Count);
        KeySetting();
    }

    void ShuffleFlowers()
    {
        for (int i = 0; i < 777; ++i)
        {
            int sour = Random.Range(0, _flowers.Count);
            int dest = Random.Range(0, _flowers.Count);

            Transform tempParent = _flowers[sour].parent;
            _flowers[sour].parent = _flowers[dest].parent;
            _flowers[dest].parent = tempParent;

            _flowers[sour].localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            _flowers[dest].localPosition = new Vector3(0.0f, 0.0f, 0.0f);

            Transform temp = _flowers[sour];
            _flowers[sour] = _flowers[dest];
            _flowers[dest] = temp;
        }
    }

    void SyncPatterns()
    {
        for (int i = 0; i < _patterGlows.Count; ++i)
        {
            GlowableObject flower = _flowers[i].GetComponentInChildren<GlowableObject>();
            if (flower)
            {
                _patterGlows[i].color = flower.color;
            }
            else
            {
                _patterGlows[i].color = Color.black;
            }
        }

        for (int i = 0; i < _patterGlows.Count; ++i)
            _entrances[i].sour = _patterGlows[i];

        SyncSessacks();
    }

    void SyncSessacks()
    {
        for (int i = 0; i < _sessackGroups.Length; ++i)
        {
            Transform group = _sessackGroups[i];

            JustRunAwaySessackyeeController[] sessacks = group.GetComponentsInChildren<JustRunAwaySessackyeeController>();
            foreach (var s in sessacks)
            {
                Color color = _patterGlows[i].color;
                color.a = 1.0f;
                s.ApplyColor(color);
            }
        }
    }

    void KeySetting()
    {
        Color color = _patterGlows[_keyFlower].color;
        foreach (var door in _doors)
        {
            door.ApplyColor(color);

            if (color == Color.black)
                door.GetComponent<SpriteRenderer>().color = Color.gray;
            else
            {
                Color newColor = color;
                newColor.a = 1.0f;

                float h = 0.0f;
                float s = 0.0f;
                float v = 0.0f;

                Color.RGBToHSV(newColor, out h, out s, out v);
                s = TowerF1Manager.flowerSValue;

                newColor = Color.HSVToRGB(h, s, v);

                door.GetComponent<SpriteRenderer>().color = newColor;
            }
        }

        keyFlower = _flowers[_keyFlower].GetComponentInChildren<TowerF1Flower>();
    }

    public List<TowerF1BuffEffectController> effects { get; set; } = new List<TowerF1BuffEffectController>();
    public void AddBuffEffect(TowerF1BuffEffectController effect)
    {
        effects.Add(effect);
    }
}
