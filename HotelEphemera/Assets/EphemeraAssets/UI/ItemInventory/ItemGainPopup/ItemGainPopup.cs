using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Michsky.UI.Dark;

public class ItemGainPopup : MonoBehaviour
{
    #region SINGLETON
    static private ItemGainPopup _instance;
    static public ItemGainPopup instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<ItemGainPopup>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "ItemGainPopup";
                _instance = container.AddComponent<ItemGainPopup>();
            }
        }
        else
            Destroy(gameObject);
    }
    #endregion

    [SerializeField]
    private Animator _anim;
    //[SerializeField]
    //private UIDissolveEffect _dissolve;
    [SerializeField]
    private Image _itemImage;

    [SerializeField]
    private ItemDictionary _itemDic;
    [SerializeField]
    private Sprite _dummySprite;

    [SerializeField]
    private bool _isStopUntilClick = true;
    private bool _isClicked = false;
    private bool _isCanClick = false;
    [SerializeField]
    private float _showDuration = 1.0f;

    [SerializeField]
    private SFXPlayer _sfx;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var i in _itemDic)
        {
            if (i.Value.binder)
                i.Value.binder.itemImage = i.Value.sprite;

            string key = $"Item_{i.Key}_Gain";
            int count = SavableDataManager.instance.FindIntSavableData(key);
            if (count >= 100)
            {
                key = $"Item_{i.Key}_Lose";
                count = count = SavableDataManager.instance.FindIntSavableData(key);
                if (count < 100)
                    _itemDic[i.Key].binder?.ItemGain();
            }
        }
    }

    void Update()
    {
        if (!_isCanClick) return;

        if (_isStopUntilClick)
        {
            if (!_isClicked)
            {
                if (Input.anyKeyDown)
                    _isClicked = true;
            }
        }
    }

    private int _lastGainItemID;
    private bool _isItemLoseSequence = false;
    public void ItemGain(int itemID, bool isItemLose)
    {
        _lastGainItemID = itemID;
        _isItemLoseSequence = isItemLose;

        Sprite sprite = null;
        if (_itemDic.ContainsKey(itemID))
            sprite = _itemDic[itemID].sprite;
        else
            sprite = _dummySprite;

        _itemImage.sprite = sprite;

        // 짜치는 코드
        if (itemID == 6)
        {
            float currentH = 202.0f / 360.0f;
            float currentS = 0.85f;
            float currentV = 1.0f;
            _itemImage.color = Color.HSVToRGB(currentH, currentS, currentV);
        }
        else
            _itemImage.color = Color.white;

        _isClicked = false;
        _isCanClick = false;

        SavableNode node = new SavableNode();
        node.key = (isItemLose ? $"Item_{itemID}_Lose" : $"Item_{itemID}_Gain");
        node.value = 100;
        SavableDataManager.instance.AddSavableObject(node);

        StartCoroutine(GainPopup());
    }

    IEnumerator GainPopup()
    {
        if (_isItemLoseSequence)
            _sfx?.PlaySFX("ItemLose");
        else
            _sfx?.PlaySFX("ItemGain");

        DialogueManager.instance.Pause();

        //_dissolve.location = 1.0f;
        //_dissolve.DissolveIn();
        _anim.Play("Panel In");

        yield return new WaitForSeconds(1.5f);

        _isCanClick = true;
        if (_isStopUntilClick)
        {
            while (!_isClicked)
                yield return null;
        }
        else
            yield return new WaitForSeconds(_showDuration);

        //_dissolve.location = 0.0f;
        //_dissolve.DissolveOut();

        _isCanClick = false;
        _anim.Play("Panel Out");

        yield return new WaitForSeconds(1.5f);

        DialogueManager.instance.Resume();

        _sfx?.PlaySFX("PopupOut");

        if (_itemDic.ContainsKey(_lastGainItemID))
        {
            if (_isItemLoseSequence)
                _itemDic[_lastGainItemID].binder?.ItemLose();
            else
            {
                int count = SavableDataManager.instance.FindIntSavableData($"Item_{_lastGainItemID}_Lose");
                if (count < 100)
                    _itemDic[_lastGainItemID].binder?.ItemGain();
            }
        }
    }

    public bool IsItemInInventory(int itemNum)
    {
        if (!_itemDic.ContainsKey(itemNum))
            return false;

        return _itemDic[itemNum].binder.gameObject.activeSelf;
    }

    public void ItemJustLose(int itemID)
    {
        if (!_itemDic.ContainsKey(itemID))
            return;

        _itemDic[itemID].binder?.ItemLose();
    }
}
