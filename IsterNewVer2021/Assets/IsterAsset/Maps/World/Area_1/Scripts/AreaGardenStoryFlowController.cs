using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class AreaGardenStoryFlowController : MonoBehaviour
{
    //[Header("넬 대화 관련")]
    //[SerializeField]
    //private GameObject _nellSecondDialogueCutScene;
    //[SerializeField]
    //private Collider2D _secondTrigger;

    [SerializeField]
    private ConditionalDoorTalkFrom _fieldDoor;

    //[SerializeField]
    //private NellAnimController[] _nellAnim;
    //[SerializeField]
    //private NellMoveController[] _nellMove;
    //[SerializeField]
    //private GameObject _nellFirstDialogueRepeat;

    //[Header("넬 도와주기 컷씬flow")]
    //[SerializeField]
    //private CutSceneController _nellTransformation;
    //[SerializeField]
    //private CutSceneController _nellBossDoorOpen;
    //[SerializeField]
    //private CutSceneController _fieldLiatrisDialogue;
    //[SerializeField]
    //private CutSceneController _nellFieldDoorOpen;
    //[SerializeField]
    //private CutSceneController _fieldLiatrisWeaponCutscene;
    //[SerializeField]
    //private CutSceneController _nellFlowerDestroyDialogueCutScene;
    //[SerializeField]
    //private CutSceneController _nellExplainTorchCutScene;

    //public bool isAcceptToNell { get; set; }

    //[SerializeField]
    //private GameObject _fieldFlower;

    [SerializeField]
    private GameObject _reward;

    [Header("보스 전투 후 꺼져야 할것들")]
    [SerializeField]
    private GameObject[] _afterLiatrisDie;
    [SerializeField]
    private GameObject _bossReward;

    // Start is called before the first frame update
    void Start()
    {
        //_fieldDoor.enabled = false;

        int count = 0;

        //count = PlayerPrefs.GetInt("AcceptToNell", 0);
        //if (count >= 100)
        //{
        //    Destroy(_nellSecondDialogueCutScene);
        //    Destroy(_nellFirstDialogueRepeat);

        //    AcceptToNell();

        //    count = PlayerPrefs.GetInt("NellTransformationCutscene", 0);
        //    if (count >= 100)
        //    {
        //        Destroy(_nellTransformation.transform.parent.gameObject);
        //        _nellFieldDoorOpen.transform.parent.gameObject.SetActive(true);
        //    }

        //    count = PlayerPrefs.GetInt("NellOpenFieldDoorCutScene", 0);
        //    if (count >= 100)
        //    {
        //        Destroy(_nellFieldDoorOpen.transform.parent.gameObject);
        //        //_nellExplainTorchCutScene.transform.parent.gameObject.SetActive(true);
        //    }

        //    //count = PlayerPrefs.GetInt("NellExplainTorchDialogue", 0);
        //    //if (count >= 100)
        //    //{
        //    //    Destroy(_nellExplainTorchCutScene.transform.parent.gameObject);
        //    //    //_nellFieldDoorOpen.transform.parent.gameObject.SetActive(true);
        //    //}

        //    count = PlayerPrefs.GetInt("NellFlowerDestroyDialogue", 0);
        //    if (count >= 100)
        //    {
        //        if (_nellFlowerDestroyDialogueCutScene)
        //            Destroy(_nellFlowerDestroyDialogueCutScene.transform.parent.gameObject);
        //        _nellBossDoorOpen.transform.parent.gameObject.SetActive(true);
        //    }

        //    count = PlayerPrefs.GetInt("NellOpenBossDoorCutscene", 0);
        //    if (count >= 100)
        //    {
        //        Destroy(_nellBossDoorOpen.transform.parent.gameObject);
        //        _fieldLiatrisDialogue.transform.parent.gameObject.SetActive(true);
        //    }

        //    count = PlayerPrefs.GetInt("FieldLiatrisDialogueCutScene", 0);
        //    if (count >= 100)
        //    {
        //        _fieldLiatrisDialogue.transform.parent.GetComponent<DisposableObject>().AlreadyUsed();
        //        Destroy(_fieldLiatrisDialogue.transform.parent.gameObject);
        //        //_nellFieldDoorOpen.transform.parent.gameObject.SetActive(true);
        //    }

        //    //count = PlayerPrefs.GetInt("FieldFlower_0", 0);
        //    //if (count >= 100)
        //    //{
        //    //    Destroy(_fieldFlower);
        //    //    _fieldLiatrisWeaponCutscene.transform.parent.gameObject.SetActive(true);
        //    //}

        //    count = PlayerPrefs.GetInt("FieldLiatrisWeaponCutscene", 0);
        //    if (count >= 100)
        //    {
        //        Destroy(_fieldLiatrisWeaponCutscene.transform.parent.gameObject);
        //        _reward.SetActive(true);
        //    }

        //    return;
        //}

        //count = PlayerPrefs.GetInt("RejectToNell", 0);
        //if (count >= 100)
        //{
        //    Destroy(_nellSecondDialogueCutScene);
        //}
        //else
        //{
        //    count = PlayerPrefs.GetInt("NellSecondDialogue", 0);
        //    if (count >= 100)
        //        Destroy(_nellSecondDialogueCutScene);
        //}

        count = PlayerPrefs.GetInt("FieldLiatrisDie", 0);
        if (count >= 100)
        {
            foreach (var g in _afterLiatrisDie)
            {
                if (g)
                    g.SetActive(false);
            }

            _bossReward.SetActive(true);
        }
    }

    // test
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ResetAreaState();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            PlayerPrefs.SetInt("FieldLiatrisDie", 100);
        }
    }

    public void ResetAreaState()
    {
        PlayerPrefs.SetInt("FieldGardenDoorOpened", 0);
        PlayerPrefs.SetInt("FieldGardenBossDoorOpened", 0);
        PlayerPrefs.SetInt("FieldGardenBoosDoorDestroyed", 0);
        PlayerPrefs.SetInt("RejectToNell", 0);
        PlayerPrefs.SetInt("AcceptToNell", 0);
        PlayerPrefs.SetInt("NellFirstDialogue", 0);
        PlayerPrefs.SetInt("NellSecondDialogue", 0);

        PlayerPrefs.SetInt("NellTransformationCutscene", 0);
        PlayerPrefs.SetInt("NellOpenBossDoorCutscene", 0);
        PlayerPrefs.SetInt("FieldLiatrisDialogueCutScene", 0);
        PlayerPrefs.SetInt("NellOpenFieldDoorCutScene", 0);
        PlayerPrefs.SetInt("FieldFlower_0", 0);
        PlayerPrefs.SetInt("FieldLiatrisWeaponCutscene", 0);
        PlayerPrefs.SetInt("NellFlowerDestroyDialogue", 0);
        //PlayerPrefs.SetInt("NellExplainTorchDialogue", 0);

        PlayerPrefs.SetInt("FieldGardenTorchTuto", 0);
        PlayerPrefs.SetInt("TanaFirstMeetCutScene", 0);

        PlayerPrefs.SetInt("FieldLiatrisDie", 0);
    }

    public void DoorBroken()
    {

    }

    //[YarnCommand("AcceptToNell")]
    //public void AcceptToNell()
    //{
    //    SavableNode node = new SavableNode();
    //    node.key = "AcceptToNell";
    //    node.value = 100;

    //    SavableDataManager.instance.AddSavableObject(node);

    //    //_fieldDoor.OpenDoor();

    //    foreach (var n in _nellAnim)
    //    {
    //        if (!n.isButterflyForm)
    //            n.Transformation();
    //    }

    //    foreach (var n in _nellMove)
    //    {
    //        n.isFollowPlayer = true;
    //        n.playerFollowDistance = 2.5f;
    //        n.followTarget = null;

    //        n.SetAccel(10.0f);
    //    }

    //    if (_secondTrigger)
    //        _secondTrigger.enabled = false;

    //    isAcceptToNell = true;

    //    if (_nellTransformation)
    //        _nellTransformation.StartCutScene();

    //    Destroy(_nellFirstDialogueRepeat);
    //}

    //private int _rejectCount = 0;
    //[YarnCommand("RejectToNell")]
    //public void RejectToNell()
    //{
    //    //_rejectCount++;
    //    //if (_rejectCount >= 2)
    //    //{
    //    //    SavableNode node = new SavableNode();
    //    //    node.key = "RejectToNell";
    //    //    node.value = 100;

    //    //    SavableDataManager.instance.AddSavableObject(node);

    //    //    Destroy(_fieldDoor);
    //    //}
    //}

    [Header("자동 저장")]
    [SerializeField]
    private SavePointController _save;
    public void StoryEnd()
    {
        _save.Save();
    }

    //public void BossBattleStart()
    //{

    //}

    //public void BossBattleEnd()
    //{

    //}
}
