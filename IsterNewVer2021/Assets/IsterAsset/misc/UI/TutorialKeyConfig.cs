using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialKeyConfig : MonoBehaviour
{
    float _uptimer;
    float _downtimer;
    private float _fadeTime;
    private bool _isUp;
    
    private bool isAlreadyFading = false;
    public Text _text;
    // Start is called before the first frame update
    void Start()
    {
        int playerDie = PlayerPrefs.GetInt("isPlayerDie", 0);
        if (playerDie != 0)
            Destroy(gameObject);

        FindObjectOfType<PlayerMoveController>().PlayerMoveFreeze(true);
        isAlreadyFading = false;

        _fadeTime = 1.0f;
        _uptimer = 0.0f;
        _downtimer = 0.0f;
        _isUp = true;
    }

    void Update()
    {
        if (isAlreadyFading) return;

        if (Input.anyKeyDown)
        {
            isAlreadyFading = true;
            StartCoroutine(Fading());
        }

        if (_isUp)
        {
            _uptimer += IsterTimeManager.deltaTime;
            _text.color = new Color(1, 1, 1, 1.0f - (_uptimer / _fadeTime));
            if(_uptimer > 1.0f)
            {
                _isUp = false;
                _uptimer = 0.0f;
            }
        }
        else
        {
            _downtimer += IsterTimeManager.deltaTime;
            _text.color = new Color(1, 1, 1, 0.0f + (_downtimer / _fadeTime));
            if (_downtimer > 1.0f)
            {
                _isUp = true;
                _downtimer = 0.0f;
            }
        }    
            //SetColor("_Color", new Color(color.r, color.g, color.b, 1.0f) * intensity);
    }

    IEnumerator Fading()
    {
        Graphic[] graphics = GetComponentsInChildren<Graphic>();

        float currentTime = 0.0f;
        float totalTime = 0.5f;
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;

            foreach (var g in graphics)
                ApplyAlpha(g, 1.0f - ratio);

            yield return null;

            currentTime += Time.deltaTime;
        }

        FindObjectOfType<PlayerMoveController>().PlayerMoveFreeze(false);
        Destroy(gameObject);
    }

    void ApplyAlpha(Graphic g, float alpha)
    {
        Color color = g.color;
        color.a = (color.a < alpha ? color.a : alpha);
        g.color = color;
    }
}
