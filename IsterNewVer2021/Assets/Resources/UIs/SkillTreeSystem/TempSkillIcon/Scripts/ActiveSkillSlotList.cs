using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkillSlotList : MonoBehaviour
{
    [SerializeField]
    private ActiveSkillIcon[] _slots;

    [SerializeField]
    private TutorialPopUp _firstActiveTutoPopUp;
    [SerializeField]
    private TutorialPopUp _secondActiveTutoPopUp;

    void Start()
    {
        for (int i = (int)ACTIVE.NONE + 1; i < (int)ACTIVE.END; ++i)
        {
            int id = 100 + i;
            string key = "active_" + id.ToString();

            int count = PlayerPrefs.GetInt(key, 0);

            if (count >= 100)
                SkillSlotOn(id);
        }

        int tutoCount = PlayerPrefs.GetInt("FirstActiveTutoEnd", 0);
        if (tutoCount < 100)
        {
            StartCoroutine(WaitForFirstActive());
        }
        else
        {
            if (_firstActiveTutoPopUp)
                Destroy(_firstActiveTutoPopUp.gameObject);
        }

            tutoCount = PlayerPrefs.GetInt("SecondActiveTutoEnd", 0);
        if (tutoCount < 100)
        {
            StartCoroutine(WaitForSecondActive());
        }
        else
        {
            if (_secondActiveTutoPopUp)
                Destroy(_secondActiveTutoPopUp.gameObject);
        }
    }

    IEnumerator WaitForFirstActive()
    {
        PlayerSkillUsage playerSkill = FindObjectOfType<PlayerSkillUsage>();
        int count = 0;
        while (count < 1)
        {
            yield return null;

            count = playerSkill.activeSkills.Count;
        }

        PlayerAttacker attacker = FindObjectOfType<PlayerAttacker>();
        if (attacker)
        {
            while (attacker.isBattle)
                yield return null;
        }

        if (_firstActiveTutoPopUp)
            _firstActiveTutoPopUp.StartPopUp();

        PlayerPrefs.SetInt("FirstActiveTutoEnd", 100);
    }

    IEnumerator WaitForSecondActive()
    {
        PlayerSkillUsage playerSkill = FindObjectOfType<PlayerSkillUsage>();
        int count = 0;
        while (count < 2)
        {
            yield return null;

            count = playerSkill.activeSkills.Count;
        }

        PlayerAttacker attacker = FindObjectOfType<PlayerAttacker>();
        if (attacker)
        {
            while (attacker.isBattle)
                yield return null;
        }
        
        if (_secondActiveTutoPopUp)
            _secondActiveTutoPopUp.StartPopUp();

        PlayerPrefs.SetInt("SecondActiveTutoEnd", 100);
    }

    public void SkillSlotOn(int id)
    {
        PlayerSkillUsage playerSkill = FindObjectOfType<PlayerSkillUsage>();
        foreach (var slot in _slots)
        {
            if (slot.id == id)
            {
                Transform prevParent = slot.transform.parent;
                slot.transform.parent = null;
                slot.transform.parent = prevParent;
                slot.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                slot.gameObject.SetActive(true);

                playerSkill.AddSkill(SkillFactory.CreateSkill(id));
            }
        }
    }
}
