﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamManager : Photon.MonoBehaviour
{

    static GamManager instance = null;
    public Camera mainVRCamera;
    public VRPlayer player;

    /**Settings */
    [SerializeField]
    private bool LeftHanded = false;
    [SerializeField]
    private PhoManager Network_Manager;
    [SerializeField]
    private EntityManager entity_manager;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    private bool Gameready;
    public int difficulty;
    

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
    // Use this for initialization
    void Start()
    {
        if (Network_Manager != null)
            Network_Manager.AutoConnect("test");

    }

    public bool IsLeftHanded()
    {
        return LeftHanded;
    }

    void gamestart(bool NetworkState, int difficulty)
    {
        if (!NetworkState)
        {
            Debug.Log("Network state isn't ready");
            return;
        }
        //such next gen.
        StartCoroutine(entity_manager.SpawnWave(difficulty, spawnWait, startWait, waveWait, NetworkState));
    }

    //cleanup coroutines,kicks everyone off network.
    void gameover()
    {
        StopAllCoroutines();
        PhotonNetwork.CloseConnection(PhotonNetwork.player);
        PhotonNetwork.Disconnect();
    }

    //This here is a test. When running this function please use 
    //gamestart(Network_Manager.NetworkStatus(),difficulty) It should return a proper network status.
    public virtual void OnJoinedRoom()
    {
        gamestart(true, difficulty);
    }

}
