using System.Collections;
using Pathfinding;
using Leap.Unity;
using UnityEngine.PostProcessing;
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
    public GameObject poolManager;
    [SerializeField]
    public Transform playerStartPosition;
    [SerializeField]
    private LeapProvider leapServiceProvider;

    public VRTK.VRTK_SDKManager VRTKManager;
    [SerializeField] private EntityManager entity_manager;
    [SerializeField] private GameObject AStarGrid;
    [SerializeField] private Pillar pillar;
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
        var behaviour = mainVRCamera.GetComponent<PostProcessingBehaviour>();
        if (behaviour.profile != null)
        {
            behaviour.profile.colorGrading.enabled = false;
        }
        if (Network_Manager != null)
            Network_Manager.AutoConnect("test");

    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            roundmanager.StartSpawn();
        }
        else if(Input.GetKeyUp(KeyCode.R))
        {
            RestartGame();
        }
    }
    public bool IsLeftHanded()
    {
        return LeftHanded;
    }
    //cleanup coroutines,kicks everyone off network.
    public void GameOver()
    {
        var behaviour = mainVRCamera.GetComponent<PostProcessingBehaviour>();
        if (behaviour.profile != null)
        {
            behaviour.profile.colorGrading.enabled = true;
        }
        StopAllCoroutines();
        enemymanager.EnemyCleanup();
        towerManager.TowerCleanUp();
        roundmanager.RestartRoundManager();
        PhotonNetwork.CloseConnection(PhotonNetwork.player);
        PhotonNetwork.Disconnect();
    }
    public void RestartGame()
    {
        ClearPool();
        PhotonNetwork.Reconnect();
        var behaviour = mainVRCamera.GetComponent<PostProcessingBehaviour>();
        if (behaviour.profile != null)
        {
            behaviour.profile.colorGrading.enabled = false;
        }
        roundmanager.RestartRoundManager();
        enemymanager.EnemyCleanup();
        towerManager.TowerCleanUp();

        player.ResetPlayer();
        player.transform.parent.position = playerStartPosition.position;
        pillar.ResetPillar();
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
    public GameObject GetAStarGrid()
    {
        return AStarGrid;
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
    public Pillar GetPillar()
    {
        return pillar;
    }
    public LeapProvider GetLeapServiceProvider()
    {
        return leapServiceProvider;
    }
    public void SetLeapServiceProvider(LeapProvider s)
    {
        leapServiceProvider = s;
    }
    public void ClearPool()
    {
        foreach(Transform o in poolManager.transform.GetChildren())
        {
            Destroy(o.gameObject);
        }
    }
}
