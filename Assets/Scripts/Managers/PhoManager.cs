using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoManager : Photon.MonoBehaviour {
void AutoConnect(string gameVersion )
    {
        PhotonNetwork.ConnectUsingSettings(gameVersion);
    }

    public virtual void OnJoinedLobby()
    {
        RoomOptions roomops = new RoomOptions() { IsVisible = true, MaxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom("hello", roomops, TypedLobby.Default);
        Debug.Log("Hello World");
    }

    void Start()
    {
        AutoConnect("test");
    }
}
