using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UndergroundChainController : MonoBehaviour
{
    private PlayerMoveController _player;

    [SerializeField]
    private SFXPlayer _sfx;

    [SerializeField]
    private int _keyCount = 3;
    private int _currentKeyCount = 0;
    private int _currentGroup = 0;

    [SerializeField]
    private List<Animator> _group1;
    [SerializeField]
    private List<Animator> _group2;
    [SerializeField]
    private List<Animator> _group3;
    [SerializeField]
    private List<Animator> _group4;

    private List<List<Animator>> _groups = new List<List<Animator>>();

    [SerializeField]
    private float[] _orthoSizes = { 16, 12, 8, 5 };

    [SerializeField]
    private SpriteRenderer _opusChain;
    [SerializeField]
    private Animator _opusAnim;

    [SerializeField]
    private CinemachineVirtualCamera _chainVCam;
    [SerializeField]
    private CinemachineVirtualCamera _playerVCam;

    [SerializeField]
    private TutorialFlowController _tuto;

    [SerializeField]
    private GameObject _effect;

    [SerializeField]
    private GameObject _blackMask;

    public bool isEnd { get; set; }
    [SerializeField]
    private bool _isCoolTime;
    [SerializeField]
    private float _chainCoolTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        _groups.Add(_group1);
        _groups.Add(_group2);
        _groups.Add(_group3);
        _groups.Add(_group4);

        int chaninCount = PlayerPrefs.GetInt("UndergroundChainDone", 0);
        if (chaninCount != 0)
        {
            KeyManager.instance.Enable("menu");

            if (_playerVCam)
                _playerVCam.m_Lens.OrthographicSize = _orthoSizes[0];

            foreach (var g in _groups)
            {
                foreach (var o in g)
                    Destroy(o.gameObject);
            }

            Destroy(_effect);

            Destroy(this);
            Destroy(_blackMask);

            return;
        }

        KeyManager.instance.Disable("menu");

        _effect.SetActive(false);

        _player = FindObjectOfType<PlayerFindHelper>().player.GetComponent<PlayerMoveController>();
        _player.Move(_opusChain.transform.position);
        _player.PlayerMoveFreeze(true);
        _player.gameObject.SetActive(false);

        _currentKeyCount = 0;
        _currentZoom = _orthoSizes[_currentKeyCount];

        isEnd = false;
        _isCoolTime = false;

        _chainVCam.Priority = 9999;

        //StartCoroutine(FindPlayerVCam());
    }

    //IEnumerator FindPlayerVCam()
    //{
    //    FindingPlayerVCam playerVCam = null;
    //    while (!playerVCam)
    //    {
    //        yield return null;
    //        playerVCam = FindObjectOfType<FindingPlayerVCam>();
    //    }

    //    playerVCam.cam.m_Lens.OrthographicSize = 12;
    //}

    // Update is called once per frame
    void Update()
    {
        if (isEnd) return;
        if (_isCoolTime) return;

        if (Input.anyKeyDown)
        {
            if (_currentGroup < _groups.Count)
            {
                _currentKeyCount++;
                if (_currentKeyCount < _keyCount)
                {
                    foreach (var anim in _groups[_currentGroup])
                        anim.SetTrigger("shake");

                    _sfx.PlaySFX("chain_click");

                    StartCoroutine(CoolTime(_chainCoolTime));
                }
                else
                {
                    foreach (var anim in _groups[_currentGroup])
                        anim.SetTrigger("out");

                    _currentKeyCount = 0;
                    _currentGroup++;

                    _sfx.PlaySFX("chain_break");

                    Invoke("ZoomIn", 0.3f);

                    StartCoroutine(CoolTime(_chainCoolTime * 2.0f));
                }
            }
            else
            {
                _currentKeyCount++;
                if (_currentKeyCount >= _keyCount)
                {
                    _opusChain.material = Instantiate<Material>(Resources.Load<Material>("ShaderEffect/DissolvableGlow/DissolvableGlowMaterialLit"));

                    _opusAnim.SetTrigger("out");

                    _sfx.PlaySFX("chain_disappear");

                    isEnd = true;
                }
                else
                {
                    _sfx.PlaySFX("chain_click");

                    StartCoroutine(CoolTime(_chainCoolTime));
                }
            }

            CameraShakeController.instance.CameraShake(2.0f);
        }
    }

    IEnumerator CoolTime(float coolTime)
    {
        _isCoolTime = true;
        yield return new WaitForSeconds(coolTime);
        _isCoolTime = false;
    }

    void ZoomIn()
    {
        if (_currentGroup < _groups.Count)
        {
            if (_zoomCoroutine != null)
                StopCoroutine(_zoomCoroutine);

            _zoomCoroutine = StartCoroutine(Zoom(_orthoSizes[_currentGroup]));
        }
    }

    private int _coroutineCount = 0;
    private float _currentZoom;
    private Coroutine _zoomCoroutine;
    IEnumerator Zoom(float toZoom)
    {
        //_coroutineCount++;
        float currentTime = 0.0f;
        float totalTime = 0.7f;
        float startZoom = _currentZoom;

        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;

            _currentZoom = Mathf.Lerp(startZoom, toZoom, ratio);
            _chainVCam.m_Lens.OrthographicSize = _currentZoom;

            yield return null;

            //if (_coroutineCount > 1)
            //    break;

            currentTime += IsterTimeManager.deltaTime;
        }
        _currentZoom = Mathf.Lerp(startZoom, toZoom, 1.0f);
        _chainVCam.m_Lens.OrthographicSize = _currentZoom;

        //_coroutineCount--;

        _zoomCoroutine = null;
    }

    private VCamChanger _changer;
    IEnumerator VirtualCamChange()
    {
        FindingPlayerVCam playerVCam = null;
        while (!playerVCam)
        {
            yield return null;
            playerVCam = FindObjectOfType<FindingPlayerVCam>();
        }

        playerVCam.cam.m_Lens.OrthographicSize = 9;

        _chainVCam.Priority = -9999;

        yield return new WaitForSeconds(1.0f);

        //float currentTime = 0.0f;
        //float totalTime = 1.0f;

        //CinemachineBrain brain = FindObjectOfType<CinemachineBrain>();
        //brain.m_DefaultBlend.m_Time = totalTime;
        //while (currentTime < totalTime)
        //{
        //    yield return null;

        //    currentTime += IsterTimeManager.deltaTime;
        //}
        //brain.m_DefaultBlend.m_Time = 0.0f;

        if (_changer)
            _changer.enterBlendTime = 0.0f;
    }

    public void DissolveEnd()
    {
        _sfx.PlaySFX("chain_impact");
        _effect.SetActive(true);
        _player.gameObject.SetActive(true);
        _opusAnim.gameObject.SetActive(false);

        _tuto.MoveFreeze();

        _changer = FindObjectOfType<VCamChanger>();
        if (_changer)
            _changer.enterBlendTime = 1.0f;

        StartCoroutine(VirtualCamChange());

        foreach (var group in _groups)
        {
            foreach (var c in group)
                c.gameObject.SetActive(false);
        }
        Destroy(_blackMask);

        KeyManager.instance.Enable("menu");
    }
}
