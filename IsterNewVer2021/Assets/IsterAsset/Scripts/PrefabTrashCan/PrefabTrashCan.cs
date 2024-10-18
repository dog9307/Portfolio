using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTrashCan : MonoBehaviour
{
    #region singleton
    static private PrefabTrashCan _instance;
    static public PrefabTrashCan instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<PrefabTrashCan>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "PrefabTrashCan";
                _instance = container.AddComponent<PrefabTrashCan>();
            }
        }
        DontDestroyOnLoad(PrefabTrashCan.instance);
    }
    #endregion singleton

    private List<GameObject> _trashList = new List<GameObject>();

    public void AddTrash(GameObject t)
    {
        _trashList.Add(t);
    }

    public void EmptyTrash()
    {
        foreach (var t in _trashList)
        {
            if (t)
                Destroy(t);
        }
        _trashList.Clear();
    }
}
