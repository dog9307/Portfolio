using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayerAttack : MonoBehaviour
{
    [SerializeField]
    private bool _isStartWithStart = false;

    void Start()
    {
        if (_isStartWithStart)
        {
            GetComponent<FadingGuideUI>().StartFading(1.0f);
            GetComponent<TutorialFadingKeyTrigger>().isEnable = true;
        }
    }

    public void Attack()
    {
        PlayerAttacker player = FindObjectOfType<PlayerAttacker>();
        if (player)
            player.AttackStart();
    }
}
