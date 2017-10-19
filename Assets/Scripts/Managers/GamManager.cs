using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamManager : Photon.MonoBehaviour {
    
    [SerializeField] private PhoManager Network_Manager;
    [SerializeField] private EntityManager entity_manager;
    private bool Gameready;
    public int difficulty;
    // Use this for initialization
    void Start () {
        Network_Manager.AutoConnect("test");
        
	}
    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            if (Network_Manager.NetworkStatus())
            {
                entity_manager.SpawnWave(difficulty);
            }
        }   
    }



}
