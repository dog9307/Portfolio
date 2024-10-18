using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField]
    private int _managerID = 100;
    public int managerID { get { return _managerID; } }

    //[SerializeField]
    //private Material _blurMat;
    private Dictionary<int, FloorList> _floorDic = new Dictionary<int, FloorList>();

    //private FloorList _prevFloorList;
    private FloorList _currentFloorList;
    public int currentPlayerFloor { get { return (_currentFloorList ? _currentFloorList.floor : int.MinValue); } }

    private int _minFloor;
    private int _maxFloor;

    //[SerializeField]
    //private float _minAmouont = 0.0f;
    //[SerializeField]
    //private float _maxAmount = 0.2f;

    private const int MAX_FLOOR_DIFF = 3;

    [SerializeField]
    private List<FloorSetter> _setters;

    void Start()
    {
        _minFloor = int.MaxValue;
        _maxFloor = int.MinValue;
        foreach (var f in _floorDic)
        {
            if (f.Value.floor < _minFloor)
                _minFloor = f.Value.floor;

            if (f.Value.floor > _maxFloor)
                _maxFloor = f.Value.floor;

            f.Value.ExitFloorTodo();
        }
    }

    private void OnEnable()
    {
        foreach (var s in _setters)
            s.gameObject.SetActive(true);
    }

    public void SettersOff()
    {
        foreach (var s in _setters)
            s.gameObject.SetActive(false);
    }

    public void AddFloor(int floor, FloorList list)
    {
        if (!list) return;
        _floorDic.Add(floor, list);

        //if (_blurMat)
        //    list.ApplySpriteMat(_blurMat);
    }

    public void AddFloorList(int floor, FloorListHelper helper)
    {
        if (!_floorDic.ContainsKey(floor)) return;

        _floorDic[floor].AddHelperList(helper);
    }

    public void AddFloorList(int floor, List<Collider2D> colList, List<SortingLayerInfo> infoes)
    {
        if (!_floorDic.ContainsKey(floor)) return;

        _floorDic[floor].AddHelperList(colList, infoes);
    }

    public void EnterFloor(int floor, bool isColorVChange, bool isSkip = false)
    {
        if (!_floorDic.ContainsKey(floor)) return;

        if (_currentFloorList)
            _currentFloorList.ExitFloorTodo();

        _currentFloorList = _floorDic[floor];
        if (!_currentFloorList) return;

        _currentFloorList.EnterFloorTodo();

        EnterFloorToCollider(floor);
        EnterFloorToSprite(floor, isColorVChange, isSkip);
    }

    public void EnterFloorToCollider(int floor)
    {
        if (!_floorDic.ContainsKey(floor)) return;

        //if (_currentFloorList)
        //    _currentFloorList.ExitFloorTodo();

        //_prevFloorList = _currentFloorList;
        //_currentFloorList = _floorDic[floor];
        //_currentFloorList.EnterFloorTodo();

        for (int i = _minFloor; i <= _maxFloor; ++i)
        {
            _floorDic[i].ColliderControl(i == floor);
        }
    }

    public void EnterFloorToSprite(int floor, bool isColorVChange, bool isSkip = false)
    {
        if (!_floorDic.ContainsKey(floor)) return;

        //if (_currentFloorList)
        //    _currentFloorList.ExitFloorTodo();

        //_prevFloorList = _currentFloorList;
        //_currentFloorList = _floorDic[floor];
        //_currentFloorList.EnterFloorTodo();

        for (int i = _minFloor; i <= _maxFloor; ++i)
        {
            if (i < floor)
            {
                _floorDic[i].SpriteControl(SORTING.Background);
                if (isColorVChange)
                    _floorDic[i].ColorVControl(floor - i, isSkip);
            }
            else if (i == floor)
            {
                _floorDic[i].SpriteControl(SORTING.NONE);
                if (isColorVChange)
                    _floorDic[i].ColorVControl(floor - i, isSkip);
            }
            else
                _floorDic[i].SpriteControl(SORTING.Foreground);
        }

        //isSpriteChangeDone = false;
        //if (_todoCoroutine != null)
        //    StopCoroutine(_todoCoroutine);

        //_todoCoroutine = StartCoroutine(WaitForTodo());
    }

    //private Coroutine _todoCoroutine;
    //public bool isSpriteChangeDone { get; set; }
    //IEnumerator WaitForTodo()
    //{
    //    if (_currentFloorList)
    //        _currentFloorList.EnterFloorTodo();

    //    while (!isSpriteChangeDone)
    //        yield return null;

    //    if (_prevFloorList)
    //        _prevFloorList.ExitFloorTodo();
    //}

    public void SetFloor(int floor)
    {
        EnterFloor(floor, true, true);
        //EnterFloorToCollider(floor);
        //EnterFloorToSprite(floor);
    }

    //public void SetBlur()
    //{
    //    if (!_currentFloorList) return;

    //    _currentFloorList.SetBlur(_minAmouont);

    //    float floorDiffAmount = (_maxAmount - _minAmouont) / (float)MAX_FLOOR_DIFF;
    //    int currentFloor = _currentFloorList.floor;
    //    for (int i = currentFloor - 1; i >= _minFloor; --i)
    //    {
    //        int floorDiff = currentFloor - i;
    //        float amount = floorDiffAmount * floorDiff;

    //        _floorDic[i].SetBlur(amount);
    //    }
    //}

    //public void SetBlur(float ratio)
    //{
    //    if (!_currentFloorList) return;

    //    _currentFloorList.SetBlur(_minAmouont);

    //    float floorDiffAmount = (_maxAmount - _minAmouont) / (float)MAX_FLOOR_DIFF;
    //    int currentFloor = _currentFloorList.floor;
    //    for (int i = currentFloor - 1; i >= _minFloor; --i)
    //    {
    //        int floorDiff = currentFloor - i;
    //        float startAmount = floorDiffAmount * (floorDiff - 1);
    //        float endAmount = (floorDiffAmount * floorDiff);
    //        float amount = Mathf.Lerp(startAmount, endAmount, ratio);

    //        _floorDic[i].SetBlur(amount);
    //    }
    //}
}
