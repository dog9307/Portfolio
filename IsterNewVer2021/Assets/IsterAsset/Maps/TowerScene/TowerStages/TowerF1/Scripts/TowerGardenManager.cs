using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TowerGardenManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _bossRoomFlowers = new List<Transform>();

    [SerializeField]
    private BossController _relativeBoss;

    private PlayerMoveController _player;

    [SerializeField]
    private GameObject _bossReward0;
    [SerializeField]
    private GameObject _bossReward1;
    [SerializeField]
    private FieldDoorController _toMiddleRoom;
    public bool isBossBattleEnd { get; set; }

    public static float MoreDamageH         = 117.0f;
    public static float AtkDecreaseH        = 0.0f;
    public static float CoolTimeIncreaseH   = 23.0f;
    public static float SlowH               = 51.0f;

    void Start()
    {
        PlayerPrefs.SetInt("TowerGardenIn", 100);
        isBossBattleEnd = false;

        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        _player.GetComponent<PlayerAttacker>().isBattle = true;
    }

    public void BossBattleStart()
    {
        //if (_player)
        //{
        //    PlayerInventory inventory = _player.GetComponent<PlayerInventory>();
        //    DebuffInfo playerDebuff = _player.GetComponent<DebuffInfo>();
        //    if (playerDebuff && inventory)
        //    {
        //        TowerRuleItemBase flower = null;
        //        // slow
        //        flower = inventory.FindRuleItem(100);
        //        if (flower == null)
        //            playerDebuff.AddSlow(0.45f);
        //        else
        //        {
        //            Transform moveTarget = FindBuffEffect(FlowerType.Slow);
        //            Transform moveDest = _bossRoomFlowers[0];

        //            StartCoroutine(EffectMove(moveTarget, moveDest));
        //        }

        //        // AtkDecrease
        //        flower = inventory.FindRuleItem(101);
        //        if (flower == null)
        //            playerDebuff.damageDecreaseMultiplier = 0.8f;
        //        else
        //        {
        //            Transform moveTarget = FindBuffEffect(FlowerType.AtkDecrease);
        //            Transform moveDest = _bossRoomFlowers[1];

        //            StartCoroutine(EffectMove(moveTarget, moveDest));
        //        }

        //        // CoolTimeIncrease
        //        flower = inventory.FindRuleItem(102);
        //        if (flower == null)
        //            playerDebuff.coolTimeMultiplier = 2.0f;
        //        else
        //        {
        //            Transform moveTarget = FindBuffEffect(FlowerType.CoolTimeIncrease);
        //            Transform moveDest = _bossRoomFlowers[2];

        //            StartCoroutine(EffectMove(moveTarget, moveDest));
        //        }

        //        // MoreDamage
        //        flower = inventory.FindRuleItem(103);
        //        if (flower == null)
        //            playerDebuff.getMoreDamage = 1.0f;
        //        else
        //        {
        //            Transform moveTarget = FindBuffEffect(FlowerType.MoreDamage);
        //            Transform moveDest = _bossRoomFlowers[3];

        //            StartCoroutine(EffectMove(moveTarget, moveDest));
        //        }
        //    }
        //}

        if (_relativeBoss)
            _relativeBoss.BattleStart();

        isBossBattleEnd = false;
    }

    public void BattleEnd()
    {
        _player.GetComponent<PlayerAttacker>().isBattle = false;
        isBossBattleEnd = true;
        StartCoroutine(WaitForItemGain());
    }

    IEnumerator WaitForItemGain()
    {
        while (_bossReward0)
            yield return null;
        while (_bossReward1)
            yield return null;

        yield return new WaitForSeconds(1.3f);

        FindObjectOfType<PlayerMoveController>().PlayerMoveFreeze(true);

        if (_toMiddleRoom)
            _toMiddleRoom.GoToNextField();
    }

    [SerializeField]
    private SFXPlayer _sfx;
    public List<TowerF1BuffEffectController> effects { get; set; } = new List<TowerF1BuffEffectController>();
    [SerializeField]
    private float _effectMoveTime = 3.0f;
    public void AddBuffEffect(TowerF1BuffEffectController effect)
    {
        effects.Add(effect);
    }

    public Transform FindBuffEffect(FlowerType type)
    {
        Transform effect = null;
        foreach (var e in effects)
        {
            if (e.type == type)
            {
                effect = e.transform;
                break;
            }
        }

        return effect;
    }

    IEnumerator EffectMove(Transform moveTarget, Transform moveDest)
    {
        if (_sfx)
            _sfx.PlaySFX("flower_flying");

        //_isEffectMoveEnd = false;
        moveTarget.transform.parent = moveDest;

        float currentTime = 0.0f;
        Vector2 startPos = moveTarget.localPosition;
        while (currentTime < _effectMoveTime)
        {
            float ratio = currentTime / _effectMoveTime;
            moveTarget.localPosition = Vector2.Lerp(startPos, Vector2.zero, ratio);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }

        TowerF1BuffEffectController effect = moveTarget.GetComponent<TowerF1BuffEffectController>();
        if (effect)
            effect.Destroy();

        _sfx.PlaySFX("debuffoff");

        TowerF1Flower flower = moveDest.GetComponent<TowerF1Flower>();
        if (flower)
            flower.isDieFlower = true;

        GlowUpAndDown glow = moveDest.GetComponentInChildren<GlowUpAndDown>();
        float min = glow.minIntensity;
        float max = glow.maxIntensity;
        SpriteRenderer renderer = glow.GetComponent<SpriteRenderer>();
        Color color = renderer.color;
        currentTime = 0.0f;
        while (currentTime < 0.5f)
        {
            float ratio = currentTime / 0.5f;

            glow.minIntensity = Mathf.Lerp(min, 0.0f, ratio);
            glow.maxIntensity = Mathf.Lerp(max, 0.0f, ratio);

            renderer.color = Color.Lerp(color, Color.white, ratio);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        glow.minIntensity = 0.0f;
        glow.maxIntensity = 0.0f;
        renderer.color = Color.white;

        renderer.GetComponent<Animator>().enabled = false;

        //TowerF1FlowerUIController ui = FindObjectOfType<TowerF1FlowerUIController>();
        //if (ui)
        //    ui.UIOff(_keyEffect.type);

        //_isEffectMoveEnd = true;
    }

    [SerializeField]
    private FieldDoorController _bossDoor;
    [SerializeField]
    private TowerGardenWarpTalkFrom _gardenStatue;
    [SerializeField]
    private CutSceneController _statueCutScene;
    [SerializeField]
    private ParticleSystem _teleportEffect;

    private delegate void NextRoom();
    private NextRoom _nextGoToRoom;

    [YarnCommand("GoToBossRoom")]
    public void GoToBossRoom()
    {
        _nextGoToRoom = _bossDoor.GoToNextField;

        StartCoroutine(WaitStatueCutScene());
    }

    [YarnCommand("GoToMoonLight")]
    public void GoToMoonLight()
    {
        _nextGoToRoom = _gardenStatue.GoToMoonLightGarden;

        StartCoroutine(WaitStatueCutScene());
    }

    [YarnCommand("CancleSelect")]
    public void CancleSelect()
    {
        _player.PlayerMoveFreeze(false);
    }

    IEnumerator WaitStatueCutScene()
    {
        _statueCutScene.StartCutScene();

        while (_statueCutScene.isStart)
            yield return null;

        if (_nextGoToRoom != null)
        {
            _nextGoToRoom();
            _nextGoToRoom = null;
        }
    }

    public void PlayerGoAnywhere()
    {
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        _player.Move(new Vector3(1000.0f, -3.253f, 0.0f));
    }

    public void PlayerMove(Transform pos)
    {
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        _player.Move(pos.position);

        if (_teleportEffect)
        {
            _teleportEffect.transform.position = pos.position;
            _teleportEffect.Play();
        }
    }
}
