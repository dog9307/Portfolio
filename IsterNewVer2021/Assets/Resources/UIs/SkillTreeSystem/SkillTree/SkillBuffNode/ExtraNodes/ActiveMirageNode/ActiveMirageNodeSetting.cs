using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMirageNodeSetting : MonoBehaviour
{
    [SerializeField]
    private ActiveMirageNode[] _nodes;

    // Start is called before the first frame update
    void OnEnable()
    {
        PlayerSkillUsage playerSkillUsage = FindObjectOfType<PlayerSkillUsage>();
        if (!playerSkillUsage) return;

        //HAND relativeHand = playerSkillUsage.GetSkillHand<ActiveMirage>();
        //if (relativeHand == HAND.NONE) return;

        List<SkillBase> passiveList = playerSkillUsage.passiveSkills;
        for (int i = 0; i < _nodes.Length; ++i)
        {
            if (i >= passiveList.Count)
            {
                _nodes[i].gameObject.SetActive(false);
                continue;
            }

            int id = NodeSetting(i, passiveList[i]);

            if (id == -1)
            {
                _nodes[i].gameObject.SetActive(false);
                continue;
            }

            RectTransform rect = _nodes[i].GetComponent<RectTransform>();
            Vector3 pos = Vector3.zero;
            if (i <= 1)
                pos.y = 100.0f;
            else
                pos.y = -100.0F;

            if (i % 2 == 0)
            {
                float x = -100.0f;
                if (passiveList.Count % 2 == 1)
                {
                    if (passiveList.Count == 1 && i == 0)
                        x = 0.0f;
                    else if (passiveList.Count == 3)
                    {
                        if (i == 0)
                            x = -100.0f;
                        else if (i == 2)
                            x = 0.0f;
                    }
                }
                pos.x = x;
            }
            else
                pos.x = 100.0f;

            rect.anchoredPosition = pos;
        }
    }

    int NodeSetting(int index, SkillBase skill)
    {
        int id = -1;

             if (skill.id == 200) id = 0;   // PassiveOtherAttack
        else if (skill.id == 201) id = 1;   // TracingArrow
        //else if (skill.id == 202) id = 2; // PassiveMirage
        else if (skill.id == 203) id = 3;   // AdditionalAttack
        //else if (skill.id == 204) id = 4; // PassiveReversePower
        else if (skill.id == 205) id = 5;   // PassiveCounterAttack
        else if (skill.id == 206) id = 6;   // PassiveMarker
        else if (skill.id == 207) id = 7;   // PassiveCoolTimeDown
        else if (skill.id == 208) id = 8;   // PassiveRemover
        else if (skill.id == 209) id = 9;   // PassiveKnockbackIncrease
        else if (skill.id == 210) id = 10;  // PassiveFortune

        _nodes[index].buffId = id;

        return id;
    }
}
