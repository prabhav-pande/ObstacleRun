using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayerListing : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public static PlayerListing instance;

    public Text playerGameMenuText;

    void Awake()
    {
        if (instance != null && instance != this)
            gameObject.SetActive(false);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    [PunRPC]
    public void UpdatePlayerList()
    {
        string gameMenuString = "Room Name: " + PhotonNetwork.CurrentRoom.Name+"\nPlayers: \n";
        int count = 0;
        foreach (var player in PhotonNetwork.PlayerList)
        {
            count++;
            Debug.Log(player.NickName);
            gameMenuString += count+") "+ player.NickName + "\n";
        }
        Debug.Log("String value: "+ gameMenuString);
        playerGameMenuText.text = gameMenuString;
    }
    void Start()
    {
        UpdatePlayerList();
        photonView.RPC("UpdatePlayerList", RpcTarget.All);
    }
    public override void OnCreatedRoom()
    {
        UpdatePlayerList();
        photonView.RPC("UpdatePlayerList", RpcTarget.All);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
        photonView.RPC("UpdatePlayerList", RpcTarget.All);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
        photonView.RPC("UpdatePlayerList", RpcTarget.All);
    }
}
