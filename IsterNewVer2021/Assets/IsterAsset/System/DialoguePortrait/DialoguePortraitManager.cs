using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePortraitManager : MonoBehaviour
{
    [SerializeField]
    private DialogueImageScaling _leftImage;
    [SerializeField]
    private DialogueImageScaling _rightImage;

    [SerializeField]
    private float _minImageScale = 0.8f;
    public float minImageScale { get { return _minImageScale; } }
    [SerializeField]
    private float _minImageColor = 0.7f;
    public float minImageColor { get { return _minImageColor; } }

    [SerializeField]
    private float _maxImageScale = 1.0f;
    public float maxImageScale { get { return _maxImageScale; } }
    [SerializeField]
    private float _maxImageColor = 1.0f;
    public float maxImageColor { get { return _maxImageColor; } }

    [SerializeField]
    private Sprite _dummySprite;

    [SerializeField]
    private PortraitManager _portraits;

    [SerializeField]
    private RectTransform _nameBar;

    public void PortraitTurnOn(bool isTurnOn)
    {
        _leftImage.gameObject.SetActive(isTurnOn);
        _rightImage.gameObject.SetActive(isTurnOn);
    }

    private Sprite FindSprite(string characterName, string face)
    {
        if (face.Contains('\"'))
        {
            string newCharacterName = face.Substring(1, face.Length - 2);
            face = newCharacterName;
        }

        Sprite sprite = null;

        if (_portraits.ContainsKey(characterName))
        {
            if (_portraits[characterName].ContainsKey(face))
                sprite = _portraits[characterName][face].sprite;
        }

        return sprite;
    }

    public void InitialSetting(string leftCharacter, string leftFace, string rightCharacter, string rightFace)
    {
        Sprite sprite = FindSprite(leftCharacter, leftFace);
        sprite = (sprite ? sprite : _dummySprite);

        _leftImage.SetImage(sprite);
        _leftImage.ApplyScale(-_maxImageScale);
        _leftImage.ApplyColor(_maxImageColor);

        sprite = FindSprite(rightCharacter, rightFace);
        sprite = (sprite ? sprite : _dummySprite);

        _rightImage.SetImage(sprite);
        _rightImage.ApplyScale(minImageScale);
        _rightImage.ApplyColor(minImageColor);
    }

    public void PortraitChange(string characterName, string face, bool isLeft = true)
    {
        DialogueImageScaling target = (isLeft ? _leftImage : _rightImage);
        DialogueImageScaling reverseTarget = (isLeft ? _rightImage : _leftImage);

        Sprite sprite = FindSprite(characterName, face);
        sprite = (sprite ? sprite : _dummySprite);

        //LogFileManager.WriteLine("PortraitIssue", $"{characterName} {reverseTarget.gameObject.name} Change : {(sprite ? sprite.name : "null")}");

        target.SetImage(sprite);
        target.TurnOn((isLeft ? -_maxImageScale : _maxImageScale), _maxImageColor);

        reverseTarget.TurnOff((isLeft ? _minImageScale : -_minImageScale), _minImageColor);

        //NameBarReposition(isLeft);
    }

    public void ResetPortrait()
    {
        _leftImage.SetImage(_dummySprite);
        _leftImage.ApplyScale(1.0f);
        _leftImage.ApplyColor(1.0f);

        _rightImage.SetImage(_dummySprite);
        _rightImage.ApplyScale(minImageScale);
        _rightImage.ApplyColor(minImageColor);
    }

    void NameBarReposition(bool isLeft)
    {
        if (!_nameBar) return;

        Vector2 anchoredPos = _nameBar.anchoredPosition;
        anchoredPos.x = (isLeft ? 199.4f : 1025.641f);
        _nameBar.anchoredPosition = anchoredPos;
    }
}
