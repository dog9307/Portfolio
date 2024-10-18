using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//미니맵 관리 스크립트
public class MinimapController : MonoBehaviour
{
    //미니맵 버튼(버튼 클릭 취소하기 위해 만듦)
    Button _minimap;
    //버튼들 버튼간 선택 취소, 확인 작업할때 사용
    GameObject _prevSelectButton;
    public GameObject _currentSelectButton;
    //선택 아이콘
    public GameObject _RoomSelector;
    //이름바
    public RoomNameBar _nameBar;

    //이펙트 on/off
    public bool _roomSelectorEffectorOn;

    [SerializeField]
    private RoomInfo[] _rooms;

    [SerializeField]
    public bool _inIanMemory;

    [SerializeField]
    private SFXPlayer _sfx;
    public SFXPlayer sfx { get { return _sfx; } }

    [SerializeField]
    private AudioClip _bgmClip;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _bgmVolume;
    [SerializeField]
    private AudioClip _ambClip;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _ambVolume;
    [SerializeField]

    private StageBGMPlayer _bgm;


    // Start is called before the first frame update
    void Start()
    {
        _minimap = GetComponent<Button>();
        _minimap.onClick.AddListener(SelectCancle);

        //_nameBar._roomName.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {

        if (!_currentSelectButton) 
        {
            if (_inIanMemory)
                _nameBar._roomName.text = "이안의 기억";
            else
                _nameBar._roomName.text = "호텔 안내 지도";
        }
        //else _nameBar._roomName.text = _currentSelectButton.GetComponentInParent<RoomSelector>().SeenRoomName();
    }
    //방 선택시
    public void SelectRoom()
    {
        if(_sfx) _sfx.PlaySFX("mapRoomClick");

        if (!_prevSelectButton)
        {
            if (_currentSelectButton)
            {
                _prevSelectButton = _currentSelectButton;
            }
            else return;
        }
        else
        {
            if (_currentSelectButton != _prevSelectButton)
            {
                _prevSelectButton.gameObject.GetComponentInParent<RoomSelector>().ClickChecker(false);
                _prevSelectButton.gameObject.GetComponent<Animator>().SetTrigger("disable");
                _prevSelectButton = _currentSelectButton;
            }
        }

        //이름바 오브젝트
        _nameBar.gameObject.SetActive(true);
        //이름바 오브젝트 내의 텍스트에 룸 이름정보 넘김.
        _nameBar._roomName.text = _currentSelectButton.GetComponentInParent<RoomSelector>().SeenRoomName();
    }
    //방 선택 해제시
    public void SelectCancle()
    {
        if (_currentSelectButton)
        {
            if (_sfx) _sfx.PlaySFX("missClick");

            // _currentSelectButton.gameObject.GetComponent<Animator>().SetTrigger("disable");
            _currentSelectButton.gameObject.GetComponentInParent<RoomSelector>().ClickChecker(false);
            _currentSelectButton = null;
            if (_RoomSelector.activeSelf) _RoomSelector.SetActive(false);
        }

    }
    public void LockOff()
    {
        foreach (var r in _rooms)
        {
            if (r._isCanSelect) continue;

            if (r._isRoomActivate)
            {
                RoomManager.instance.ButtonsFreeze();  
                r.GetComponentInChildren<LockOnOff>().gameObject.GetComponent<Animator>().Play("LockOff");
            }
        }
    }
    public void BgmChange()
    {
        if (!_bgm)
            _bgm = FindObjectOfType<StageBGMPlayer>();

        if (_bgm)
        {
            if (_bgmClip)
            {
                _bgm.bgm = _bgmClip;
                _bgm.bgmVolume = _bgmVolume;
                _bgm.PlayBGM();
            }

            if (_ambClip)
            {
                _bgm.amb = _ambClip;
                _bgm.ambVolume = _ambVolume;
            }
        }
    }
}
