using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingIllustFader : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private FadingGuideUI _fading;

    [HideInInspector]
    [SerializeField]
    private EndingIllustReceiver _receiver;

    private bool _isSkip = false;

    [SerializeField]
    private SFXPlayer _sfx;

    void Start()
    {
        ShowIllust();
    }

    void Update()
    {
        if (_isSkip) return;

        if (Input.anyKeyDown)
            _isSkip = true;
    }

    public void ShowIllust()
    {
        if (!_fading) return;

        _isSkip = false;

        StartCoroutine(Fading());
    }

    IEnumerator Fading()
    {
        if (_receiver.ReceiveEndingIllust())
        {
            if (_sfx)
                _sfx.PlaySFX("illustOn");

            _fading.StartFading(1.0f);

            yield return new WaitForSeconds(_fading.fadeDuration * 2.0f);

            while (!_isSkip)
                yield return null;
        }

        _fading.StartFading(0.0f);

        yield return new WaitForSeconds(_fading.fadeDuration);

        SceneManager.LoadScene("PlayerMoveScene");
        SceneManager.LoadScene("IngameUiScene", LoadSceneMode.Additive);
    }
}
