using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    //instance
    public static NetworkManager instance;

    private string defaultUsername = "Player";

    [SerializeField] private InputField usernameInput;
    [SerializeField] private InputField createandjoinRoomInput;

    [SerializeField] private GameObject changeUsernameButton;
    [SerializeField] private GameObject createRoomButton;
    [SerializeField] private GameObject joinRoomButton;

    [SerializeField] private Text roomNames;
    bool setUsernameBool = false;

    Dictionary<string, int> roomInfo = new Dictionary<string, int>();
    void Awake()
    {
        if(instance != null && instance != this)
        {
            gameObject.SetActive(false);
        }
        else
        {
            //set the instance
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
    }

    public void CreateRoom()
    {
        if (!setUsernameBool)
        {
            Debug.Log("Player Name Null");
            PhotonNetwork.LocalPlayer.NickName = defaultUsername + " " + PhotonNetwork.CountOfPlayers;
        }
        Debug.Log("Creating Room");
        PhotonNetwork.CreateRoom(createandjoinRoomInput.text, new RoomOptions() { MaxPlayers = 2, IsVisible = true }, null);
    }
    public void JoinRoom()
    {
        if (!setUsernameBool)
        {
            Debug.Log("Player Name Null");
            PhotonNetwork.LocalPlayer.NickName = defaultUsername + " " + PhotonNetwork.CountOfPlayers;
        }
        Debug.Log("Joining Room");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsVisible = true;
        
        PhotonNetwork.JoinOrCreateRoom(createandjoinRoomInput.text, roomOptions, TypedLobby.Default);
    }
    public void ChangeScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }

    public void ChangeUserNameInput()
    {

    }
    public void setUsername()
    {
        setUsernameBool = true;
        PhotonNetwork.LocalPlayer.NickName = usernameInput.text;
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
        Invoke("CountPlayers", 1.0f);
    }
    public void CountPlayers()
    {
        Debug.Log("Joining: " + PhotonNetwork.CountOfPlayers + " players in room");
        foreach(var player in PhotonNetwork.PlayerList)
        {

            Debug.Log("Name: " + player.NickName);
        }
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Rooms have changed");
        roomNames.text = "";
        foreach (KeyValuePair<string, int> kvp in roomInfo)
        {
            roomNames.text += "\n" + kvp.Key + ": " + kvp.Key + "/2 players";
        }
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room: " + PhotonNetwork.CurrentRoom.Name);
    }
}
