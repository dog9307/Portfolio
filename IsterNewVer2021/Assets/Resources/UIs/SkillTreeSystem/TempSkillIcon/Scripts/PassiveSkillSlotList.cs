using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillSlotList : MonoBehaviour
{
    [SerializeField]
    private PassiveSkillSelector[] _slots;

    [SerializeField]
    private TutorialPopUp _secondEquipTuto;

    [SerializeField]
    private PassiveSkillEquipedSkillIconManager _passiveEquipedIconManager;

    void Start()
    {
        int count = PlayerPrefs.GetInt("swordCount", -1);
        if (count >= 100)
            SkillSlotOn(0);

        for (int i = (int)ACTIVE.NONE + 1; i < (int)ACTIVE.END; ++i)
        {
            int id = 200 + i;
            string key = "passive_" + id.ToString();

            count = PlayerPrefs.GetInt(key, 0);

            if (count >= 100)
                SkillSlotOn(id);
        }

        int tutoCount = PlayerPrefs.GetInt("SecondEquipTutoEnd", 0);
        if (tutoCount < 100)
        {
            StartCoroutine(WaitForSecondEquip());
        }
        else
        {
            if (_secondEquipTuto)
                Destroy(_secondEquipTuto.gameObject);
        }
    }

    IEnumerator WaitForSecondEquip()
    {
        int count = 0;
        while (count < 2)
        {
            yield return null;

            count = 0;
            foreach (var slot in _slots)
            {
                if (slot.gameObject.activeSelf)
                    count++;
            }
        }

        if (_secondEquipTuto)
            _secondEquipTuto.StartPopUp();

        PlayerPrefs.SetInt("SecondEquipTutoEnd", 100);
    }

    public void SkillSlotOn(int id)
    {
        foreach (var slot in _slots)
        {
            if (slot.skillId == id)
            {
                Vector3 scale = slot.transform.localScale;

                Transform prevParent = slot.transform.parent;
                slot.transform.parent = null;
                slot.transform.parent = prevParent;
                slot.transform.localScale = scale;

                slot.gameObject.SetActive(true);

                PlayerPassiveSkillStorage storage = FindObjectOfType<PlayerPassiveSkillStorage>();
                if (storage)
                {
                    if (storage.equipedCount < 4)
                    {
                        if (storage.IsExist(slot.skillId)) return;

                        int targetIndex = -1;
                        for (int i = 0; i < storage.passiveSkills.Count; ++i)
                        {
                            if (storage.passiveSkills[i] == -1)
                            {
                                targetIndex = i;
                                break;
                            }
                        }
                        if (targetIndex != -1)
                        {
                            storage.AddPassiveInList(targetIndex, slot.skillId);

                            if (_passiveEquipedIconManager)
                                _passiveEquipedIconManager.ChangeIcons();
                        }
                    }
                }
            }
        }
    }
}
