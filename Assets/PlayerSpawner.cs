using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab = null;
    [SerializeField] private GameObject leaveRoom = null;
    public GameObject cameraPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity); 
        //PhotonNetwork.Instantiate(cameraPrefab.name, Vector3.zero, Quaternion.identity);
        
    }
    public void leaveRoomOnClick()
    {
        StartCoroutine(Disconnect());
    }
    IEnumerator Disconnect()
    {
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
        {
            yield return null;
        }
        PhotonNetwork.LoadLevel(0);

    }

    // Update is called once per frame
    void Update()
    {

    }
}