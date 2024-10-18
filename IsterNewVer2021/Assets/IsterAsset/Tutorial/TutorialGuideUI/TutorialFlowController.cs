using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TutorialFlowController : MonoBehaviour
{
    [Header("체인 관련")]
    [SerializeField]
    private TutorialPopUp _moveTutoPopUp;
    //[SerializeField]
    //private TutorialFadingKeyTrigger _arrowTutorial;
    [SerializeField]
    private GameObject _chainOpus;

    [SerializeField]
    private GameObject _darkLight;
    [SerializeField]
    private CutSceneController _darkLightCutScene;

    [SerializeField]
    private TutorialPopUp _attackPopUp;

    [SerializeField]
    private GameObject _ban;
    [SerializeField]
    private GameObject _banDialogueCutScene_0;
    [SerializeField]
    private GameObject _banCutSceneColliderTrigger;

    [SerializeField]
    private GameObject _chainDoor;

    [SerializeField]
    private GameObject _attackTuto;
    [SerializeField]
    private GameObject _attackTuto1;
    [SerializeField]
    private GameObject _breakingObject_0;
    [SerializeField]
    private GameObject _breakingObject_1;

    [SerializeField]
    private GameObject _saveTutoPopUp;
    [SerializeField]
    private GameObject _saveTuto;

    [SerializeField]
    private GameObject _relicLight;

    //[Header("레버 관련")]
    //[SerializeField]
    //private GameObject _leverTuto;

    [Header("대시 관련")]
    [SerializeField]
    private GameObject _dashPopUp;
    [SerializeField]
    private GameObject _dashUseTuto;
    [SerializeField]
    private GameObject _dashUseTuto2;

    private PlayerMoveController _player;

    [Header("스킬 관련")]
    [SerializeField]
    private TutorialPopUp _skillGainPopUp;
    [SerializeField]
    private TutorialPopUp _tabPopUp;

    [Header("텔포 관련")]
    [SerializeField]
    private TutorialPopUp _teleportPopUp;

    [Header("전투 관련")]
    [SerializeField]
    private Collider2D _tutoDoorCol;
    [SerializeField]
    private CutSceneController _battleCutScene;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerFindHelper>().player.GetComponent<PlayerMoveController>();

        int chainCount = PlayerPrefs.GetInt("UndergroundChainDone", 0);
        if (chainCount != 0)
        {
            if (_moveTutoPopUp)
                Destroy(_moveTutoPopUp.gameObject);

            Destroy(_ban);
            Destroy(_chainOpus);

            Destroy(_darkLight);
            Destroy(_darkLightCutScene.gameObject);

            Destroy(_banDialogueCutScene_0);
            Destroy(_banCutSceneColliderTrigger);

            Destroy(_chainDoor);

            Destroy(_attackTuto);
            Destroy(_attackTuto1);
            Destroy(_breakingObject_0);
            Destroy(_breakingObject_1);

            Destroy(_saveTutoPopUp);
            Destroy(_saveTuto);
        }

        //int leverCount = PlayerPrefs.GetInt("NormalLever_1", 0);
        //if (leverCount != 0)
        //{
        //    Destroy(_leverTuto);
        //}
        //else
        //{
        //    NormalLeverTalkFrom lever = FindObjectOfType<NormalLeverTalkFrom>();
        //    if (lever)
        //        lever.OnLeverActive.AddListener(LeverTutoEnd);
        //}

        int dashCount = PlayerPrefs.GetInt("DashGain", 0);
        if (dashCount != 0)
        {
            Destroy(_dashPopUp);

            Destroy(_dashUseTuto);
            Destroy(_dashUseTuto2);
        }

        //if (_skillGainPopUp)
        //{
        //    int skillCount = PlayerPrefs.GetInt("SkillTutoEnd", 0);
        //    if (skillCount != 0)
        //    {
        //        Destroy(_skillGainPopUp.gameObject);
        //    }
        //    else
        //        StartCoroutine(PlayerSkillWait());
        //}


        int tabCount = PlayerPrefs.GetInt("TabTutoEnd", 0);
        if (tabCount != 0)
        {
            Destroy(_tabPopUp.gameObject);
            KeyManager.instance.Enable("tabUI");
        }
        else
        {
            KeyManager.instance.Disable("tabUI");
            StartCoroutine(TabTutoWait());
        }

        if (_teleportPopUp)
        {
            int teleportCount = PlayerPrefs.GetInt("VanRuinFirstAppear", 0);
            if (teleportCount != 0)
            {
                Destroy(_teleportPopUp.transform.parent.gameObject);
            }
        }

        //_ban.SetActive(false);

        //if (_player)
        //    BlackMaskController.instance.AddEvent(MoveFreeze, BlackMaskEventType.POST);
    }

    IEnumerator TabTutoWait()
    {
        PlayerInventory inventory = null;
        while (!inventory)
        {
            inventory = FindObjectOfType<PlayerInventory>();

            yield return null;
        }

        while (inventory.relicInventory.items.Count <= 0)
            yield return null;

        if (_tabPopUp)
        {
            _tabPopUp.StartPopUp();
            while (!_tabPopUp.isPopUpOver)
                yield return null;
        }

        KeyManager.instance.Enable("tabUI");

        PlayerPrefs.SetInt("TabTutoEnd", 100);
    }

    IEnumerator PlayerSkillWait()
    {
        PlayerSkillUsage playerSkill = _player.GetComponent<PlayerSkillUsage>();
        while (playerSkill.activeSkills.Count <= 0)
            yield return null;

        if (_skillGainPopUp)
        {
            _skillGainPopUp.StartPopUp();
            while (!_skillGainPopUp.isPopUpOver)
                yield return null;
        }

        if (_tabPopUp)
        {
            _tabPopUp.StartPopUp();
            while (!_tabPopUp.isPopUpOver)
                yield return null;
        }

        PlayerPrefs.SetInt("SkillTutoEnd", 100);
    }

    //public void LeverTutoEnd()
    //{
    //    _leverTuto.GetComponent<FadingGuideUI>().StartFading(0.0f);
    //}

    public void SavePointTutoEnd()
    {
        _saveTuto.GetComponent<FadingGuideUI>().StartFading(0.0f);
        //Destroy(_saveTuto);
    }

    public void MoveFreeze()
    {
        //_sword.SetActive(true);

        //_arrowTutorial.isEnable = false;
        StartCoroutine(Freezing());
    }

    IEnumerator Freezing()
    {
        if (_moveTutoPopUp)
        {
            _moveTutoPopUp.StartPopUp();
            while (!_moveTutoPopUp.isPopUpOver)
                yield return null;
        }

        PlayerAnimController anim = _player.GetComponent<PlayerAnimController>();

        float currentTime = 0.0f;
        while (currentTime < 0.5f)
        {
            anim.CharacterSetDir(Vector2.down, 0.0f);
            _player.PlayerMoveFreeze(true);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }

        yield return new WaitForSeconds(1.0f);

        //_arrowTutorial.isEnable = true;

        _player.PlayerMoveFreeze(false);

        //int count = 0;
        //while (count < 100)
        //{
        //    yield return null;

        //    count = SavableDataManager.instance.FindIntSavableData("RelicLightGain");
        //}

        while (_darkLight)
            yield return null;

        if (_darkLightCutScene)
            _darkLightCutScene.StartCutScene();

        while (CutSceneController.isAnyCutsceneAlreayDoing)
            yield return null;

        FindObjectOfType<PassiveSkillSlotList>().SkillSlotOn(0);
        if (_attackPopUp)
        {
            _attackPopUp.StartPopUp();
            while (!_attackPopUp.isPopUpOver)
                yield return null;
        }

        //if (_dashPopUp)
        //    _dashPopUp.GetComponent<TutorialPopUp>().StartPopUp();

        // 튜토리얼 전투 관련
        while (_tutoDoorCol.enabled)
            yield return null;

        yield return new WaitForSeconds(0.3f);

        if (_battleCutScene)
            _battleCutScene.StartCutScene();
    }

    public void PlayerGainDash()
    {
        FindObjectOfType<PlayerSkillUsage>().ActiveDashGain();
    }

    public void DarkLightGain()
    {
        SavableNode savable = new SavableNode();
        savable.key = "swordCount";
        savable.value = 100;

        SavableDataManager.instance.AddSavableObject(savable);

        FindObjectOfType<PlayerAttacker>().SwordAppear();
        FindObjectOfType<PlayerDarkLightUIFinder>().GainDarkLight();
        FindObjectOfType<PlayerHPUIController>().HPChange();

        SavePointController savePoint = FindObjectOfType<SavePointController>();
        if (savePoint)
            savePoint.OnSave.AddListener(SavePointTutoEnd);

        KeyManager.instance.Enable("attack_left");
        //KeyManager.instance.Enable("tabUI");
    }

    [YarnCommand("DarkLightOpen")]
    public void DarkLightOpen()
    {
        if (_darkLight)
            _darkLight.SetActive(true);
    }

    public void TutorialBattleEnd()
    {
        SavableNode node = new SavableNode();

        node.key = "Tutorial_battle";
        node.value = 100;

        SavableDataManager.instance.AddSavableObject(node);
    }
}
