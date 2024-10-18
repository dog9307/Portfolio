using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo : MonoBehaviour
{
    public int _roomNum;
    //룸 활성화가 됐는가 아니라면 ???? 이미지
    public bool _isRoomActivate;
    //룸 버튼이 눌릴 수 있는가
    public bool _isCanSelect;
    //룸이 활성화 된 후 들어가 본 적이 있는가 아니라면 이름 ????
    public bool _isEvenOnceEnterRoom;
    //룸 이름 들어가 본 적 있다면 이름으로 아니라면 ????로
    public string _roomName;
}
