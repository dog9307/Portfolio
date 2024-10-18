using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaMiddleTutoManager : MonoBehaviour
{
    [SerializeField]
    private Damagable _dongley;
    [SerializeField]
    private FadingGuideUI _battleText;

    [SerializeField]
    private GameObject _items;
    [SerializeField]
    private Transform _passiveItemTutorial;
    [SerializeField]
    private FadingGuideUI _fade;

    [SerializeField]
    private FadingGuideUI _passiveTutoFading;
    [SerializeField]
    private FadingGuideUI _attackGuide;

    [SerializeField]
    private FadingGuideUI _activeTutoFading;
    [SerializeField]
    private FadingGuideUI _skillChangeGuide;

    private bool _isAlreadyBattle;

    public void BattleStart()
    {
        if (_isAlreadyBattle) return;

        _isAlreadyBattle = true;
        _battleText.StartFading(1.0f);
        StartCoroutine(DongleyDieCheck());
    }

    IEnumerator DongleyDieCheck()
    {
        while (!_dongley.isDie)
            yield return null;

        yield return new WaitForSeconds(1.0f);

        _battleText.StartFading(0.0f);
        PassiveItemAppear();
    }

    public void PassiveItemAppear()
    {
        _items.SetActive(true);

        GameObject item = _items;
        if (item)
        {
            item.transform.parent = transform;
            item.transform.localPosition = Vector3.zero;
            item.SetActive(true);

            if (_passiveItemTutorial)
            {
                _passiveItemTutorial.gameObject.SetActive(true);
                _passiveItemTutorial.parent = item.transform.GetChild(0).GetChild(0);

                if (_fade)
                    _fade.StartFading(1.0f, true);
            }
        }
        //if (_fade)
        //{
        //    _fade.StartFading(1.0f);
        //    _fade.transform.parent = _healItem.transform.GetChild(0).GetChild(0);
        //}
    }

    public void PassiveTutoStart()
    {
        if (gameObject.activeSelf)
            StartCoroutine(PassiveTuto());
    }

    IEnumerator PassiveTuto()
    {
        PlayerSkillUsage playerSkill = FindObjectOfType<PlayerSkillUsage>();

        // 탭 열 때까지 기다림
        while (!UIManager.instance.tabOn)
            yield return null;

        KeyManager.instance.Disable("tabUI");
        KeyManager.instance.Disable("menu");
        _passiveTutoFading.StartFading(1.0f);

        while (playerSkill.passiveSkills.Count < 1)
            yield return null;

        _passiveTutoFading.StartFading(0.0f);
        KeyManager.instance.Enable("tabUI");
        KeyManager.instance.Enable("menu");

        // 탭 다시 닫을 때까지 기다림
        while (UIManager.instance.tabOn)
            yield return null;

        _attackGuide.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        _attackGuide.StartFading(1.0f, true);
        _attackGuide.GetComponent<TutorialPlayerFreezing>().Freezing();
        _attackGuide.GetComponent<FollowingPlayerUI>().FollowStart();
        _attackGuide.GetComponent<TutorialFadingKeyTrigger>().isEnable = true;
    }

    public void ActiveTutoStart()
    {
        StartCoroutine(ActiveTuto());
    }

    IEnumerator ActiveTuto()
    {
        PlayerSkillUsage playerSkill = FindObjectOfType<PlayerSkillUsage>();

        // 탭 열 때까지 기다림
        while (!UIManager.instance.tabOn)
            yield return null;

        KeyManager.instance.Disable("tabUI");
        _activeTutoFading.StartFading(1.0f);

        bool isEnd = false;
        while (!isEnd)
        {
            yield return null;

            int count = 0;
            foreach (var skill in playerSkill.activeSkills)
            {
                if (skill != null)
                    count++;
            }

            if (count > 1)
                isEnd = true;
        }

        _activeTutoFading.StartFading(0.0f);
        KeyManager.instance.Enable("tabUI");

        // 탭 다시 닫을 때까지 기다림
        while (UIManager.instance.tabOn)
            yield return null;

        _skillChangeGuide.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        _skillChangeGuide.StartFading(1.0f, true);
        _skillChangeGuide.GetComponent<TutorialPlayerFreezing>().Freezing();
        _skillChangeGuide.GetComponent<FollowingPlayerUI>().FollowStart();
        _skillChangeGuide.GetComponent<TutorialFadingKeyTrigger>().isEnable = true;
    }
}
