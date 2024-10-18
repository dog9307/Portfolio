using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 주석

public class StartSceneFader : MonoBehaviour
{
    [SerializeField]
    private float _fadeTime = 0.5f;

    [SerializeField]
    private Image _img;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        KeyManager.instance.isEnable = true;

        StartCoroutine(Fading());

        //FindObjectOfType<BGMPlayer>().PlaySound();

        Camera.main.transform.position = new Vector3(0.0f, 0.0f, -10.0f);
    }

    IEnumerator Fading()
    {
        float currentTime = 0.0f;
        float ratio = 1.0f;
        while (currentTime < _fadeTime)
        {
            ratio = currentTime / _fadeTime;
            ApplyAlpha(1.0f - ratio);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(0.0f);
    }

    void ApplyAlpha(float ratio)
    {
        Color color = _img.color;
        color.a = ratio;
        _img.color = color;
    }
}
