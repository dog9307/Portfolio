using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDummyGiveMeItem : MonoBehaviour
{
    [SerializeField]
    private bool _isPassive = false;

    [SerializeField]
    private Transform _tutorial;
    [SerializeField]
    private FadingGuideUI _fade;

    // Start is called before the first frame update
    void Start()
    {
        GameObject item = TowerItemRewardManager.instance.GetRandomSkillItem(_isPassive);
        if (item)
        {
            item.transform.parent = transform;
            item.transform.localPosition = Vector3.zero;
            item.SetActive(true);

            if (_tutorial)
            {
                _tutorial.gameObject.SetActive(true);
                _tutorial.parent = item.transform.GetChild(0);

                if (_fade)
                    _fade.StartFading(1.0f);
            }
        }
    }
}
