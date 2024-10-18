using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerItemRewardManager : MonoBehaviour
{
    #region SINGLETON
    static private TowerItemRewardManager _instance;
    static public TowerItemRewardManager instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<TowerItemRewardManager>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "TowerItemRewardManager";
                _instance = container.AddComponent<TowerItemRewardManager>();
            }
        }
        else
            Destroy(gameObject);
    }
    #endregion

    [Header("Item List")]
    [SerializeField]
    private List<GameObject> _itemPrefabs;

    private List<GameObject> _currentItemList = new List<GameObject>();
    private List<GameObject> _backItemList = new List<GameObject>();
    private int _currentIndex;

    public GameObject currentItem
    {
        get
        {
            if (_currentIndex >= _currentItemList.Count)
                return null;

            return _currentItemList[_currentIndex];
        }
    }

    [Header("Reward")]
    [SerializeField]
    private GameObject _rewardEffect;
    public GameObject reward { get; set; }

    void Start()
    {
        SetList();
    }

    private void SetList()
    {
        foreach (var prefab in _itemPrefabs)
        {
            GameObject newItem = Instantiate(prefab);
            newItem.SetActive(false);
            newItem.transform.parent = transform;

            _currentItemList.Add(newItem);
        }

        ShuffleItemList();
    }

    private void ReloadItemList()
    {
        foreach (var item in _backItemList)
            _currentItemList.Add(item);

        _backItemList.Clear();
    }

    private void ShuffleItemList()
    {
        CommonFuncs.ShuffleList(_currentItemList);
    }

    public GameObject GetNextItem()
    {
        if (_currentItemList.Count == 0)
            ReloadItemList();

        GameObject current = currentItem;

        if (current)
            _currentItemList.Remove(current);

        return current;
    }

    public void ReturnItem(GameObject item)
    {
        if (!item) return;

        _backItemList.Add(item);
    }

    public void ReturnReward()
    {
        if (!reward) return;

        _backItemList.Add(reward);
        reward.SetActive(false);
        reward = null;
    }

    public void OpenReward(Transform pos)
    {
        if (!reward || !pos) return;

        if (_rewardEffect)
        {
            GameObject effect = Instantiate(_rewardEffect);
            Vector3 effectPos = pos.position;
            effectPos.y += 1.0f;
            effect.transform.position = effectPos;
        }

        reward.transform.parent = null;
        reward.transform.position = pos.position;
        reward.SetActive(true);
    }

    public GameObject GetRandomSkillItem(bool isWantPassive)
    {
        int compare = (isWantPassive ? 2 : 1);
        foreach (var g in _currentItemList)
        {
            SkillItemTalkFrom talk = g.GetComponent<SkillItemTalkFrom>();
            if (talk)
            {
                if (talk.relativeSkillId / 100 == compare)
                {
                    _currentItemList.Remove(g);
                    return g;
                }
            }
        }

        return null;
    }
}
