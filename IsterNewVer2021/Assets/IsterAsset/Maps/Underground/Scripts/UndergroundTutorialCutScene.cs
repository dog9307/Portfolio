using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UndergroundTutorialCutScene : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _playerVCam;
    [SerializeField]
    private CinemachineVirtualCamera _illimaVCam;

    //[SerializeField]
    //private Transform _portal;

    //[SerializeField]
    //private DissolveApplier _illimaDissolve;

    private PlayerMoveController _player;

    [SerializeField]
    private LobbyStartDialogue _dialogue;

    [SerializeField]
    private TestDummyAnimationController _illimaAnim;

    [SerializeField]
    private SFXPlayer _sfx;

    [SerializeField]
    private FadingGuideUI _activeEquipTuto;

    public void StartCutScene()
    {
        Invoke("CutSceneStart", 2.0f);
    }

    void CutSceneStart()
    {
        _player = FindObjectOfType<PlayerFindHelper>().player.GetComponent<PlayerMoveController>();
        _player.PlayerMoveFreeze(true);

        StartCoroutine(CutScene());
    }

    // Test
    [SerializeField]
    private CutSceneController _illimaAppearCutScene;
    IEnumerator CutScene()
    {
        //yield return new WaitForSeconds(0.5f);

        CinemachineBrain brain = FindObjectOfType<CinemachineBrain>();
        //brain.m_DefaultBlend.m_Time = illimaVCamTime;

        //int prevPlayerVCamPriority = _playerVCam.Priority;
        //_playerVCam.Priority = 0;

        //_player.GetComponent<LookAtMouse>().dir = CommonFuncs.CalcDir(_player, _illimaDissolve);

        //_sfx.PlaySFX("portal_open");

        //float portalScaleTime = 1.5f;
        //float currentTime = 0.0f;
        //float ratio = 0.0f;
        //float portalScaleFator = 3.0f;
        //while (currentTime < portalScaleTime)
        //{
        //    ratio = currentTime / portalScaleTime;

        //    float scaleFactor = Mathf.Lerp(0.0f, portalScaleFator, ratio);
        //    _portal.localScale = new Vector3(scaleFactor, portalScaleFator, portalScaleFator);

        //    yield return null;

        //    currentTime += IsterTimeManager.deltaTime;
        //}
        //_portal.localScale = new Vector3(portalScaleFator, portalScaleFator, portalScaleFator);


        //_sfx.PlaySFX("illima_appear");

        //currentTime = 0.0f;
        //ratio = 0.0f;
        //float illimaDissolveTime = 2.5f;
        //while (currentTime < illimaDissolveTime)
        //{
        //    ratio = currentTime / illimaDissolveTime;
        //    _illimaDissolve.currentFade = ratio;

        //    yield return null;

        //    currentTime += IsterTimeManager.deltaTime;
        //}
        //_illimaDissolve.currentFade = 1.0f;

        //yield return new WaitForSeconds(1.0f);

        //brain.m_DefaultBlend.m_Time = 1.5f;
        //_playerVCam.m_Lens.OrthographicSize = 16.0f;
        //_playerVCam.Priority = prevPlayerVCamPriority;

        //yield return new WaitForSeconds(2.0f);

        //_dialogue.StartDialogue();

        _illimaAppearCutScene.StartCutScene();
        while (_illimaAppearCutScene.isStart)
            yield return null;

        // 탭 열 때까지 기다림
        //while (!UIManager.instance.tabOn)
        //    yield return null;

        //KeyManager.instance.Disable("tabUI");
        //KeyManager.instance.Disable("menu");
        //_activeEquipTuto.StartFading(1.0f);
        //PlayerSkillUsage playerSkill = _player.GetComponent<PlayerSkillUsage>();

        //// 스킬 장착 기다림
        //bool isEnd = false;
        //while (!isEnd)
        //{
        //    yield return null;

        //    int count = 0;
        //    foreach (var skill in playerSkill.activeSkills)
        //    {
        //        if (skill != null)
        //            count++;
        //    }

        //    if (count > 0)
        //        isEnd = true;
        //}
        //_activeEquipTuto.StartFading(0.0f);
        //KeyManager.instance.Enable("tabUI");
        //KeyManager.instance.Enable("menu");

        //// 탭 다시 닫을 때까지 기다림
        //while (UIManager.instance.tabOn)
        //    yield return null;

        //yield return new WaitForSeconds(1.5f);

        //DialogueManager.instance.AddEndEvent(IllimaDialogueEnd);

        //_illimaDialogue.StartDialogue();

        brain.m_DefaultBlend.m_Time = 0.0f;
    }

    private bool _illimaAttackEnd = false;
    public void IllimaDialogueEnd()
    {
        _playerVCam.Follow = null;
        FindingPlayerVCam find = _playerVCam.GetComponent<FindingPlayerVCam>();
        if (find)
            find.enabled = false;

        _illimaAttackEnd = false;

        Invoke("PlayerFreeze", 0.1f);

        StartCoroutine(ChangeMap());
    }

    public void PlayerFreeze()
    {
        _player.PlayerMoveFreeze(true);
    }

    IEnumerator ChangeMap()
    {
        yield return new WaitForSeconds(2.0f);

        _illimaAnim.Attack();

        while (!_illimaAttackEnd)
            yield return null;

        _player.Move(new Vector3(100.0f, 100.0f, 0.0f));

        PlayerMapChanger mapChanger = FindObjectOfType<PlayerMapChanger>();
        if (mapChanger)
            mapChanger.ChangeMap("WorldScene", 5.0f);

        StageBGMPlayer bgm = FindObjectOfType<StageBGMPlayer>();
        if (bgm)
            bgm.StopAmbient();
    }

    public void PlayerGoToUniverse()
    {
        _illimaAttackEnd = true;
    }
}
