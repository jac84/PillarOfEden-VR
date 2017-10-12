using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoManager : Photon.MonoBehaviour {

    private GameObject PlayerObj;


    public void AutoConnect(string gameVersion )
    {
        PhotonNetwork.ConnectUsingSettings(gameVersion);
        Debug.Log(PhotonNetwork.connectionStateDetailed.ToString());
    }
    public void RoomSetup()
    {
        RoomOptions roomops = new RoomOptions() { IsVisible = true, MaxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom("hello", roomops, TypedLobby.Default);
        Debug.Log("Hello World");

    }

    //Seems redundant but just in case we want to do other other things when the clinet
    //joins lobby.
    public virtual void OnJoinedLobby()
    {
        RoomSetup();
    }
    //Really can't do anything until this happens.
    public virtual void OnJoinedRoom()
    {
        PlayerObj = PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), 0);
        PlayerObj.GetComponent<PhotonView>().owner.NickName = "V.R Player";
    }


}
