using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialPopUp : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private FadingGuideUI _fading;

    private bool _isPopUp;
    private bool _isPopUpOver;
    public bool isPopUpOver { get { return _isPopUpOver; } }

    private static PlayerMoveController _player;

    [SerializeField]
    private SFXPlayer _sfx;

    [SerializeField]
    private ParticleSystem _effect;

    public UnityEvent onPopUpEnd;

    [SerializeField]
    private string _key;

    // Start is called before the first frame update
    void Start()
    {
        int count = PlayerPrefs.GetInt(_key, -1);
        if (count >= 100)
        {
            Destroy(gameObject);
            return;
        }

        if (!_fading)
            Destroy(gameObject);

        _isPopUp = false;
        _isPopUpOver = false;
    }

    void Update()
    {
        if (!_isPopUp) return;

        if (Input.anyKeyDown)
        {
            _isPopUp = false;

            _sfx.PlaySFX("disappear");
        }
    }

    public void StartPopUp()
    {
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        _isPopUpOver = false;

        _sfx.PlaySFX("appear");

        PlayerPrefs.SetInt(_key, 100);

        StartCoroutine(PopUp());
    }

    IEnumerator PopUp()
    {
        if (_effect)
            _effect.Play();

        _player.PlayerMoveFreeze(true);

        CameraBlurController blur = FindObjectOfType<CameraBlurController>();

        _fading.StartFading(1.0f);

        if (blur)
            blur.StartBlur(50.0f);

        yield return new WaitForSeconds(_fading.totalFadeTime * 1.5f);

        _isPopUp = true;
        while (_isPopUp)
            yield return null;

        _fading.StartFading(0.0f);

        if (blur)
            blur.StartBlur(1.0f);

        _isPopUpOver = true;

        _player.PlayerMoveFreeze(false);

        if (_effect)
            _effect.Stop();

        if (onPopUpEnd != null)
            onPopUpEnd.Invoke();
    }
}
