using System.Collections;
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
    private TowerManager towerManager;
    [SerializeField]
    private EnemyManager enemymanager;
    [SerializeField]
    private RoundManager roundmanager;
    private bool Gameready;

    

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
    //cleanup coroutines,kicks everyone off network.
    void GameOver()
    {
        StopAllCoroutines();
        PhotonNetwork.CloseConnection(PhotonNetwork.player);
        PhotonNetwork.Disconnect();
    }

    //This here is a test. When running this function please use 
    //gamestart(Network_Manager.NetworkStatus(),difficulty) It should return a proper network status.
    void GameStart(bool NetworkState, int difficulty)
    {
        if (!NetworkState)
        {
            Debug.Log("Network state isn't ready");
            return;
        }
        //such next gen.
    }
    public virtual void OnJoinedRoom()
    {

    }
    public TowerManager GetTowerManager()
    {
        return towerManager;
    }
    public void SetTowerManager(TowerManager tw)
    {
        towerManager = tw;
    }
    public EnemyManager GetEnemyManager()
    {
        return enemymanager;
    }
    public RoundManager GetRoundManager()
    {
        return roundmanager;
    }
    public VRPlayer GetVRPlayer()
    {
        return player;
    }
}
