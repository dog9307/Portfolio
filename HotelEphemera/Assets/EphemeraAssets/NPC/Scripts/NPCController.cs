using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class NPCController : MonoBehaviour
{
    [SerializeField]
    private string _npcName = "";
    public string npcName { get { return _npcName; } }
    [SerializeField]
    private Sprite _npcSprite;
    public Sprite npcSprite { get { return _npcSprite; } }

    public int currentRoomNumber { get; set; }

    [System.Serializable]
    public struct NPCDialogueInfo
    {
        public int roomNumber;
        public string buttonName;
        public string dialogueName;
        public InteractionConditionBase condition;
        public string savableKey;
        public bool isConditionRequired;
        public bool isShowAnyway;
    }

    [SerializeField]
    private NPCDialogueInfo[] _selectableKeys;

    void Start()
    {
        if (_minimapNPC)
        {
            MinimapNPCClose();
            MinimapNPCOpen();
        }
    }

    public void RoomSelectableSetting(bool isDialogueAllShow)
    {
        if (_selectableKeys == null)
            return;

        for (int i = 0; i < RoomRenderer.instance.choiceButtons.Length; ++i)
        {
            Selectable currentButton = RoomRenderer.instance.choiceButtons[i];

            currentButton.gameObject.SetActive(false);

            if (i >= _selectableKeys.Length)
                continue;
            else
            {
                if (RoomRenderer.instance.roomNumber != _selectableKeys[i].roomNumber) continue;

                if (_selectableKeys[i].isConditionRequired)
                {
                    if (!_selectableKeys[i].condition)
                        currentButton.gameObject.SetActive(false);
                    else
                    {
                        if (_selectableKeys[i].condition.IsCanInteraction())
                        {
                            currentButton.gameObject.SetActive(true);

                            RoomSelectableButtonBinder binder = currentButton.GetComponent<RoomSelectableButtonBinder>();
                            binder.Bind(_selectableKeys[i].buttonName, _selectableKeys[i].dialogueName, _selectableKeys[i].condition, _selectableKeys[i].savableKey);
                        }
                        else
                        {
                            if (_selectableKeys[i].isShowAnyway)
                            {
                                currentButton.gameObject.SetActive(true);

                                RoomSelectableButtonBinder binder = currentButton.GetComponent<RoomSelectableButtonBinder>();
                                binder.Bind(_selectableKeys[i].buttonName, _selectableKeys[i].dialogueName, _selectableKeys[i].condition, _selectableKeys[i].savableKey);
                            }
                        }
                    }
                }
                else
                {
                    currentButton.gameObject.SetActive(true);

                    RoomSelectableButtonBinder binder = currentButton.GetComponent<RoomSelectableButtonBinder>();
                    binder.Bind(_selectableKeys[i].buttonName, _selectableKeys[i].dialogueName, _selectableKeys[i].condition, _selectableKeys[i].savableKey);
                }

                if (currentButton.gameObject.activeSelf)
                {
                    int count = SavableDataManager.instance.FindIntSavableData(_selectableKeys[i].savableKey);
                    if (count >= 100)
                    {
                        if (isDialogueAllShow)
                        {
                            Transform prevParent = currentButton.transform.parent;

                            currentButton.transform.parent = null;
                            currentButton.transform.parent = prevParent;
                        }
                        else
                            currentButton.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    [YarnCommand("NPCMove")]
    public void NPCMove(int roomNumber)
    {
        RoomManager.instance.RoomNPCBind(roomNumber, this);
    }

    [Header("¹Ì´Ï¸Ê NPC °ü·Ã")]
    [SerializeField]
    private GameObject _minimapNPC;
    public GameObject minimapNPC { get { return _minimapNPC; } }
    [SerializeField]
    private string _relativeKey;

    [YarnCommand("MinimapNPCOpen")]
    public void MinimapNPCOpen()
    {
        if (!_minimapNPC) return;

        int count = SavableDataManager.instance.FindIntSavableData(_relativeKey);
        if (count >= 100)
            _minimapNPC.SetActive(true);

        if (npcName == "Daisey")
        {
            count = SavableDataManager.instance.FindIntSavableData("Miro_3_WelcomeIan");
            if (count >= 100)
                _minimapNPC.SetActive(false);
        }
    }

    [YarnCommand("MinimapNPCClose")]
    public void MinimapNPCClose()
    {
        _minimapNPC?.SetActive(false);
    }
}
