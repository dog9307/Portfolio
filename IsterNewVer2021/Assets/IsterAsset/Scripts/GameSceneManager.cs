using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public int currentRegion { get; set; }
    
    void Awake()
    {
        currentRegion = 0;
    }

    void Start()
    {
        //GameObject start = GameObject.Find("StartPoint");
        //GameObject player = FindObjectOfType<PlayerMoveController>().gameObject;

        //player.transform.position = start.transform.position;
    }
}
