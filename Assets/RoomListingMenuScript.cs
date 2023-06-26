using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomListingMenuScript : MonoBehaviourPunCallbacks
{
    public RoomListingPrefabScript roomListingPrefab;
    public Transform scrollView;
    private List<RoomListingPrefabScript> prefabList = new List<RoomListingPrefabScript>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Updating Rooms");
        updateRooms(roomList);
        photonView.RPC("updateRooms", RpcTarget.All, roomList);

    }

    [PunRPC]
    public void updateRooms(List<RoomInfo> roomList)
    {
        foreach (RoomListingPrefabScript list in prefabList)
        {
            Destroy(list.gameObject);
            prefabList.Remove(list);
        }

        foreach (RoomInfo info in roomList)
        {

            if (info.RemovedFromList)
            {
                int index = prefabList.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(prefabList[index].gameObject);
                    prefabList.RemoveAt(index);
                }
            }
            else
            {
                Debug.Log("Instantiating Prefab to populate content");
                RoomListingPrefabScript listing = Instantiate(roomListingPrefab, scrollView);
                if (listing != null)
                {
                    listing.SetRoomInfo(info);
                    prefabList.Add(listing);
                }

            }

        }
    }

}
