using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSkipper : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private Image _black;

    [SerializeField]
    private float _skipTime = 0.2f;
    private static bool _isSkipStart = false;

    [SerializeField]
    private string _nextScene;
    [SerializeField]
    private string[] _additiveScenes;

    // Update is called once per frame
    void Update()
    {
        if (_isSkipStart) return;

        if (KeyManager.instance.IsOnceKeyDown("scene_skip", true))
            SkipStart();
    }

    public void SkipStart()
    {
        StartCoroutine(Fading());
    }

    IEnumerator Fading()
    {
        _isSkipStart = true;

        float currentTime = 0.0f;
        while (currentTime < _skipTime)
        {
            float ratio = currentTime / _skipTime;

            Color color = _black.color;
            color.a = ratio;
            _black.color = color;

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }

        _isSkipStart = false;

        if (_nextScene.Length != 0)
        {
            SceneManager.LoadScene(_nextScene);

            foreach (var sceneName in _additiveScenes)
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

            IsterTimeManager.globalTimeScale = 1.0f;
            SceneManager.sceneLoaded += LoadSceneEnd;
        }
    }

    private void LoadSceneEnd(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (!_black) return;

        Color color = _black.color;
        color.a = 0.0f;
        _black.color = color;
    }
}
