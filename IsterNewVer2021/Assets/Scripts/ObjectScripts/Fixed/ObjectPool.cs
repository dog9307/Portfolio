using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<PooledObject> objectPool = new List<PooledObject>();

    static private ObjectPool _instance;
    static public ObjectPool instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<ObjectPool>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "ObjectPool";
                _instance = container.AddComponent<ObjectPool>();
            }
        }

        for (int ix = 0; ix < objectPool.Count; ++ix)
        {
            objectPool[ix].Initialize(transform);
        }

        DontDestroyOnLoad(ObjectPool.instance);
    }
    
    //객체의 pool 오브젝트 이름, 반환할 객체, 부모계층관계 설정.
    public bool PushToPool(string itemName, GameObject item, Transform parent = null)
    {
        PooledObject pool = GetPoolItem(itemName);
        if (pool == null)
            return false;

        pool.PushToPool(item, parent == null ? transform : parent);
        return true;
    }
    //요청할 객체의 pool오브젝트 이름, 부모계층 관계설정.
    public GameObject PopFromPool(string itemName, Transform parent = null)
    {
        PooledObject pool = GetPoolItem(itemName);
        if (pool == null)
            return null;

        return pool.PopFromPool(parent);
    }
    PooledObject GetPoolItem(string itemName)
    {
        for (int ix = 0; ix < objectPool.Count; ++ix)
        {
            if (objectPool[ix].poolItemName.Equals(itemName))
                return objectPool[ix];
        }
        Debug.LogWarning("There's no matched pool list. ");
        return null;
    }
}
