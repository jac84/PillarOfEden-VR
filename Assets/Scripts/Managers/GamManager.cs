using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamManager : Photon.MonoBehaviour
{

    static GamManager instance = null;
    public Camera mainVRCamera;

    /**Settings */
    [SerializeField] private bool IsLeftHanded = false;
    [SerializeField] private PhoManager Network_Manager;

    public static GamManager singleton
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
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
