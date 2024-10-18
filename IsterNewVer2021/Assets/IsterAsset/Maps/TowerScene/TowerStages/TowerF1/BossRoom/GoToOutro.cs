using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToOutro : MonoBehaviour
{
    [SerializeField]
    private Image _black;

    void Start()
    {
        if (_black)
            _black.gameObject.SetActive(false);
    }

    public void StartFading(float totalTime)
    {
        StageBGMPlayer bgm = FindObjectOfType<StageBGMPlayer>();
        if (bgm)
            bgm.StopBGM();

        StartCoroutine(Fading(totalTime));
    }

    IEnumerator Fading(float totalTime)
    {
        _black.gameObject.SetActive(true);

        float currentTime = 0.0f;
        Color color;
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;
            color = _black.color;
            color.a = ratio;
            _black.color = color;

            yield return null;

            currentTime += Time.deltaTime;
        }
        color = _black.color;
        color.a = 1.0f;
        _black.color = color;

        GoOutro();
    }

    public void GoOutro()
    {
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
        if (inventory)
            SavableDataManager.instance.AddSavableObject(inventory);

        SavableDataManager.instance.SaveList();

        SceneManager.LoadScene("OutroScene", LoadSceneMode.Single);
    }
}
