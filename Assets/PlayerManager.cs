using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using System.IO;
public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
        Debug.Log("Instantiated Player Controller");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
