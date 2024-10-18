using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleBackgroundSelector : MonoBehaviour
{
    [SerializeField]
    private Image _background;

    [SerializeField]
    private Image _nextImage;

    [SerializeField]
    private Sprite[] _sprites;
    private int[] _indexes;
    private int _currentIndex = 0;

    void Start()
    {
        _indexes = new int[_sprites.Length];
        for (int i = 0; i < _indexes.Length; ++i)
            _indexes[i] = i;

        Shuffle();

        SelectNextSprite();
    }

    void Shuffle()
    {
        for (int i = 0; i < 777; ++i)
        {
            int dest = Random.Range(1, _indexes.Length);
            int sour = Random.Range(1, _indexes.Length);

            int temp = _indexes[dest];
            _indexes[dest] = _indexes[sour];
            _indexes[sour] = temp;
        }
    }

    public void SelectNextSprite()
    {
        _nextImage.sprite = _sprites[_currentIndex];
        _currentIndex += 1;
        if (_currentIndex >= _indexes.Length)
        {
            Shuffle();
            _currentIndex = 0;
        }
    }

    public void AnimStart()
    {
        _background.sprite = _nextImage.sprite;
    }
}
