using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ProgressType
{
    NONE = -1,
    HORIZONTAL,
    VERTICAL,
    CLOCKWISE,
    RCLOCKWISE,
    END
}
public abstract class ProgressBase : UiBase
{
    public ProgressType _type;

    protected float _MaxGuage;
    public float MaxGuage { get; set; }
    protected float _CurretGuage;
    public float CurretGuage { get; set; }

    private void OnEnable()
    {
        _firstImage.type = Image.Type.Filled;

        if (_type == ProgressType.HORIZONTAL)
        {
            _firstImage.fillMethod = Image.FillMethod.Horizontal;
            _firstImage.fillOrigin = (int)Image.OriginHorizontal.Left;
        }
        else if (_type == ProgressType.VERTICAL)
        {
            _firstImage.fillMethod = Image.FillMethod.Vertical;
            _firstImage.fillOrigin = (int)Image.OriginVertical.Bottom;
        }
        else if (_type == ProgressType.CLOCKWISE)
        {
            _firstImage.fillMethod = Image.FillMethod.Radial360;
            _firstImage.fillOrigin = (int)Image.Origin360.Top;
            _firstImage.fillClockwise = true;
        }
        else if (_type == ProgressType.RCLOCKWISE)
        {
            _firstImage.fillMethod = Image.FillMethod.Radial360;
            _firstImage.fillOrigin = (int)Image.Origin360.Top;
            _firstImage.fillClockwise = false;
        }
    }
    public override void Init(){}
    public abstract void UpdateGauge();
    public override void ActiveUI()
    {
        UpdateGauge();
        switch (_type)
        {
            case ProgressType.NONE:
                break;
            case ProgressType.HORIZONTAL:
                _firstImage.fillAmount = _CurretGuage / _MaxGuage;
                break;
            case ProgressType.VERTICAL:
                _firstImage.fillAmount = _CurretGuage / _MaxGuage;
                break;
            case ProgressType.CLOCKWISE:
                _firstImage.fillAmount = _CurretGuage / _MaxGuage;
                break;
            case ProgressType.RCLOCKWISE:
                _firstImage.fillAmount = (_MaxGuage - _CurretGuage) / _MaxGuage;
                break;
            case ProgressType.END:
                break;
        }
    }
}