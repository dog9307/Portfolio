using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorListHelper : MonoBehaviour
{
    [SerializeField]
    private int _targetManagerID = 100;
    [SerializeField]
    private int _targetFloor = 0;

    [SerializeField]
    private List<Collider2D> _colList;
    public List<Collider2D> colList { get { return _colList; } }

    [SerializeField]
    private List<SortingLayerInfo> _infoes;
    public List<SortingLayerInfo> infoes { get { return _infoes; } }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FindManager());
    }

    IEnumerator FindManager()
    {
        FloorManager manager = null;
        while (true)
        {
            manager = FindObjectOfType<FloorManager>();
            if (!manager)
            {
                yield return null;
                continue;
            }

            if (manager.managerID == _targetManagerID)
                break;
        }

        manager.AddFloorList(_targetFloor, this);
    }
}
