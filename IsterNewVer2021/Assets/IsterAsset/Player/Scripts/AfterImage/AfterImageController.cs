using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageController : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private Color _startColor;
    [SerializeField]
    [Range(0.0f, 1.0f)] private float _diffRatio;

    //[SerializeField]
    //private int _startDiff;
    //[SerializeField]
    //private int _diff;

    [SerializeField]
    private float _delayTime;
    private float _currentTime;

    private List<GameObject> _afterImages;

    private SpriteRenderer _playerSprite;
    
    void OnEnable()
    {
        if (!_playerSprite)
            _playerSprite = GameObject.Find("Player").GetComponent<SpriteRenderer>();

        if (_afterImages == null)
            _afterImages = new List<GameObject>(3);

        Clear();

        _currentTime = 0.0f;
    }

    void Clear()
    {
        foreach (var image in _afterImages)
            Destroy(image);

        _afterImages.Clear();
    }
    
    void Update()
    {
        _currentTime += IsterTimeManager.deltaTime;
        if (_currentTime >= _delayTime)
        {
            if (_afterImages.Count < 3)
            {
                _afterImages.Add(CreateImage());

                SpriteRenderer sprite = _afterImages[_afterImages.Count - 1].GetComponent<SpriteRenderer>();
                //sprite.sortingOrder = -_afterImages.Count - 999;
                Color newColor = _startColor * (1.0f - (_afterImages.Count - 1) * _diffRatio);
                sprite.color = newColor;

                //float b = 255 - (_afterImages.Count - 1) * _diff;
                //b = (b > 0 ? b : 0) / 255;
                //sprite.color = new Color(0.0f, 0.0f, b, b);
            }
            
            for (int i = _afterImages.Count - 1; i >= 0; --i)
                ChangeSprite(i);

            _currentTime = 0.0f;
        }
    }

    void ChangeSprite(int index)
    {
        SpriteRenderer sprite = _afterImages[index].GetComponent<SpriteRenderer>();

        if (index != 0)
        {
            SpriteRenderer next = _afterImages[index - 1].GetComponent<SpriteRenderer>();
            _afterImages[index].transform.position = _afterImages[index - 1].transform.position;
            sprite.sprite = next.sprite;
            sprite.sortingOrder = _playerSprite.sortingOrder - 10;
        }
        else
        {
            _afterImages[0].transform.position = FindObjectOfType<PlayerMoveController>().prevPosition;
            sprite.sprite = _playerSprite.sprite;
            sprite.sortingOrder = _playerSprite.sortingOrder - 10;
        }
    }

    GameObject CreateImage()
    {
        GameObject newImage = Instantiate(_prefab) as GameObject;
        newImage.transform.parent = transform;

        return newImage;
    }
}
