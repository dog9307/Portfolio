using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = transform.GetComponent<Button>();

        BGMPlayer bgm = GetComponentInChildren<BGMPlayer>();
        if (bgm)
            bgm.PlaySound();
    }
    
    public void StartGame()
    {
        //SceneLoader.instance.AddScene("PlayerMoveScene");
        //SceneLoader.instance.AddScene("IngameUiScene");
        //SceneLoader.instance.AddScene("TownScene");
        //SceneLoader.instance.LoadScene();

        SceneLoader.instance.AddScene("PlayerMoveScene");
        SceneLoader.instance.AddScene("IngameUiScene");
        SceneLoader.instance.LoadScene();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
