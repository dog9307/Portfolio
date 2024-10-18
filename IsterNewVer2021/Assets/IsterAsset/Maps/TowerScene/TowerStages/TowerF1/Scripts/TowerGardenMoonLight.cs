using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerGardenMoonLight : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private UnityEngine.Rendering.Universal.Light2D _light;
    [HideInInspector]
    [SerializeField]
    private UnityEngine.Rendering.Universal.Light2D _foregroundLight;

    private PlayerMoveController _player;

    [HideInInspector]
    [SerializeField]
    private SpriteMask _backgroundMask;
    [HideInInspector]
    [SerializeField]
    private SpriteMask _foregroundMask;

    [HideInInspector]
    [SerializeField]
    private SpriteRenderer[] _renderers;

    [SerializeField]
    private RelicLightAffected _relicLight;

    public FlowerType currentType { get; set; }

    private int _originSortingOrder = -1000;

    [SerializeField]
    private bool _isTurnOn = false;

    void Start()
    {
        _player = FindObjectOfType<PlayerMoveController>();

        if (_isTurnOn)
            TurnOn();
        else
            TurnOff();

        currentType = FlowerType.NONE;

        if (_relicLight)
            _relicLight.AffectRelicLight();
    }

    void Update()
    {
        transform.position = _player.center;
    }

    public void TurnOn()
    {
        _light.intensity = 1.0f;
        _foregroundLight.intensity = 15.0f;

        foreach (var s in _renderers)
            s.enabled = true;
    }

    public void TurnOff()
    {
        _light.intensity = 0.0f;
        _foregroundLight.intensity = 0.0f;

        foreach (var s in _renderers)
            s.enabled = false;
    }

    public void Switch(bool toTurnOn)
    {
        if (toTurnOn)
            BlackMaskController.instance.AddEvent(TurnOn, BlackMaskEventType.MIDDLE);
        else
            BlackMaskController.instance.AddEvent(TurnOff, BlackMaskEventType.MIDDLE);
    }

    public void LightColorChange(FlowerType type)
    {
        currentType = type;

        Color newColor = _light.color;

        float targetH = 0.0f;
        switch (type)
        {
            // 부채꼴 - 초록색
            case FlowerType.MoreDamage:
                targetH = TowerGardenManager.MoreDamageH;
            break;

            // 소환 - 빨간색
            case FlowerType.AtkDecrease:
                targetH = TowerGardenManager.AtkDecreaseH;
            break;

            // 근접 - 주황색
            case FlowerType.CoolTimeIncrease:
                targetH = TowerGardenManager.CoolTimeIncreaseH;
            break;

            // 피자 - 노란색
            case FlowerType.Slow:
                targetH = TowerGardenManager.SlowH;
            break;
        }

        float currentH = 0.0f;
        float currentS = 0.0f;
        float currentV = 0.0f;
        Color.RGBToHSV(newColor, out currentH, out currentS, out currentV);

        currentH = targetH / 360.0f;
        currentS = 1.0f;

        newColor = Color.HSVToRGB(currentH, currentS, currentV);
        _light.color = newColor;

        if (_backgroundMask)
        {
            _backgroundMask.backSortingOrder = _originSortingOrder - 5;
            _backgroundMask.frontSortingOrder = _originSortingOrder + 5;
        }
        if (_foregroundMask)
        {
            _foregroundMask.backSortingOrder = _originSortingOrder - 5;
            _foregroundMask.frontSortingOrder = _originSortingOrder + 5;
        }

        currentS = 0.5f;
        newColor = Color.HSVToRGB(currentH, currentS, currentV);
        _foregroundLight.color = newColor;

        currentS = 0.3f;
        newColor = Color.HSVToRGB(currentH, currentS, currentV);
        foreach (var s in _renderers)
        {
            newColor.a = s.color.a;
            s.color = newColor;
        }
    }
}
