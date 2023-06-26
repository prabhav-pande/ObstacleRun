using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomListingPrefabScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Text _text;

    public RoomInfo RoomInfo { get; private set; }

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        _text.text = roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers + " " + roomInfo.Name;
        Debug.Log("Room Stuff:"+roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers + " " + roomInfo.Name);
    }
}
