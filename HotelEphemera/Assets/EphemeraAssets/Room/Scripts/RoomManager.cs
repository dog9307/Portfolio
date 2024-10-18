using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class RoomManager : MonoBehaviour
{
    #region SINGLETON
    static private RoomManager _instance;
    static public RoomManager instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<RoomManager>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "RoomManager";
                _instance = container.AddComponent<RoomManager>();
            }
        }
        else
            Destroy(gameObject);
    }
    #endregion

    [SerializeField]
    private Selectable[] _roomButtons;

    public void ButtonsFreeze()
    {
        foreach (var s in _roomButtons)
            s.interactable = false;
    }

    public void ButtonsUnfreeze()
    {
        foreach (var s in _roomButtons)
            s.interactable = true;
    }

    [SerializeField]
    private RoomInfo[] _rooms;
    [YarnCommand("ActivateRoom")]
    public void ActivateRoom(int roomNumber)
    {
        foreach (var r in _rooms)
        {
            if (r._roomNum == roomNumber)
            {
                r._isRoomActivate = true;

                SavableNode node = new SavableNode();
                node.key = $"Room_{roomNumber}_Opened";
                node.value = 100;
                SavableDataManager.instance.AddSavableObject(node);

                return;
            }
        }
    }

    public void RoomNPCBind(int targetRoom, NPCController npc)
    {
        foreach (var r in _rooms)
        {
            if (r._roomNum == targetRoom)
            {
                if (npc.transform.parent)
                    npc.GetComponentInParent<RoomDatas>().currentNPC = null;

                RoomDatas data = r.GetComponent<RoomDatas>();
                data.currentNPC = npc;
                npc.transform.parent = r.transform;

                if (npc.minimapNPC)
                {
                    Transform npcPos = npc.minimapNPC.transform;

                    npcPos.parent = data.minimapNPCPos;
                    npcPos.localPosition = Vector3.zero;
                }

                return;
            }
        }
    }
}
