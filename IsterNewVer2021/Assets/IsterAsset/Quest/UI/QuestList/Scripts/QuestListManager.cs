using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListManager : MonoBehaviour
{
    [SerializeField]
    private List<QuestElementControllerPro> _elements;

    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private GameObject _relicLightPrefab;

    void Start()
    {
        if (_item)
            _item.SetActive(false);

        int count = 0;
        for (int i = 100; i < 100 + 4; ++i)
        {
            count = PlayerPrefs.GetInt($"quest_{i}", 0);
            if (count >= 100)
            {
                foreach (var q in _elements)
                {
                    if (q.questId == i)
                    {
                        q.QuestClear();
                        break;
                    }
                }
            }
        }
    }

    public void QuestUpdate(int id)
    {
        foreach (var q in _elements)
        {
            if (q.questId == id)
            {
                q.UpdateUI();
                break;
            }
        }
    }

    [SerializeField]
    private GameObject _item;

    private Damagable _player;
    public void PlayerHeal()
    {
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>().GetComponent<Damagable>();

        _player.Heal(20.0f);
    }

    public void PlayerTotalHPUp()
    {
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>().GetComponent<Damagable>();

        _player.TotalHPUp(20.0f);
    }

    public void OpenBossDoor()
    {
        PlayerPrefs.SetInt("quest_100", 100);
        PlayerTotalHPUp();
    }

    public void GainAllFlower()
    {
        PlayerPrefs.SetInt("quest_101", 100);
        PlayerTotalHPUp();
    }

    public void OpenDoorWithFullHP()
    {
        PlayerPrefs.SetInt("quest_102", 100);
        PlayerTotalHPUp();
    }

    public void FirstDie()
    {
        int count = PlayerPrefs.GetInt("quest_103", 0);
        if (count >= 100)
        {
            count = PlayerPrefs.GetInt($"passive_211", 0);
            if (count < 100)
            {
                if (_item)
                    BlackMaskController.instance.AddEvent(CreateItem, BlackMaskEventType.POST);
            }

            count = PlayerPrefs.GetInt("relic_100", 0);
            if (count < 100)
            {
                if (_relicLightPrefab)
                    BlackMaskController.instance.AddEvent(CreateRelicLight, BlackMaskEventType.POST);
            }
        }
    }

    public void CreateItem()
    {
        GameObject newItem = Instantiate(_prefab);
        newItem.transform.position = _item.transform.position;
    }

    public void CreateRelicLight()
    {
        int count = PlayerPrefs.GetInt("relic_100", 0);
        if (count < 100)
        {
            GameObject newItem = Instantiate(_relicLightPrefab);
            Vector3 newPos = _item.transform.position;
            newPos.y -= 4.0f;
            newItem.transform.position = newPos;
        }
    }
}
