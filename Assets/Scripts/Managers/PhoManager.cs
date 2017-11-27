using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoManager : Photon.MonoBehaviour {

    private GameObject PlayerObj;
    private bool networkstatus= false;

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
    public virtual void OnJoinedLobby()
    {
        RoomSetup();
    }
    //Really can't do anything until this happens.
    public virtual void OnJoinedRoom()
    {
        //networkstatus = true;
        //PlayerObj = PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), 0);
        //PlayerObj.GetComponent<PhotonView>().owner.NickName = "V.R Player";
    }
    
    public virtual void OnLeftRoom()
    {
        networkstatus = false;
    }

    public bool NetworkStatus()
    {
        if (PhotonNetwork.playerList.Length > 1)
        {
            networkstatus = true;
        }
        return networkstatus;
    }
    //Another player connecting
    public virtual void OnPhotonPlayerConnected(PhotonPlayer other)
    {
        Debug.Log("Mobile player has connected! " + other.NickName);
    }

    //We will have to decide what the VR client should upon this action.
    public virtual void OnPhotonPlayerDisconnected(PhotonPlayer other)
    {
        Debug.Log("Mobile player been disconnected");
    }
}
