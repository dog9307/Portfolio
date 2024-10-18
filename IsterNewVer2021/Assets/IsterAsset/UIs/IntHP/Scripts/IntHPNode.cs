using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HPNODE
{
    NONE = -1,
    FULL,
    HALF,
    EMPTY,
    END
}

public class IntHPNode : MonoBehaviour
{
    [SerializeField]
    private Sprite _full;
    [SerializeField]
    private Sprite _half;
    [SerializeField]
    private Sprite _empty;

    [SerializeField]
    private Image _cover;
    //[SerializeField]
    //private ParticleSystem _particle;

    public void UpdateUI(HPNODE mode)
    {
        Sprite next = null;
        switch (mode)
        {
            case HPNODE.FULL:
                next = _full;
            break;

            case HPNODE.HALF:
                next = _half;
            break;

            case HPNODE.EMPTY:
                next = _empty;
            break;
        }

        _cover.sprite = next;
        //_particle.textureSheetAnimation.SetSprite(0, next);
    }

    public void ApplyColor(Color color)
    {
        Color newColor = _cover.color;

        newColor.r = color.r;
        newColor.g = color.g;
        newColor.b = color.b;

        _cover.color = newColor;
    }
}