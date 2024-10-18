using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class SceneLoader : MonoBehaviour
{
    //-----씬 로더 기본구성
    protected static SceneLoader Instance;

    public static SceneLoader instance
    {
        get {
            if (Instance == null)
            {
                var obj = FindObjectOfType<SceneLoader>();
                if (obj != null)
                {
                    Instance = obj;
                }
                else
                {
                    Instance = create();
                }
            }

            return Instance;
        }
        private set
        {
            Instance = value;
        }
    }

    //전체 패널의 투명도 조절 ( 페이드인 아웃)을 위한 캔버스 그룹 사용
    [SerializeField]
    private CanvasGroup sceneLoaderCanvasGroup;

    //진행도를 보여주기 위한 이미지(프로그래스바)
    [SerializeField]
    private Image progressBar;
    [SerializeField]
    private Image progressBarBack;
    [SerializeField]
    Slider _sessackSlider;

    private string loadSceneName;
    private Queue<string> _loadingScenes = new Queue<string>();
    private Queue<string> _unloadingScenes = new Queue<string>();

    [SerializeField]
    [Range(0.0f,1.0f)]
    float _sceneLoadDelay;
    public static SceneLoader create()
    {
        var SceneLoaderPrefab = Resources.Load<SceneLoader>("UIs/Prefabs/SceneLoader");
        return Instantiate(SceneLoaderPrefab);
    }

    private void Awake()
    {
        if(instance !=this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    //---------

    public void AddUnloadScene(string name)
    {
        _unloadingScenes.Enqueue(name);
    }

    public void AddScene(string name)
    {
        _loadingScenes.Enqueue(name);
    }

    public void LoadScene(LoadSceneMode loadMode = LoadSceneMode.Single)
    {
        //if (Camera.main)
        //{
        //    SkipBlend skip = Camera.main.GetComponent<SkipBlend>();
        //    if (skip)
        //        skip.isSkip = true;
        //}

        gameObject.SetActive(true);
        SceneManager.sceneLoaded += LoadSceneEnd;
        loadSceneName = _loadingScenes.Dequeue();
        StartCoroutine(Load(loadSceneName, loadMode));
    }

    private IEnumerator Load(string sceneName, LoadSceneMode loadMode)
    {
        progressBar.fillAmount = 0f;
        progressBar.fillOrigin = 0;

        //_sessackSlider.value = 0f;

        //페이드인 페이드 아웃용 코루틴
        yield return StartCoroutine(Fade(true));

        //씬 부르되 장면전환을 제어해놓음
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, loadMode);
        List<AsyncOperation> ops = new List<AsyncOperation>();
        for (int i = 0; i < _loadingScenes.Count;)
            ops.Add(SceneManager.LoadSceneAsync(_loadingScenes.Dequeue(), LoadSceneMode.Additive));
        //전환 제어
        op.allowSceneActivation = false;

        float timer = 0.0f;

        //불러오기 다 안됬다면
        while (!op.isDone)
        {
            yield return null;
            timer += _sceneLoadDelay * Time.unscaledDeltaTime;
            //90퍼 이하면
            if (op.progress < 0.9f)
            {
                //_sessackSlider.value = Mathf.Lerp(_sessackSlider.value, op.progress, timer);
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
                //if (_sessackSlider.value >= op.progress)
                //{
                //    timer = 0f;
                //}
            }
            //그 이상일때
            else
            {   //프로그래스바 다 채워주고
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                //_sessackSlider.value = Mathf.Lerp(_sessackSlider.value, 1f, timer);
                //다 채웠을때
                if (progressBar.fillAmount >= 1.0f)
                {
                    //비로소 장면이 바뀌게 함.
                    op.allowSceneActivation = true;
                    for (int i = 0; i < ops.Count;)
                    {
                        if (ops[i].isDone)
                            ops.Remove(ops[i]);
                        else
                            yield return null;
                    }
                    break;
                }
                //if (_sessackSlider.value >= 1.0f)
                //{
                //    //비로소 장면이 바뀌게 함.
                //    op.allowSceneActivation = true;
                //    for (int i = 0; i < ops.Count;)
                //    {
                //        if (ops[i].isDone)
                //            ops.Remove(ops[i]);
                //        else
                //            yield return null;
                //    }
                //    break;
                //}
            }
        }
    }
    private void LoadSceneEnd(Scene scene,LoadSceneMode loadSceneMode)
    {
        if(scene.name == loadSceneName)
        {
            //if (Camera.main)
            //{
            //    SkipBlend skip = Camera.main.GetComponent<SkipBlend>();
            //    if (skip)
            //        skip.isSkip = true;
            //}

            for (int i = 0; i < _unloadingScenes.Count;)
                SceneManager.UnloadSceneAsync(_unloadingScenes.Dequeue());

            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= LoadSceneEnd;
        }
    }
    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;
        while (timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 2f;
            sceneLoaderCanvasGroup.alpha = Mathf.Lerp(isFadeIn ?0:1,isFadeIn? 1:0,timer);
        }
        if(!isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }
}


