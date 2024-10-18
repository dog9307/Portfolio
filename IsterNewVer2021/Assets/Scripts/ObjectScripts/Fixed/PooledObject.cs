using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PooledObject
{  
    //객체 이름, 저장할 프리팹, 초기화시 생성 객체수 
    public string poolItemName = string.Empty;
    public GameObject prefab = null;
    public int poolCount = 0;

    [SerializeField]
    //생성 객체 저장되는 리스트
    private List<GameObject> poolList = new List<GameObject>();

    //시작시 설정 갯수만큼 생성
    public void Initialize(Transform parent = null)
    {
        for (int ix = 0; ix < poolCount; ++ix)
        {
            poolList.Add(CreateItem(parent));
        }
    }

    //객체 반환 및 리스트 복귀
    public void PushToPool(GameObject item, Transform parent = null)
    {
        item.transform.SetParent(parent);
        item.SetActive(false);
        poolList.Add(item);
    }
    
    //꺼내쓰기 - 남은객체가 0개 이하면 새로 만들어준다.
    public GameObject PopFromPool(Transform parent = null)
    { 
        if (poolList.Count == 0)
            poolList.Add(CreateItem(parent));

        GameObject item = poolList[0];
        poolList.RemoveAt(0);

        return item;
    }
    
    //객체 생성하기
    private GameObject CreateItem(Transform parent = null)
    {
        GameObject item = Object.Instantiate(prefab) as GameObject;
        item.name = poolItemName;
        item.transform.SetParent(parent);
        item.SetActive(false);

        return item;
    }
	
}

