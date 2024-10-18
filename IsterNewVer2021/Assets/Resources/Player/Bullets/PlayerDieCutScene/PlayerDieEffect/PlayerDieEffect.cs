using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerDieEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject _effect;

    public CinemachineVirtualCamera currentRoomCamera { get; set; }

    private PlayerMoveController _player;
    public PlayerMoveController player
    {
        get
        {
            if (!_player)
                _player = FindObjectOfType<PlayerMoveController>();

            return _player;
        }
    }

    private void OnEnable()
    {
        _effect.SetActive(true);
        
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("disappearStart");

        if (currentRoomCamera)
            currentRoomCamera.Priority = 0;
    }

    public void PlayerDisappear()
    {
        transform.parent = null;
        player.Move(new Vector2(10000.0f, 10000.0f));
    }

    public void PlayerAppearStart()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("appearStart");
    }

    public void PlayerAppear()
    {
        player.Move(Vector2.zero);

        Animator anim = player.GetComponent<Animator>();
        anim.SetTrigger("dieEnd");

        LookAtMouse look = player.GetComponent<LookAtMouse>();
        look.dir = Vector2.up;
    }

    public void EffectDisappear()
    {
        _effect.SetActive(false);
    }

    public void SceneReload()
    {
        EnemyBase[] enemys = FindObjectsOfType<EnemyBase>();
        foreach (var enemy in enemys)
            Destroy(enemy.gameObject);

        SceneManager.UnloadSceneAsync("MapScene");
        SceneManager.LoadSceneAsync("MapScene", LoadSceneMode.Additive);

        transform.position = Vector2.zero;
        GetComponentInChildren<CinemachineVirtualCamera>().transform.localPosition = Vector3.zero;

        Vector3 camNewPos = Camera.main.transform.position;
        camNewPos.x = 0.0f;
        camNewPos.y = 0.0f;
        Camera.main.transform.position = camNewPos;
    }
}
