using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SavePointController : MonoBehaviour
{
    [SerializeField]
    private MapStartPos _startPos;

    public UnityEvent OnSave;

    [SerializeField]
    private float _coolTime = 1.0f;
    private bool _isCanSave = true;

    [SerializeField]
    private Animator _anim;

    private void OnEnable()
    {
        _isCanSave = true;
    }

    public void Save()
    {
        if (!_isCanSave) return;

        ActiveItemRerollUser healUser = FindObjectOfType<ActiveItemRerollUser>();
        if (healUser)
            healUser.ChargeAll();

        if (SavableDataManager.instance)
        {
            PlayerSkillUsage playerSkill = FindObjectOfType<PlayerSkillUsage>();
            if (playerSkill)
                SavableDataManager.instance.AddSavableObject(playerSkill);

            if (_startPos)
            {
                SavableNode node = new SavableNode();

                node.key = "PlayerNextStartPos";
                node.value = _startPos.id;

                SavableDataManager.instance.AddSavableObject(node);
            }

            Damagable damagable = playerSkill.GetComponent<Damagable>();
            if (damagable)
            {
                SavableNode node = new SavableNode();

                node.key = "PlayerTotalHP";
                node.value = damagable.totalHP;

                SavableDataManager.instance.AddSavableObject(node);
            }

            PlayerMapChanger mapChanger = FindObjectOfType<PlayerMapChanger>();
            if (mapChanger)
                SavableDataManager.instance.AddSavableObject(mapChanger);

            PlayerAreaFinder area = FindObjectOfType<PlayerAreaFinder>();
            if (area)
                SavableDataManager.instance.AddSavableObject(area);

            FieldManager field = FindObjectOfType<FieldManager>();
            if (field)
                SavableDataManager.instance.AddSavableObject(field);

            RelicIconList relicIcon = FindObjectOfType<RelicIconList>();
            if (relicIcon)
                SavableDataManager.instance.AddSavableObject(relicIcon);

            PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
            if (inventory)
                SavableDataManager.instance.AddSavableObject(inventory);

            PlayerPassiveSkillStorage passiveStorage = FindObjectOfType<PlayerPassiveSkillStorage>();
            if (passiveStorage)
                SavableDataManager.instance.AddSavableObject(passiveStorage);

            OnSave?.Invoke();

            SavableDataManager.instance.SaveList();

            StartCoroutine(CoolTime());
        }
    }

    IEnumerator CoolTime()
    {
        if (_anim)
            _anim.SetTrigger("active");

        _isCanSave = false;
        yield return new WaitForSeconds(_coolTime);
        _isCanSave = true;
    }
}
