using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ChapterFlowController : MonoBehaviour
{
    #region SINGLETON
    static private ChapterFlowController _instance;
    static public ChapterFlowController instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<ChapterFlowController>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "ChapterFlowController";
                _instance = container.AddComponent<ChapterFlowController>();
            }
        }
        else
            Destroy(gameObject);
    }
    #endregion

    [SerializeField]
    private bool _isAutoRoomOpen = false;
    [SerializeField]
    private RoomDatas _defaultRoom;

    [SerializeField]
    private int _targetChapter = 0;
    public int targetChapter { get { return _targetChapter; } }

    [SerializeField]
    private NPCController[] _npcs;

    [SerializeField]
    private NPCController _defaultNPC;
    public NPCController defaultNPC { get { return _defaultNPC; } }
    [SerializeField]
    private NPCController _partnerNPC;
    public NPCController partnerNPC { get { return _partnerNPC; } }

    [SerializeField]
    private ChapterFlowNode[] _flowNodes;

    void Start()
    {
        int count = SavableDataManager.instance.FindIntSavableData("FirstRun");
        if (count < 100)
        {
            if (_isAutoRoomOpen && _defaultRoom)
            {
                StartCoroutine(FirstRunDelay());

                _defaultRoom.OpenRoom();
                SavableNode node = new SavableNode();
                node.key = "FirstRun";
                node.value = 100;
                SavableDataManager.instance.AddSavableObject(node);
                //SavableDataManager.instance.SaveList();
            }
        }

        if (_flowNodes != null)
        {
            foreach (var fn in _flowNodes)
                fn.ApplyNPCMove();
        }

        count = SavableDataManager.instance.FindIntSavableData("Miro_3_WelcomeIan");
        if (count >= 100)
        {
            count = SavableDataManager.instance.FindIntSavableData("GoodbyeIan");
            if (count < 100)
                RegistPartner("Ian");
        }
    }

    IEnumerator FirstRunDelay()
    {
        for (int i = 0; i < Application.targetFrameRate * 2; ++i)
            yield return new WaitForEndOfFrame();

        ScreenMaskController.instance.isPause = false;
    }

    [YarnCommand("RegistPartner")]
    public void RegistPartner(string name)
    {
        _partnerNPC = null;
        foreach (var n in _npcs)
        {
            if (n.npcName == name)
            {
                _partnerNPC = n;
                _partnerNPC.MinimapNPCOpen();
                return;
            }
        }
    }
}
