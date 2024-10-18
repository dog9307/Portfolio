using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : SavableObject
{
    [SerializeField]
    private GameObject[] _fields;

    private GameObject _currentField;
    public GameObject currentField { get { return _currentField; } }

    public GameObject FindFieldByName(string fieldName)
    {
        foreach (var f in _fields)
        {
            if (f.name == fieldName)
                return f;
        }

        return null;
    }

    public void LoadField()
    {
        string nextField = PlayerPrefs.GetString("PlayerCurrentField", "Underground_field_0");
        ChangeField(nextField, true);
    }

    public void ChangeField(GameObject nextField, int nextStartPosID)
    {
        _currentField = FindField(nextField);
        if (!_currentField) return;

        OpenField(nextStartPosID);
    }

    public void ChangeField(string nextField, int nextStartPosID)
    {
        if (nextField == "TowerStartField")
            _currentField = FindTowerStartField();
        else
            _currentField = FindField(nextField);

        if (!_currentField) return;

        OpenField(nextStartPosID);
    }

    public void ChangeField(GameObject nextField, bool isLoad = false)
    {
        _currentField = FindField(nextField);
        if (!_currentField) return;

        OpenField(isLoad);
    }

    public void ChangeField(string nextField, bool isLoad = false)
    {
        _currentField = FindField(nextField);
        if (!_currentField) return;

        OpenField(isLoad);
    }

    private GameObject FindField(string name)
    {
        GameObject field = null;
        foreach (var f in _fields)
        {
            if (f.name == name)
            {
                field = f;
                break;
            }
        }

        return field;
    }

    private GameObject FindField(GameObject find)
    {
        GameObject field = null;
        foreach (var f in _fields)
        {
            if (f == find)
            {
                field = f;
                break;
            }
        }

        return field;
    }

    private GameObject FindTowerStartField()
    {
        GameObject field = null;
        foreach (var f in _fields)
        {
            if (f.name.Contains("_0") ||
                f.name.Contains("_start"))
            {
                field = f;
                break;
            }
        }

        return field;
    }

    public void OpenField(bool isLoad = false)
    {
        foreach (var f in _fields)
        {
            if (f == _currentField)
                f.SetActive(true);
            else
                f.SetActive(false);
        }

        PlayerStartPosFinder startPosFinder = FindObjectOfType<PlayerStartPosFinder>();
        if (startPosFinder)
            startPosFinder.FindStartPos(isLoad);
    }

    public void OpenField(int nextStartPosID)
    {
        foreach (var f in _fields)
        {
            if (f == _currentField)
                f.SetActive(true);
            else
                f.SetActive(false);
        }

        PlayerStartPosFinder startPosFinder = FindObjectOfType<PlayerStartPosFinder>();
        if (startPosFinder)
        {
            startPosFinder.nextStartPosID = nextStartPosID;
            startPosFinder.FindStartPos();
        }
    }

    public override SavableNode[] GetSaveNodes()
    {
        SavableNode[] nodes = new SavableNode[1];
        nodes[0] = new SavableNode();

        nodes[0].key = "PlayerCurrentField";
        nodes[0].value = _currentField.name;

        return nodes;
    }
}
