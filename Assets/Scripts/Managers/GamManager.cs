using System.Collections;
using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class GamManager : Photon.MonoBehaviour
{

    static GamManager instance = null;
    public Camera mainVRCamera;
    public VRPlayer player;

    /**Settings */
    [SerializeField] private bool LeftHanded = false;
    [SerializeField] private PhoManager Network_Manager;
    [SerializeField] private EntityManager entity_manager;
    [SerializeField] private GameObject AStarGrid;
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
    void Start () {
        if(Network_Manager != null)
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

    public bool IsLeftHanded()
    {
        return LeftHanded;
    }
    public GameObject GetAStarGrid()
    {
        return AStarGrid;
    }


}
