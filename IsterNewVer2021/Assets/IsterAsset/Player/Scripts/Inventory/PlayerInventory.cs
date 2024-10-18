using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayerInventory : SavableObject
{
    private List<IUpdatable> _updatables = new List<IUpdatable>();

    [SerializeField]
    private BuffInfo _playerBuff;

    private PlayerInfo _playerInfo;

    void Start()
    {
        darkLight = 0;
        // Load DarkLight
        LoadDarkLight();
    }

    void Update()
    {
        foreach (var u in _updatables)
        {
            if (u != null)
                u.Update();
        }
    }

    #region RuleItem
    private PlayerTowerRuleInventory _towerRuleInventory = new PlayerTowerRuleInventory();
    public PlayerTowerRuleInventory towerRuleInventory { get { return _towerRuleInventory; } }

    public void AddRuleItem(TowerRuleItemTalkFrom talkFrom)
    {
        TowerRuleItemBase newItem = TowerRuleItemFactory.CreateItem(talkFrom.itemID);

        if (newItem == null) return;

        _towerRuleInventory.AddItem(newItem);

        if (typeof(IUpdatable).IsInstanceOfType(newItem))
            _updatables.Add((IUpdatable)newItem);
    }

    public void RemoveRuleItem(int id)
    {
        _towerRuleInventory.RemoveItem(id);
    }

    public void RuleInventoryReset()
    {
        _towerRuleInventory.RemoveItemAll();
    }

    public TowerRuleItemBase FindRuleItem(int id)
    {
        return _towerRuleInventory.FindItem(id);
    }
    #endregion

    #region RelicItem
    private PlayerRelicInventory _relicInventory = new PlayerRelicInventory();
    public PlayerRelicInventory relicInventory { get { return _relicInventory; } }

    void AddToList(RelicItemBase newItem)
    {
        _relicInventory.AddItem(newItem);

        FindObjectOfType<RelicIconList>().RelicIconOn(newItem.id);
    }

    public RelicItemBase AddRelicItem(RelicItemTalkFrom talkFrom)
    {
        RelicItemBase newItem = RelicItemFactory.CreateItem(talkFrom.itemID);

        if (newItem == null) return null;
        AddToList(newItem);

        if (typeof(IUpdatable).IsInstanceOfType(newItem))
            _updatables.Add((IUpdatable)newItem);

        return newItem;
    }

    public RelicItemBase AddRelicItem(RelicItemBase newItem)
    {
        if (newItem == null) return null;
        AddToList(newItem);

        if (typeof(IUpdatable).IsInstanceOfType(newItem))
            _updatables.Add((IUpdatable)newItem);

        return newItem;
    }

    public void RemoveRelicItem(int id)
    {
        _relicInventory.RemoveItem(id);
    }

    public void RelicInventoryReset()
    {
        _relicInventory.RemoveItemAll();
    }

    public RelicItemBase FindRelicItem(int id)
    {
        return _relicInventory.FindItem(id);
    }

    [YarnCommand("RelicItemCheck")]
    public void CheckItemExist(string key, int id)
    {
        InMemoryVariableStorage storage = FindObjectOfType<InMemoryVariableStorage>();
        if (!storage) return;

        RelicItemBase relic = FindRelicItem(id);
        storage.SetValue(key, (relic != null));
    }
    #endregion

    #region DarkLight

    private int _darkLight;
    public int darkLight
    {
        get { return _darkLight; }
        set
        {
            int newValue = value;
            if (_darkLight != newValue)
            {
                _darkLight = newValue;

                StopAllCoroutines();
                StartCoroutine(UpdateUI());
            }
        }
    }
    private DarkLightUI _darkLightUI;

    public void GainDarkLight(int figure)
    {
        darkLight += figure;
        UpdateStatus();
        StartCoroutine(UpdateUI());
    }

    public bool LoseDarkLight(int figure)
    {
        bool isSuccess = false;

        if (darkLight >= figure)
        {
            darkLight -= figure;
            UpdateStatus();

            isSuccess = true;
        }

        return isSuccess;
    }

    void UpdateStatus()
    {
        if (!_playerBuff) return;

        if (!_playerInfo)
            _playerInfo = _playerBuff.GetComponent<PlayerInfo>();

        int attackFigure = (int)Mathf.Sqrt(((float)darkLight / 10.0f));
        _playerInfo.attackFigure = attackFigure;
    }

    void LoadDarkLight()
    {
        int count = PlayerPrefs.GetInt(_key, 0);
        int dieCount = PlayerPrefs.GetInt("PlayerDieDarkLight", 0);
        GainDarkLight(count/* - (dieCount / 2)*/);
    }

    public override SavableNode[] GetSaveNodes()
    {
        SavableNode[] nodes = new SavableNode[1];
        nodes[0] = new SavableNode();

        nodes[0].value = darkLight;
        nodes[0].key = _key;

        return nodes;
    }

    IEnumerator UpdateUI()
    {
        while (!_darkLightUI)
        {
            _darkLightUI = FindObjectOfType<DarkLightUI>();
            yield return null;
        }

        _darkLightUI.UpdateUI(darkLight);
    }

    #endregion
}
