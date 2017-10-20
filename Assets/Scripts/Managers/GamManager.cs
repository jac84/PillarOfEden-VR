using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class GamManager : Photon.MonoBehaviour
{

    static GamManager instance = null;
    public Camera mainVRCamera;

    /**Settings */
    [SerializeField] private bool IsLeftHanded = false;
=======
public class GamManager : Photon.MonoBehaviour {
    
>>>>>>> b7b9f88c1211c5408fb64275625f9fed406fd918
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
