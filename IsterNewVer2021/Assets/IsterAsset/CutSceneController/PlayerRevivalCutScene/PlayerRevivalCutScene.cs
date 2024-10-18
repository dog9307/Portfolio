using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRevivalCutScene : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] _renderers;
    [SerializeField]
    private ParticleSystem[] _effects;

    [HideInInspector]
    [SerializeField]
    private MapStartPos _startPos;

    public struct SortingInfo
    {
        public string layer;
        public int order;
    }

    private List<SortingInfo> _rendererInfoes;
    private List<SortingInfo> _effectInfoes;

    [SerializeField]
    private CutSceneController _revivalCutScene;

    void Start()
    {
        int startPosID = PlayerPrefs.GetInt("PlayerNextStartPos", -1);
        if (startPosID == _startPos.id)
            StartCoroutine(WaitNextFrame());
    }

    IEnumerator WaitNextFrame()
    {
        yield return new WaitForEndOfFrame();

        if (gameObject.activeInHierarchy)
        {
            int playerDie = PlayerPrefs.GetInt("PlayerDie", -1);
            if (playerDie >= 100)
            {
                _revivalCutScene.StartCutScene();

                PlayerPrefs.SetInt("PlayerDie", -1);
            }
        }
    }

    public void OnPreCutScene()
    {
        _rendererInfoes = new List<SortingInfo>();
        _effectInfoes = new List<SortingInfo>();

        foreach (var r in _renderers)
        {
            SortingInfo info = new SortingInfo();

            info.layer = r.sortingLayerName;
            info.order = r.sortingOrder;

            _rendererInfoes.Add(info);

            r.sortingLayerName = "Depth";
            r.sortingOrder = 1010;
        }

        foreach (var p in _effects)
        {
            ParticleSystemRenderer renderer = p.GetComponent<ParticleSystemRenderer>();

            SortingInfo info = new SortingInfo();

            if (!renderer)
            {
                _effectInfoes.Add(info);
                continue;
            }

            info.layer = renderer.sortingLayerName;
            info.order = renderer.sortingOrder;

            _effectInfoes.Add(info);

            renderer.sortingLayerName = "Depth";
            renderer.sortingOrder = 1009;
        }
    }

    public void OnPostCutScene()
    {
        for (int i = 0; i < _renderers.Length; ++i)
        {
            SortingInfo info = _rendererInfoes[i];

            _renderers[i].sortingLayerName = info.layer;
            _renderers[i].sortingOrder = info.order;
        }

        for (int i = 0; i < _effects.Length; ++i)
        {
            ParticleSystemRenderer renderer = _effects[i].GetComponent<ParticleSystemRenderer>();

            SortingInfo info = _effectInfoes[i];

            if (!renderer) continue;

            renderer.sortingLayerName = info.layer;
            renderer.sortingOrder = info.order;
        }

        _rendererInfoes.Clear();
        _effectInfoes.Clear();
    }

    public void PlayerFreeze()
    {
        PlayerMoveController player = FindObjectOfType<PlayerMoveController>();
        player.PlayerMoveFreeze(true);
    }
}
