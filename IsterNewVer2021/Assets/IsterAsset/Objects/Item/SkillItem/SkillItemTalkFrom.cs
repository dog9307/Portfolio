using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItemTalkFrom : TalkFrom
{
    [SerializeField]
    private int _relativeSkillId = 100;
    public int relativeSkillId { get { return _relativeSkillId; } }

    [SerializeField]
    private float _previewScale = 1.0f;
    public float previewScale { get { return _previewScale; } }

    [SerializeField]
    private DisposableObject _disposable;

    [SerializeField]
    private GameObject _tuto;

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        bool isPassive = (_relativeSkillId / 100 == 2);
        if (!isPassive)
        {
            ActiveSkillSlotList skillList = FindObjectOfType<ActiveSkillSlotList>();
            if (skillList)
                skillList.SkillSlotOn(_relativeSkillId);
        }
        else
        {
            PassiveSkillSlotList skillList = FindObjectOfType<PassiveSkillSlotList>();
            if (skillList)
                skillList.SkillSlotOn(_relativeSkillId);
        }

        //if (TowerItemRewardManager.instance)
        //{
        //    if (TowerItemRewardManager.instance.reward == gameObject)
        //        TowerItemRewardManager.instance.reward = null;
        //}

        Destroy(gameObject);

        if (_sfx)
            _sfx.PlaySFX(_sfxName);

        SavableObject save = GetComponent<SavableObject>();
        if (save)
            save.AddSaveData();

        if (_disposable)
            _disposable.UseObject();

        //if (_relativeSkillId == (int)ACTIVE.PARRYING)
        //    PlayerPrefs.SetInt("revenge", 100);

        if (_tuto)
            _tuto.SetActive(true);
    }

    [SerializeField]
    private GameObject _darkLight;
    public void CreateDarkLight()
    {
        GameObject newDark = Instantiate(_darkLight);
        newDark.transform.position = transform.position;

        DarkLightTalkFrom talkFrom = newDark.GetComponent<DarkLightTalkFrom>();
        if (talkFrom)
        {
            talkFrom.figure = 100;
            talkFrom.isAutoGain = true;
        }
    }
}
