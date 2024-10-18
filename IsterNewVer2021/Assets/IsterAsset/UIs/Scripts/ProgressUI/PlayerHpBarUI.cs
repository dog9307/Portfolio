using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBarUI : ProgressBase
{
    private Damagable _playerHP;
   // PlayerSkillUsage _skill;

    public override void Init()
    {
        //_slider = GetComponent<Slider>();
        //_slider.targetGraphic = _firstImage;

        _playerHP = FindObjectOfType<PlayerMoveController>().GetComponent<Damagable>();
       // _skill.skills[index].skillIcon
    }
    public override void UpdateGauge()
    {
        _MaxGuage = _playerHP.totalHP;
        _CurretGuage = _playerHP.currentHP;
    }
}
