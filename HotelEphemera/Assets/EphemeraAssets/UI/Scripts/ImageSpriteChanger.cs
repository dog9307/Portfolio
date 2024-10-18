using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSpriteChanger : MonoBehaviour
{
    [SerializeField]
    private Image _targetImage;

    [SerializeField]
    private Sprite[] _sprites;

    public void SpriteChange(int index)
    {
        _targetImage.sprite = _sprites[index];
    }
}
