using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ChapterFlowNpcMoveInfo
{
    public NPCController npc;
    public int targetRoomNumber;
}

public class ChapterFlowNode : MonoBehaviour
{
    [SerializeField]
    private string _relativeSavableKey = "";

    [SerializeField]
    private ChapterFlowNpcMoveInfo[] _flowInfoes;

    public void ApplyNPCMove()
    {
        if (_flowInfoes == null) return;

        int count = SavableDataManager.instance.FindIntSavableData(_relativeSavableKey);

        if (count >= 100)
        {
            foreach (var i in _flowInfoes)
                i.npc.NPCMove(i.targetRoomNumber);
        }
    }
}
