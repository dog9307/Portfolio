using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TowerEndStairCutScene : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _endStairStart;
    [SerializeField]
    private SpriteRenderer _endStairMiddle;
    [SerializeField]
    private SpriteRenderer _endStairEnd;
    [SerializeField]
    private Collider2D _endTrigger;

    [SerializeField]
    private WalkThroughCutScene _upstairCutScene;
    [SerializeField]
    private WalkThroughCutScene _walkCutScene;

    private PlayerMoveController _player;
    private LookAtMouse _look;
    private Rigidbody2D _rigid;

    [SerializeField]
    private SpriteRenderer[] _endToBackRenderers;

    [SerializeField]
    private CinemachineVirtualCamera _zoomOutVCam;
    [SerializeField]
    private float _orthographicMax = 16.0f;
    [SerializeField]
    private float _zoomOutDelayTime = 5.0f;
    [SerializeField]
    private float _zoomOutTime = 2.0f;

    [SerializeField]
    private SpriteRenderer _endStairWall;

    [SerializeField]
    private Transform _endStairPatternPos;

    // Start is called before the first frame update
    void Start()
    {
        _endStairStart.sortingLayerName = "Foreground";
        _endStairMiddle.sortingLayerName = "Foreground";
        _endStairEnd.sortingLayerName = "Foreground";

        _endStairWall.sortingLayerName = "Background";
         

        _endTrigger.enabled = false;

        if (_endStairPatternPos)
            _endStairPatternPos.gameObject.SetActive(false);
    }

    public void EndBattle()
    {
        int sortinglayer = 200;
        foreach (var renderer in _endToBackRenderers)
        {
            renderer.sortingLayerName = "Background";
            renderer.sortingOrder = sortinglayer;
            sortinglayer++;
        }

        _endStairWall.sortingLayerName = "Foreground";

        _endTrigger.enabled = true;

        if (_endStairPatternPos)
        {
            _endStairPatternPos.gameObject.SetActive(true);
            EffectMoveToStair();
        }
    }

    void EffectMoveToStair()
    {
        PlayerInventory _inventory = FindObjectOfType<PlayerInventory>();
        if (!_inventory) return;

        _inventory.RemoveRuleItem(100);
        _inventory.RemoveRuleItem(101);
        _inventory.RemoveRuleItem(102);
        _inventory.RemoveRuleItem(103);

        TowerF1Manager manager = FindObjectOfType<TowerF1Manager>();
        List<TowerF1BuffEffectController> effects = manager.effects;
        foreach (var effect in effects)
        {
            if (!effect) continue;

            effect.MoveStart(_endStairPatternPos);
        }
    }

    private CinemachineBrain _brain;
    public void CutSceneStart()
    {
        _player = FindObjectOfType<PlayerMoveController>();
        _look = _player.GetComponent<LookAtMouse>();
        _rigid = _player.GetComponent<Rigidbody2D>();

        _upstairCutScene.OnDuringWalk.AddListener(OnDuringUp);
        _upstairCutScene.startPos.position = _player.transform.position;

        _walkCutScene.OnDuringWalk.AddListener(OnDuringWalk);

        _brain = FindObjectOfType<CinemachineBrain>();
        _brain.m_DefaultBlend.m_Time = 1.0f;

        _zoomOutVCam.Priority = 200;

        StartCoroutine(CutScene());

        // for OBT
        FindObjectOfType<GoToOutro>().StartFading(_walkCutScene.walkTime + 2.0f);
    }

    public void OnDuringUp()
    {
        _look.dir = _upstairCutScene.moveDir;
        _rigid.velocity = _upstairCutScene.moveDir * 0.02f;
        _rigid.GetComponent<Animator>().SetFloat("velocity", 1.0f);
    }

    public void OnDuringWalk()
    {
        _look.dir = _walkCutScene.moveDir;
        _rigid.velocity = _walkCutScene.moveDir * 0.02f;
        _rigid.GetComponent<Animator>().SetFloat("velocity", 1.0f);
    }

    IEnumerator CutScene()
    {
        _player.PlayerMoveFreeze(true);

        _upstairCutScene.StartWalk(_player.transform);
        while (!_upstairCutScene.isWalkEnd)
            yield return null;

        StageBGMPlayer bgm = FindObjectOfType<StageBGMPlayer>();
        if (bgm)
            bgm.StopAmbient(_zoomOutDelayTime + _walkCutScene.walkTime);

        float ratio = 0.0f;
        float startOrtho = _zoomOutVCam.m_Lens.OrthographicSize;
        _walkCutScene.StartWalk(_player.transform);

        yield return new WaitForSeconds(_zoomOutDelayTime);

        while (!_walkCutScene.isWalkEnd)
        {
            ratio += IsterTimeManager.originDeltaTime / _zoomOutTime;
            ratio = (ratio < 1.0f ? ratio : 1.0f);

            _zoomOutVCam.m_Lens.OrthographicSize = Mathf.Lerp(startOrtho, _orthographicMax, ratio);

            yield return null;
        }

        _brain.m_DefaultBlend.m_Time = 0.0f;
    }
}
