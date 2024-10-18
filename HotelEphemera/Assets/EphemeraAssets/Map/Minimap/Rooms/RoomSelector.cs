using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Coffee.UIExtensions;

public class RoomSelector : MonoBehaviour
{
    //미니맵
    [HideInInspector]
    public MinimapController _minimap;
    [SerializeField]
    //현재 버튼
    Button _thisButton;
    //현재 룸 정보
    public RoomInfo _thisRoom;
    //버튼이 눌렸는가
    public bool _isClick;
    bool _isPointEnter;

    [SerializeField]
    Image _roomActiveOff;

    //셀렉트 이펙트
    public GameObject _selectEffectObject;
    public ParticleSystem _selectEffect;
    public Vector3 _selectEffectScale;

    RoomDatas _roomData;
    
    // [SerializeField]
    // RoomButtonParticleManager _buttonParticle;
    //처음 위치.
    //public Vector3 _prevPosition;
    //public Vector3 _prevScale;

    //카메라 오브젝트(추후에 변경)
    public GameObject _camera;
    public void Start()
    {
        //_thisButton = GetComponent<Button>();
        _minimap = GetComponentInParent<MinimapController>();
        _roomData = GetComponent<RoomDatas>();

        if (_startNode)
        {
            string lastRoom = PlayerPrefs.GetString("LastRoom", "GuestRoom");
            if (_thisRoom._roomName == lastRoom)
                MinimapMovableCharacter.instance.SetPos(_startNode);
        }

        _thisRoom._roomName = _roomData.roomName;
        // _prevPosition = this.transform.position;
        // _prevScale = this.transform.localScale;
        if (_thisButton) _thisButton.onClick.AddListener(SelectButton);
    }
    //방 버튼 클릭시.
    public void SelectButton()
    {
        //if (_minimap._currentSelectButton == _thisButton) _minimap._currentSelectButton.GetComponent<RoomSelector>()._isClick = false;


        if (!_isClick)
        {
            _minimap._currentSelectButton = _thisButton.gameObject;
            _minimap.SelectRoom();
            ClickChecker(true);
            ButtonSelector();
            //_thisButton.GetComponent<Animator>().Play("RoomButtonSelect");
            //ButtonSelectEffectOn();
            //ButtonParticleOn("select", _thisButton.transform.position);
            SeenRoomName();

            MinimapCharacterMoveStart();
        }
        else EnterRoom();       
    }
    //방 활성화에 따른 버튼 select 가능 유무.
    private void Update()
    {
        if(_thisRoom != null)
        {
            if (_thisRoom._isCanSelect)
            {
                _thisButton.interactable = true;
                _roomActiveOff.gameObject.SetActive(false);

            }
            else { 
                _thisButton.interactable = false;
                _roomActiveOff.gameObject.SetActive(true);
            }       
            //if (_isClick)
            //    _thisButton.Select();
            //else return;
        }

    }
    
    public UnityEvent onEnterRoom;
    //방에 들어가는 함수
    public void EnterRoom()
    {
        if(_thisRoom._roomName == "그랜드 홀") _minimap.sfx.PlaySFX("openGrandhall");
        else _minimap.sfx.PlaySFX("mapRoomEnter");
        //Debug.Log(_thisRoom._roomName);
        if (onEnterRoom != null)
            onEnterRoom.Invoke();

        _thisRoom._isEvenOnceEnterRoom = true; 
        _minimap._currentSelectButton = null;
        if (_minimap._RoomSelector.activeSelf) _minimap._RoomSelector.SetActive(false);
    }

    //방 이름 정보를 미니맵 컨트롤러 이름바에 넘기는 부분. 방문 기록 없을시 이름 ?????
    public string SeenRoomName() 
    {
        if(_thisRoom)
        {
            if (_thisRoom._isEvenOnceEnterRoom)
            {
                if(_thisRoom._roomName != null) return _thisRoom._roomName;
                else return "?????";
            }
            else
            {
                return "?????";
            }
        }
        else return null;
    }
    public void ClickChecker(bool _ischeck)
    {
        _isClick = _ischeck;
        _thisButton.GetComponent<Animator>().SetBool("isclick", _ischeck);
    }
    public void ButtonSelector()
    {
        if (_minimap._RoomSelector)
        {
            _minimap._RoomSelector.SetActive(true);
            var selectorPos = _thisButton.transform.localPosition;
            selectorPos.x = _thisButton.transform.localPosition.x - 130;
            selectorPos.y = _thisButton.transform.localPosition.y + 130;
            _minimap._RoomSelector.transform.localPosition = selectorPos;
        }
        else return;
    }

    //public void ButtonSelectEffectOn()
    //{
    //    if (_minimap._roomSelectorEffectorOn)
    //    {
    //        if (_selectEffectObject)
    //        {
    //            _selectEffectObject.transform.position = _thisButton.transform.position;
    //            _selectEffectObject.GetComponent<UIParticle>().scale3D = _selectEffectScale;
    //        }
    //        if (_selectEffect && _selectEffect.isPlaying) _selectEffect.Stop();
    //
    //        _selectEffect.Play();
    //    }
    //}

    [Header("미니맵 이동 관련")]
    [SerializeField]
    private MinimapNodes _leftNode;
    [SerializeField]
    private MinimapNodes _rightNode;
    [SerializeField]
    private MinimapNodes _upNode;
    [SerializeField]
    private MinimapNodes _downNode;

    public void MinimapCharacterMoveStart()
    {
        if (!_leftNode && !_rightNode && !_upNode && !_downNode) return;

        if (MinimapMovableCharacter.instance.currentNode == _leftNode   ||
            MinimapMovableCharacter.instance.currentNode == _rightNode  ||
            MinimapMovableCharacter.instance.currentNode == _upNode     ||
            MinimapMovableCharacter.instance.currentNode == _downNode)
            return;

        MinimapNodes targetNode = null;

        Vector3 movablePos = MinimapMovableCharacter.instance.currentNode.transform.position;
        Vector3 selectorPos = GetComponentInChildren<Button>().transform.position;

        if (Mathf.Abs(movablePos.y - selectorPos.y) < 10.0f)
        {
            if (movablePos.x < selectorPos.x && _leftNode)
                targetNode = _leftNode;
            else if (movablePos.x > selectorPos.x && _rightNode)
                targetNode = _rightNode;

            if (!targetNode)
            {
                if (movablePos.y > selectorPos.y && _upNode)
                    targetNode = _upNode;
                else if (movablePos.y < selectorPos.y && _downNode)
                    targetNode = _downNode;
            }
        }
        else
        {
            if (movablePos.y > selectorPos.y && _upNode)
                targetNode = _upNode;
            else if (movablePos.y < selectorPos.y && _downNode)
                targetNode = _downNode;

            if (!targetNode)
            {
                if (movablePos.x < selectorPos.x && _leftNode)
                    targetNode = _leftNode;
                else if (movablePos.x > selectorPos.x && _rightNode)
                    targetNode = _rightNode;
            }
        }

        if (!targetNode) return;

        MinimapMovableCharacter.instance.MoveStart(targetNode);

        SavableNode node = new SavableNode();
        node.key = "LastRoom";
        node.value = "GuestRoom";
        SavableDataManager.instance.AddSavableObject(node);
    }

    [SerializeField]
    private MinimapNodes _startNode;
}