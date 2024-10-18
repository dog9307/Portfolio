using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerF1FlowerTalkFrom : TowerRuleItemTalkFrom, IObjectCreator
{
    [SerializeField]
    private GlowableObject _glow;
    private FlowerType _type;

    [SerializeField]
    private SpriteRenderer _sprite;

    [SerializeField]
    private GameObject _effect;
    public GameObject effectPrefab { get { return _effect; } set { _effect = value; } }

    [SerializeField]
    private GameObject _buffEffectPrefab;

    private static int _effectCreateCount = 0;

    public TowerGardenFlowerDropper dropper { get; set; }

    public GameObject CreateObject()
    {
        if (!_effect) return null;

        GameObject newEffect = Instantiate(_effect);
        newEffect.transform.position = transform.position;

        return newEffect;
    }

    public void Init(FlowerType type, Color color, GameObject buffEffect)
    {
        _type = type;

        if (_glow)
            _glow.ApplyColor(color);

        if (color != Color.black)
        {
            Color newColor = color;
            newColor.a = 1.0f;

            float h = 0.0f;
            float s = 0.0f;
            float v = 0.0f;

            Color.RGBToHSV(newColor, out h, out s, out v);
            s = TowerF1Manager.flowerSValue;

            newColor = Color.HSVToRGB(h, s, v);

            _sprite.color = newColor;
        }

        _buffEffectPrefab = (buffEffect ? buffEffect : _buffEffectPrefab);
    }

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        switch (_type)
        {
            case FlowerType.Slow:
                _itemID = 100;
            break;

            case FlowerType.AtkDecrease:
                _itemID = 101;
            break;

            case FlowerType.CoolTimeIncrease:
                _itemID = 102;
            break;

            case FlowerType.MoreDamage:
                _itemID = 103;
            break;
        }

        //base.Talk(currentPlayer);
        base.Talk();

        CreateObject();

        PlayerMoveController currentPlayer = FindObjectOfType<PlayerMoveController>();
        if (_buffEffectPrefab)
        {
            GameObject buffEffect = Instantiate(_buffEffectPrefab);
            if (buffEffect)
            {
                buffEffect.transform.parent = currentPlayer.transform;

                float x = 0.0f;
                float y = 0.0f;
                if (_effectCreateCount % 2 == 0)
                {
                    x = Random.Range(-1.2f, -0.8f);
                    y = Random.Range(-0.6f, 0.6f);
                }
                else
                {
                    x = Random.Range(0.8f, 1.2f);
                    y = Random.Range(-0.6f, 0.6f);
                }
                _effectCreateCount++;

                buffEffect.transform.localPosition = new Vector3(x, y, 0.0f);

                buffEffect.GetComponent<TowerF1BuffEffectController>().type = _type;
            }
        }

        //TowerF1FlowerUIController ui = FindObjectOfType<TowerF1FlowerUIController>();
        //if (ui)
        //    ui.UIOn(_type);

        //Shortcut shortcut = FindObjectOfType<Shortcut>();
        //if (shortcut)
        //    shortcut.EnableShortcut();

        //TowerF1TutoManager tuto = FindObjectOfType<TowerF1TutoManager>();
        //if (tuto)
        //    tuto.FlowerGainSignal(_type);

        //QuestListManager questManager = FindObjectOfType<QuestListManager>();
        //if (questManager)
        //{
        //    questManager.QuestUpdate(200);
        //    questManager.QuestUpdate(101);
        //}

        if (dropper.door)
            dropper.door.GoToNextField();

        if (_sfx)
            _sfx.PlaySFX(_sfxName);

        Destroy(gameObject);
    }
}
