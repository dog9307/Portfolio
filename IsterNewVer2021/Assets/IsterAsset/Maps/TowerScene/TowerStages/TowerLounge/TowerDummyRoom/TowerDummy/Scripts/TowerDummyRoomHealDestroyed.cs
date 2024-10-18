using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDummyRoomHealDestroyed : MonoBehaviour
{
    [SerializeField]
    private GameObject _healItem;
    private bool _isPrevDisappear;

    [SerializeField]
    private GameObject _skills;
    [SerializeField]
    private float _delayTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        _isPrevDisappear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPrevDisappear) return;

        if (!_healItem)
        {
            Invoke("OpenSkills", _delayTime);
            _isPrevDisappear = true;
        }
    }

    void OpenSkills()
    {
        _skills.SetActive(true);
    }
}
