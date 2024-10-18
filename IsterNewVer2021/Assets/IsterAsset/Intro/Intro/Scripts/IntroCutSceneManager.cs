using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroCutSceneManager : MonoBehaviour
{
    [SerializeField]
    private float _startDelayTime = 0.5f;
    [SerializeField]
    private float _nextScriptDelayTime = 0.5f;

    [SerializeField]
    private Image _godMaker;
    [SerializeField]
    private Image _godMakerGone;
    [SerializeField]
    private Image _towerImage;
    [SerializeField]
    private Image _backImage;
    [SerializeField]
    private Image _noTowerBackImage;
    [SerializeField]
    private Image _blackMask;

    [SerializeField]
    private Image _player;
    [SerializeField]
    private Image _frontChain;
    [SerializeField]
    private Image _backChain;
    [SerializeField]
    private Image _gradient;

    [SerializeField]
    private IntroScriptController[] _scripts;
    private int _currentScript;

    [Header("사운드 앞 딜레이")]
    [SerializeField]
    private StageBGMPlayer _bgm;
    [SerializeField]
    private float _soundDelay = 0.0f;

    [Header("스킵관련")]
    [SerializeField]
    private FadingGuideUI _skip;
    [SerializeField]
    private Image _fill;
    [SerializeField]
    private float _skipPressTime = 1.0f;
    private bool _isAlreadySkip;
    private int _keyCount = 0;

    [SerializeField]
    private SceneSkipper _skipper;

    private IntroScriptController currentScript { get { if (_currentScript < _scripts.Length) return _scripts[_currentScript]; return null; } }

    [SerializeField]
    private VideoPlayer _video;
    private VideoClip _clip;
    private Coroutine _sceneChangeDelay;
    [SerializeField]
    private AudioSource _audio;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        ApplyAlpha(_godMaker, 0.0f);
        ApplyAlpha(_godMakerGone, 0.0f);
        ApplyAlpha(_towerImage, 0.0f);
        ApplyAlpha(_backImage, 0.0f);
        ApplyAlpha(_noTowerBackImage, 0.0f);
        ApplyAlpha(_blackMask, 0.0f);
        ApplyAlpha(_player, 0.0f);
        ApplyAlpha(_frontChain, 0.0f);
        ApplyAlpha(_backChain, 0.0f);
        ApplyAlpha(_gradient, 0.0f);

        _currentScript = 0;

        //StartCoroutine(PlayeSound());
        //StartCoroutine(CutScene());

        _skipper.enabled = false;
        _isAlreadySkip = false;

        _fill.fillAmount = 0.0f;

        _sceneChangeDelay = StartCoroutine(SceneChangeDelay());
    }

    IEnumerator SceneChangeDelay()
    {
        if (!_video)
            yield break;

        _clip = _video.clip;

        if (!_clip)
            yield break;

        float totalTime = 2.5f;

        yield return new WaitForSeconds((float)_clip.length - totalTime);

        float currentTime = 0.0f;
        float volume = 1.0f;
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;
            volume = Mathf.Lerp(1.0f, 0.0f, ratio);

            _audio.volume = volume;

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        _audio.volume = 0.0f;

        SceneManager.LoadScene("StartScene");

        _sceneChangeDelay = null;
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (_keyCount == 0)
        {
            if (Input.anyKeyDown)
            {
                _skip.StartFading(1.0f);
                _keyCount++;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                _fill.fillAmount += Time.deltaTime / _skipPressTime;

                if (_fill.fillAmount >= 1.0f)
                {
                    _fill.fillAmount = 1.0f;
                    if (!_isAlreadySkip)
                    {
                        _isAlreadySkip = true;
                        _skipper.SkipStart();

                        if (_sceneChangeDelay != null)
                        {
                            StopCoroutine(_sceneChangeDelay);
                            _sceneChangeDelay = null;
                        }
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.Escape))
                _fill.fillAmount = 0.0f;
        }
    }

    IEnumerator PlayeSound()
    {
        yield return new WaitForSeconds(_soundDelay);

        _bgm.PlaySound();
    }

    IEnumerator CutScene()
    {
        yield return new WaitForSeconds(_startDelayTime);

        float currentTime = 0.0f;
        float fadeTime = 1.0f;
        float ratio = 0.0f;

        List<Graphic> group = new List<Graphic>();

        // Scene 0 - god maker
        currentTime = 0.0f;
        fadeTime = 1.5f;
        ratio = 0.0f;
        while (currentTime < fadeTime)
        {
            ratio = currentTime / fadeTime;

            ApplyAlpha(_godMaker, ratio);
            //ApplyAlpha(_blackMask, ratio * 0.65f);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_godMaker, 1.0f);
        //ApplyAlpha(_blackMask, 0.65f);

        yield return new WaitForSeconds(_nextScriptDelayTime);

        for (int i = 0; i < 2; ++i)
        {
            if (currentScript)
            {
                currentScript.StartScript();
                while (!currentScript.isScriptEnd)
                    yield return null;
                _currentScript++;
            }
        }

        // Scene 1 - god maker gone
        yield return new WaitForSeconds(1.0f);

        currentTime = 0.0f;
        fadeTime = 1.5f;
        ratio = 0.0f;
        while (currentTime < fadeTime)
        {
            ratio = currentTime / fadeTime;

            ApplyAlpha(_godMakerGone, ratio);
            //ApplyAlpha(_blackMask, ratio * 0.65f);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_godMaker, 0.0f);
        ApplyAlpha(_godMakerGone, 1.0f);
        //ApplyAlpha(_blackMask, 0.65f);

        yield return new WaitForSeconds(_nextScriptDelayTime);

        for (int i = 0; i < 1; ++i)
        {
            if (currentScript)
            {
                currentScript.StartScript();
                while (!currentScript.isScriptEnd)
                    yield return null;
                _currentScript++;
            }
        }

        currentTime = 0.0f;
        fadeTime = 1.5f;
        ratio = 0.0f;
        while (currentTime < fadeTime)
        {
            ratio = currentTime / fadeTime;

            //ApplyAlpha(_godMaker, 1.0f - ratio);
            ApplyAlpha(_godMakerGone, 1.0f - ratio);
            //ApplyAlpha(_blackMask, ratio * 0.65f);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_godMakerGone, 0.0f);
        //ApplyAlpha(_blackMask, 0.65f);

        // Scene 2 - tower
        yield return new WaitForSeconds(1.0f);

        currentTime = 0.0f;
        fadeTime = 1.5f;
        ratio = 0.0f;
        while (currentTime < fadeTime)
        {
            ratio = currentTime / fadeTime;

            ApplyAlpha(_towerImage, ratio);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_towerImage, 1.0f);

        yield return new WaitForSeconds(_nextScriptDelayTime);

        for (int i = 0; i < 1; ++i)
        {
            if (currentScript)
            {
                currentScript.StartScript();
                while (!currentScript.isScriptEnd)
                    yield return null;
                _currentScript++;
            }
        }

        // Scene 3 - opus
        currentTime = 0.0f;
        fadeTime = 1.5f;
        ratio = 0.0f;
        while (currentTime < fadeTime)
        {
            ratio = currentTime / fadeTime;

            ApplyAlpha(_towerImage, 1.0f - ratio);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_towerImage, 0.0f);
        yield return new WaitForSeconds(2.0f);

        group.Add(_player);
        group.Add(_frontChain);
        group.Add(_backChain);
        group.Add(_gradient);

        currentTime = 0.0f;
        fadeTime = 1.5f;
        ratio = 0.0f;
        while (currentTime < fadeTime)
        {
            ratio = currentTime / fadeTime;

            ApplyListAlpha(group, ratio);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyListAlpha(group, 1.0f);

        yield return new WaitForSeconds(_nextScriptDelayTime);

        for (int i = 0; i < 2; ++i)
        {
            if (currentScript)
            {
                currentScript.StartScript();
                while (!currentScript.isScriptEnd)
                    yield return null;
                _currentScript++;
            }
        }
        group.Clear();

        // Scene 4 - clim the tower and come to me
        currentTime = 0.0f;
        fadeTime = 1.5f;
        ratio = 0.0f;
        while (currentTime < fadeTime)
        {
            ratio = currentTime / fadeTime;

            ApplyAlpha(_blackMask, ratio * 0.65f);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_blackMask, 0.65f);

        yield return new WaitForSeconds(_nextScriptDelayTime);

        for (int i = 0; i < 1; ++i)
        {
            if (currentScript)
            {
                currentScript.StartScript();
                while (!currentScript.isScriptEnd)
                    yield return null;
                _currentScript++;
            }
        }

        // Scene 5 - last script
        currentTime = 0.0f;
        fadeTime = 1.5f;
        ratio = 0.0f;
        while (currentTime < fadeTime)
        {
            ratio = currentTime / fadeTime;

            float alpha = Mathf.Lerp(0.65f, 0.0f, ratio);

            ApplyAlpha(_blackMask, alpha);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_blackMask, 0.0f);
        yield return new WaitForSeconds(1.0f);

        //group.Add(_frontChain);
        //group.Add(_backChain);

        //currentTime = 0.0f;
        //fadeTime = 1.5f;
        //ratio = 0.0f;
        //while (currentTime < fadeTime)
        //{
        //    ratio = currentTime / fadeTime;

        //    ApplyListAlpha(group, 1.0f - ratio);

        //    yield return null;

        //    currentTime += Time.deltaTime;
        //}
        //ApplyListAlpha(group, 0.0f);

        yield return new WaitForSeconds(_nextScriptDelayTime);

        for (int i = 0; i < 1; ++i)
        {
            if (currentScript)
            {
                currentScript.StartScript();
                while (!currentScript.isScriptEnd)
                    yield return null;
                _currentScript++;
            }
        }
        group.Clear();

        group.Add(_player);
        group.Add(_frontChain);
        group.Add(_backChain);
        group.Add(_gradient);

        currentTime = 0.0f;
        fadeTime = 1.5f;
        ratio = 0.0f;
        while (currentTime < fadeTime)
        {
            ratio = currentTime / fadeTime;

            ApplyListAlpha(group, 1.0f - ratio);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyListAlpha(group, 0.0f);

        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("StartScene");
    }

    void ApplyAlpha(Graphic g, float alpha)
    {
        Color color = g.color;
        color.a = alpha;
        g.color = color;
    }

    void ApplyListAlpha(List<Graphic> graphics, float alpha)
    {
        foreach (var g in graphics)
            ApplyAlpha(g, alpha);
    }
}
