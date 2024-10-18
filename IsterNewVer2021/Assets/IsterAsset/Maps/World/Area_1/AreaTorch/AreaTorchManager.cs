using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTorchManager : SavableObject
{
    private int _currentColor = -1;
    private int _prevColor = -1;

    [SerializeField]
    private ParticleSystem.MinMaxGradient _startColor;
    [SerializeField]
    private ParticleSystem.MinMaxGradient[] _colors;

    [SerializeField]
    private AreaTorchController[] _torches;
    [SerializeField]
    private AreaTorchWall[] _walls;

    [SerializeField]
    private float _dissolveDuration = 1.0f;
    [SerializeField]
    private DissolveApplier[] _appearDissolves;
    [SerializeField]
    private DissolveApplier[] _disappearDissolves;

    [System.Serializable]
    public struct DissolveArray
    {
        [SerializeField]
        public DissolveApplier[] dissolves;
    }
    [SerializeField]
    private List<DissolveArray> _dissolves;

    public override SavableNode[] GetSaveNodes()
    {
        SavableNode[] nodes = new SavableNode[1];

        nodes[0] = new SavableNode();
        nodes[0].key = _key;
        nodes[0].value = _currentColor;

        return nodes;
    }

    public void LoadLastColor()
    {
        _currentColor = PlayerPrefs.GetInt(_key, -1);

        TorchColorChange();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadLastColor();
    }

    void TorchColorChange()
    {
        if (_dissolves == null) return;

        ParticleSystem.MinMaxGradient nextColor = new ParticleSystem.MinMaxGradient();

        bool isWallControl = false;
        if (_currentColor < 0)
        {
            nextColor = _startColor;
            _currentColor = (int)AreaTorchType.Yellow;
        }
        else
        {
            isWallControl = true;
            nextColor = _colors[_currentColor];
        }

        foreach (var t in _torches)
            t.TorchColorChange(nextColor);

        if (isWallControl)
        {
            foreach (var w in _walls)
            {
                if (w.type == (AreaTorchType)_currentColor)
                    w.TurnOffWall();
                else
                    w.TurnOnWall();
            }
        }
    }

    public void WallDissolveStart()
    {
        if (_prevColor >= 0)
            _appearDissolves = _dissolves[_prevColor].dissolves;
        _disappearDissolves = _dissolves[_currentColor].dissolves;

        StartCoroutine(Dissolve());
    }

    IEnumerator Dissolve()
    {
        float currentTime = 0.0f;
        while (currentTime < _dissolveDuration)
        {
            float ratio = currentTime / _dissolveDuration;

            if (_appearDissolves != null)
            {
                foreach (var d in _appearDissolves)
                    d.currentFade = ratio;
            }
            if (_disappearDissolves != null)
            {
                foreach (var d in _disappearDissolves)
                    d.currentFade = (1.0f - ratio);
            }

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }

        if (_appearDissolves != null)
        {
            foreach (var d in _appearDissolves)
                d.currentFade = 1.0f;
        }
        if (_disappearDissolves != null)
        {
            foreach (var d in _disappearDissolves)
                d.currentFade = 0.0f;
        }

        _prevColor = _currentColor;
    }

    public void TorchSignal()
    {
        _currentColor = (_currentColor + 1) % _colors.Length;

        TorchColorChange();
        //WallDissolveStart();

        CameraShakeController.instance.CameraShake(10.0f);
    }
}
