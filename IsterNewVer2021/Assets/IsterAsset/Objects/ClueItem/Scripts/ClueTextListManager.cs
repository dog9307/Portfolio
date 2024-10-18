using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueTextListManager : MonoBehaviour
{
    [SerializeField]
    private List<ClueTextListController> _textLists;
    [SerializeField]
    private Text _title;

    private ClueTextListController _currentList;

    void Start()
    {
        _currentList = null;
    }

    private void OnEnable()
    {
        _title.text = "";
        foreach (var tl in _textLists)
            tl.gameObject.SetActive(false);

        if (_currentList)
        {
            _title.text = _currentList.OpenClue();
            _currentList.gameObject.SetActive(true);
        }
    }

    public void GainClue(string keyward, int id)
    {
        ClueTextListController targetList = FindTextList(keyward);

        if (targetList)
            targetList.OpenClue(id);
    }

    public ClueTextListController FindTextList(string keyward)
    {
        ClueTextListController find = null;

        foreach (var tl in _textLists)
        {
            if (tl.keyward == keyward)
            {
                find = tl;
                break;
            }
        }

        return find;
    }

    public void ChangeList(ClueTextListController newList)
    {
        if (_currentList)
            _currentList.gameObject.SetActive(false);

        _currentList = newList;

        if (_currentList)
        {
            _title.text = _currentList.OpenClue();
            _currentList.gameObject.SetActive(true);
        }
    }

    public void Load()
    {
        foreach (var list in _textLists)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                int id = (100 + i);
                string key = "Clue_" + list.keyward + "_" + id.ToString();
                int count = PlayerPrefs.GetInt(key, 0);
                if (count >= 100)
                    list.OpenClue(id);
            }
        }
    }
}
