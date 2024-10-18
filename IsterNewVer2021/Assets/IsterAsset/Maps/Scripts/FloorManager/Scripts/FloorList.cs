using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloorList : MonoBehaviour
{
    [SerializeField]
    private FloorManager _manager;
    public FloorManager manager { get { return _manager; } set { manager = value; } }

    [SerializeField]
    private int _floor;
    public int floor { get { return _floor; } }

    [SerializeField]
    private List<Collider2D> _colList;

    [SerializeField]
    private List<SortingLayerInfo> _infoes;
    private Dictionary<SortingLayerInfo, FloorHSVController> _hsvConDic = new Dictionary<SortingLayerInfo, FloorHSVController>();

    public UnityEvent OnEnterFloor;
    public UnityEvent OnExitFloor;

    [SerializeField]
    private float _minVMulti = 0.1f;
    [SerializeField]
    private float _maxVMulti = 1.0f;
    private float _currentVMulti = 1.0f;

    // Start is called before the first frame update
    void Awake()
    {
        if (!manager)
        {
            Destroy(this);
            return;
        }

        manager.AddFloor(_floor, this);
    }

    void Start()
    {
        for (int i = 0; i < _colList.Count; ++i)
        {
            if (!_colList[i])
            {
                _colList.RemoveAt(i);
                --i;
            }
        }

        if (_hsvConDic == null)
            _hsvConDic = new Dictionary<SortingLayerInfo, FloorHSVController>();

        for (int i = 0; i < _infoes.Count; ++i)
        {
            if (!_infoes[i])
            {
                _infoes.RemoveAt(i);
                --i;
                continue;
            }

            if (_hsvConDic.ContainsKey(_infoes[i])) continue;

            FloorHSVController hsv = _infoes[i].gameObject.AddComponent<FloorHSVController>();
            _hsvConDic.Add(_infoes[i], hsv);
        }
    }

    public void AddHelperList(FloorListHelper helper)
    {
        if (helper.colList != null)
        {
            if (_colList != null)
                _colList.AddRange(helper.colList);
            else
            {
                if (helper.colList.Count > 0)
                {
                    _colList = new List<Collider2D>();
                    _colList.AddRange(helper.colList);
                }
            }
        }

        if (helper.infoes != null)
        {
            if (_infoes != null)
                _infoes.AddRange(helper.infoes);
            else
            {
                if (helper.infoes.Count > 0)
                {
                    _infoes = new List<SortingLayerInfo>();
                    _infoes.AddRange(helper.infoes);
                }
            }

            foreach (var i in helper.infoes)
            {
                if (_hsvConDic.ContainsKey(i)) continue;

                FloorHSVController hsv = i.gameObject.AddComponent<FloorHSVController>();
                _hsvConDic.Add(i, hsv);
            }
        }
    }

    public void AddHelperList(List<Collider2D> colList, List<SortingLayerInfo> infoes)
    {
        if (colList != null)
        {
            if (_colList != null)
                _colList.AddRange(colList);
            else
            {
                if (colList.Count > 0)
                {
                    _colList = new List<Collider2D>();
                    _colList.AddRange(colList);
                }
            }
        }

        if (infoes != null)
        {
            if (_infoes != null)
                _infoes.AddRange(infoes);
            else
            {
                if (infoes.Count > 0)
                {
                    _infoes = new List<SortingLayerInfo>();
                    _infoes.AddRange(infoes);
                }
            }

            foreach (var i in infoes)
            {
                if (_hsvConDic.ContainsKey(i)) continue;

                FloorHSVController hsv = i.gameObject.AddComponent<FloorHSVController>();
                _hsvConDic.Add(i, hsv);
            }
        }
    }

    public void ColliderControl(bool isOn)
    {
        if (_colList == null) return;
        if (_colList.Count <= 0) return;

        for (int i = 0; i < _colList.Count; ++i)
        {
            if (!_colList[i])
            {
                _colList.RemoveAt(i);
                --i;
                continue;
            }

            _colList[i].enabled = isOn;
        }
    }

    public void SpriteControl(SORTING layer)
    {
        if (_infoes == null) return;

        if (layer == SORTING.NONE)
        {
            for (int i = 0; i < _infoes.Count; ++i)
            {
                if (!_infoes[i])
                {
                    _infoes.RemoveAt(i);
                    --i;
                    continue;
                }

                _infoes[i].ReturnSorting();
            }
        }
        else
        {
            for (int i = 0; i < _infoes.Count; ++i)
            {
                if (!_infoes[i])
                {
                    _infoes.RemoveAt(i);
                    --i;
                    continue;
                }

                _infoes[i].ChangeSortingLayer(layer.ToString());
            }
        }
    }

    private Coroutine _colorVCoroutine;
    public void ColorVControl(int floorDiff, bool isSkip = false)
    {
        float multi = floorDiff * 0.3f;
        float targetMulti = Mathf.Clamp(1.0f - multi, _minVMulti, _maxVMulti);

        if (isSkip)
            ApplyColorV(targetMulti);
        else
        {
            if (_colorVCoroutine != null)
                StopCoroutine(_colorVCoroutine);

            _colorVCoroutine = StartCoroutine(ColorVChange(targetMulti));
        }
    }

    IEnumerator ColorVChange(float targetMulti)
    {
        float startMulti = _currentVMulti;
        float currentTime = 0.0f;
        float totalTime = 0.5f;
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;

            _currentVMulti = Mathf.Lerp(startMulti, targetMulti, ratio);

            ApplyColorV(_currentVMulti);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        _currentVMulti = targetMulti;

        ApplyColorV(_currentVMulti);

        _colorVCoroutine = null;
    }

    public void ApplyColorV(float vMulti)
    {
        _currentVMulti = vMulti;
        foreach (var info in _infoes)
        {
            if (!info) continue;

            if (_hsvConDic.ContainsKey(info))
                _hsvConDic[info].MultiplyColorV(vMulti);
        }
    }

    public void EnterFloorTodo()
    {
        if (OnEnterFloor != null)
            OnEnterFloor.Invoke();
    }

    public void ExitFloorTodo()
    {
        if (OnExitFloor != null)
            OnExitFloor.Invoke();
    }
}
