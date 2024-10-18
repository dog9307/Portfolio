using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DieSceneController : MonoBehaviour
{
    [SerializeField]
    private Graphic _dieText;
    [SerializeField]
    private Graphic _blackMask;

    [SerializeField]
    private SFXPlayer _sfx;
    [SerializeField]
    private float _soundDelay = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerDieEffectFindHelper die = FindObjectOfType<PlayerDieEffectFindHelper>();
        if (die)
            die.relativeObj.SetActive(false);

        _dieText.gameObject.SetActive(false);

        StageBGMPlayer bgm = FindObjectOfType<StageBGMPlayer>();
        if (bgm)
            bgm.PlayBGM();

        StartCoroutine(SoundDelay());
        StartCoroutine(CutScene());
    }

    IEnumerator SoundDelay()
    {
        yield return new WaitForSeconds(_soundDelay);
        if (_sfx)
            _sfx.PlaySFX("die");
    }

    IEnumerator CutScene()
    {
        yield return new WaitForSeconds(9.541f);
        //yield return new WaitForSeconds(1.0f);

        //_dieText.gameObject.SetActive(true);

        //yield return new WaitForSeconds(5.0f);

        //float currentTime = 0.0f;
        //float fadeTime = 3.0f;
        //Color color;
        //while (currentTime < fadeTime)
        //{
        //    color = _blackMask.color;
        //    color.a = (currentTime / fadeTime);
        //    _blackMask.color = color;

        //    yield return null;

        //    currentTime += IsterTimeManager.originDeltaTime;
        //}
        //color = _dieText.color;
        //color.a = 0.0f;
        //_dieText.color = color;

        SceneManager.LoadScene("PlayerMoveScene");
        SceneManager.LoadScene("IngameUiScene", LoadSceneMode.Additive);
    }
}
