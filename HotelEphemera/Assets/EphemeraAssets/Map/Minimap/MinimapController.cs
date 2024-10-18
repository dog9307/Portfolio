using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�̴ϸ� ���� ��ũ��Ʈ
public class MinimapController : MonoBehaviour
{
    //�̴ϸ� ��ư(��ư Ŭ�� ����ϱ� ���� ����)
    Button _minimap;
    //��ư�� ��ư�� ���� ���, Ȯ�� �۾��Ҷ� ���
    GameObject _prevSelectButton;
    public GameObject _currentSelectButton;
    //���� ������
    public GameObject _RoomSelector;
    //�̸���
    public RoomNameBar _nameBar;

    //����Ʈ on/off
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
                _nameBar._roomName.text = "�̾��� ���";
            else
                _nameBar._roomName.text = "ȣ�� �ȳ� ����";
        }
        //else _nameBar._roomName.text = _currentSelectButton.GetComponentInParent<RoomSelector>().SeenRoomName();
    }
    //�� ���ý�
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

        //�̸��� ������Ʈ
        _nameBar.gameObject.SetActive(true);
        //�̸��� ������Ʈ ���� �ؽ�Ʈ�� �� �̸����� �ѱ�.
        _nameBar._roomName.text = _currentSelectButton.GetComponentInParent<RoomSelector>().SeenRoomName();
    }
    //�� ���� ������
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
