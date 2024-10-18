using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OutroManager : MonoBehaviour
{
    [SerializeField]
    private Graphic _chain;
    [SerializeField]
    private Graphic _title;
    [SerializeField]
    private Graphic _thxText;
    [SerializeField]
    private Graphic _godmaker;
    [SerializeField]
    private Graphic _script0;
    [SerializeField]
    private Graphic _script1;
    [SerializeField]
    private Graphic _whitePanel;
    [SerializeField]
    private Graphic _logo;
    [SerializeField]
    private Graphic _black;

    [Header("사운드")]
    [SerializeField]
    private StageBGMPlayer _bgm;
    [SerializeField]
    private float _soundDelay = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _chain.gameObject.SetActive(true);
        _title.gameObject.SetActive(true);
        _thxText.gameObject.SetActive(true);
        _godmaker.gameObject.SetActive(true);
        _script0.gameObject.SetActive(true);
        _whitePanel.gameObject.SetActive(false);
        _logo.gameObject.SetActive(true);
        _black.gameObject.SetActive(true);

        ApplyAlpha(_chain, 0.0f);
        ApplyAlpha(_title, 0.0f);
        ApplyAlpha(_thxText, 0.0f);
        ApplyAlpha(_godmaker, 0.0f);
        ApplyAlpha(_script0, 0.0f);
        ApplyAlpha(_whitePanel, 1.0f);
        ApplyAlpha(_logo, 0.0f);
        ApplyAlpha(_black, 0.0f);


        StartCoroutine(PlayeSound());
        StartCoroutine(CutScene());
    }

    IEnumerator PlayeSound()
    {
        yield return new WaitForSeconds(_soundDelay);

        _bgm.PlaySound();
    }

    IEnumerator CutScene()
    {
        yield return null;
        float currentTime = 0.0f;
        float totalTime = 0.0f;
        float ratio = 0.0f;
        float alpha = 0.0f;


        //1. 쇠사슬 스르륵
        currentTime = 0.0f;
        totalTime = 2.0f;
        ratio = 0.0f;
        while (currentTime < totalTime)
        {
            ratio = currentTime / totalTime;
            alpha = Mathf.Lerp(0.0f, 1.0f, ratio);

            ApplyAlpha(_chain, alpha);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_chain, 1.0f);

        yield return new WaitForSeconds(1.0f);

        //2.ISTER 양산이 등장
        currentTime = 0.0f;
        totalTime = 2.0f;
        ratio = 0.0f;
        while (currentTime < totalTime)
        {
            ratio = currentTime / totalTime;
            alpha = Mathf.Lerp(0.0f, 1.0f, ratio);

            ApplyAlpha(_title, alpha);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_title, 1.0f);

        yield return new WaitForSeconds(1.0f);

        //3.감사합니다 등장
        currentTime = 0.0f;
        totalTime = 2.0f;
        ratio = 0.0f;
        while (currentTime < totalTime)
        {
            ratio = currentTime / totalTime;
            alpha = Mathf.Lerp(0.0f, 1.0f, ratio);

            ApplyAlpha(_thxText, alpha);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_thxText, 1.0f);

        yield return new WaitForSeconds(5.0f);

        //4. 1번, 2번 3번 같이 스르륵 사라짐
        currentTime = 0.0f;
        totalTime = 2.0f;
        ratio = 0.0f;
        while (currentTime < totalTime)
        {
            ratio = currentTime / totalTime;
            alpha = Mathf.Lerp(1.0f, 0.0f, ratio);

            ApplyAlpha(_chain, alpha);
            ApplyAlpha(_title, alpha);
            ApplyAlpha(_thxText, alpha);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_chain, 0.0f);
        ApplyAlpha(_title, 0.0f);
        ApplyAlpha(_thxText, 0.0f);

        yield return new WaitForSeconds(2.0f);

        //5.쇠사슬 그대로 어두운 상태에서 갓메이커 음흉하게 등장
        currentTime = 0.0f;
        totalTime = 2.0f;
        ratio = 0.0f;
        while (currentTime < totalTime)
        {
            ratio = currentTime / totalTime;
            alpha = Mathf.Lerp(0.0f, 0.6f, ratio);

            ApplyAlpha(_godmaker, alpha);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_godmaker, 0.6f);

        yield return new WaitForSeconds(3.0f);

        //6.갓메이커 사라지면서
        currentTime = 0.0f;
        totalTime = 1.0f;
        ratio = 0.0f;
        while (currentTime < totalTime)
        {
            ratio = currentTime / totalTime;
            alpha = Mathf.Lerp(0.6f, 0.3f, ratio);

            ApplyAlpha(_godmaker, alpha);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_godmaker, 0.3f);
        // 대사1 등장
        currentTime = 0.0f;
        totalTime = 3.0f;
        ratio = 0.0f;
        while (currentTime < totalTime)
        {
            ratio = currentTime / totalTime;
            alpha = Mathf.Lerp(0.0f, 1.0f, ratio);

            ApplyAlpha(_script0, alpha);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_script0, 1.0f);

        yield return new WaitForSeconds(3.0f);
        // 대사1 사라짐
        currentTime = 0.0f;
        totalTime = 3.0f;
        ratio = 0.0f;
        while (currentTime < totalTime)
        {
            ratio = currentTime / totalTime;
            alpha = Mathf.Lerp(1.0f, 0.0f, ratio);

            ApplyAlpha(_script0, alpha);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_script0, 0.0f);

        // 대사2 등장
        currentTime = 0.0f;
        totalTime = 3.0f;
        ratio = 0.0f;
        while (currentTime < totalTime)
        {
            ratio = currentTime / totalTime;
            alpha = Mathf.Lerp(0.0f, 1.0f, ratio);

            ApplyAlpha(_script1, alpha);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_script1, 1.0f);

        yield return new WaitForSeconds(3.0f);

        //7.쇠사슬이랑 대사 다 사라지면서 검정화면 잠깐
        currentTime = 0.0f;
        totalTime = 2.0f;
        ratio = 0.0f;
        while (currentTime < totalTime)
        {
            ratio = currentTime / totalTime;
            alpha = Mathf.Lerp(0.3f, 0.0f, ratio);

            ApplyAlpha(_godmaker, alpha);

            alpha = Mathf.Lerp(1.0f, 0.0f, ratio);

            ApplyAlpha(_script1, alpha);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_godmaker, 0.0f);
        ApplyAlpha(_script1, 0.0f);

        yield return new WaitForSeconds(3.0f);

        //8.로고 쨘
        _whitePanel.gameObject.SetActive(true);
        currentTime = 0.0f;
        totalTime = 1.0f;
        ratio = 0.0f;
        while (currentTime < totalTime)
        {
            ratio = currentTime / totalTime;
            alpha = Mathf.Lerp(0.0f, 1.0f, ratio);

            ApplyAlpha(_logo, alpha);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_logo, 1.0f);

        yield return new WaitForSeconds(3.0f);

        currentTime = 0.0f;
        totalTime = 1.0f;
        ratio = 0.0f;
        while (currentTime < totalTime)
        {
            ratio = currentTime / totalTime;
            alpha = Mathf.Lerp(1.0f, 0.0f, ratio);

            ApplyAlpha(_logo, alpha);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_logo, 0.0f);

        currentTime = 0.0f;
        totalTime = 1.0f;
        ratio = 0.0f;
        while (currentTime < totalTime)
        {
            ratio = currentTime / totalTime;
            alpha = Mathf.Lerp(0.0f, 1.0f, ratio);

            ApplyAlpha(_black, alpha);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(_black, 1.0f);

        yield return new WaitForSeconds(1.0f);

        //9.타이틀 화면
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }

    void ApplyAlpha(Graphic g, float a)
    {
        Color color = g.color;
        color.a = a;
        g.color = color;
    }
}
