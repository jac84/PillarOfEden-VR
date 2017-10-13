using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamManager : Photon.MonoBehaviour {
    
    [SerializeField] private PhoManager Network_Manager;

    // Use this for initialization
    void Start () {
        Network_Manager.AutoConnect("test");
        
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
