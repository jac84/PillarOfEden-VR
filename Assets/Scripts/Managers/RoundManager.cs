using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RoundManager : MonoBehaviour
{
    [Header("Modifiers for waves and rounds")]
    [Tooltip("Must equal the MAXIMUM amount of children waves in a Round GameObject")]
    [SerializeField] private int maxWaves;
    [Tooltip("Must equal the MAXIMUM number of children rounds in a Spawnpoint GameObject")]
    [SerializeField] private int maxRounds;
    [Tooltip("Amount of seconds to wait before each round begins")]
    [SerializeField] private float roundGracePeriodSeconds;
    [Tooltip("The number of enemies to spawn per group in a wave")]
    [SerializeField] private int groupThreshold;
    [Tooltip("Amount of seconds to wait before spawning the next group in a wave")]
    [SerializeField] private float waitBeforeSpawningNextThreshold;

    private List<Transform> spawnPoints = new List<Transform>();

    public int spawnPointsFinished = 0;
    int currentRound = 0;
    int currentWave = 0;
    private int counter;
    private bool roundStart;
    public void Awake()
    {
        foreach (Transform t in transform)
        {
            spawnPoints.Add(t);
        }
    }
    private void Start()
    {
        roundStart = false;
        StartCoroutine(StartRound(1));
    }
    private void Update()
    {
        if(roundStart)
        {
            //Condition to Spawn next wave
            if(Input.GetKeyUp(KeyCode.Space))
            {
                GamManager.singleton.GetEnemyManager().DespawnEnemy(GamManager.singleton.GetEnemyManager().GetLastEnemySpawned());
            }
            if(spawnPointsFinished >= spawnPoints.Count && GamManager.singleton.GetEnemyManager().GetEnemyCount() <= 0)
            {
                if (currentWave < maxWaves)
                {
                    currentWave++;
                    SpawnWave(currentWave);
                }
                else
                {
                    if(currentRound < maxRounds)
                    {
                        currentRound++;
                        roundStart = false;
                        StartCoroutine(StartRound(currentRound));
                    }
                    else
                        roundStart = false;
                }
            }
        }
    }
    // Update is called once per frame
    IEnumerator StartRound(int round)
    {
        currentRound = round;
        currentWave = 1;
        Debug.Log("Round " + currentRound + ":  Starting...");
        yield return new WaitForSeconds(roundGracePeriodSeconds);
        Debug.Log("Round " + currentRound + ":  Start!");
        SpawnWave(currentWave);
        roundStart = true;
    }
    public void SpawnWave(int waveNumber)
    {
        spawnPointsFinished = 0;
        GamManager.singleton.GetEnemyManager().EnemyCleanup();
        Debug.Log("Wave " + currentWave + ":  Spawned!");
        foreach (Transform t in spawnPoints)
        {
            StartCoroutine(t.GetChild(currentRound - 1).GetChild(currentWave - 1).GetComponent<Wave>().SpawnWave(groupThreshold, waitBeforeSpawningNextThreshold));
        }
    }
    public void StopRound()
    {
        roundStart = false;
    }
    public void IncrementSpawnFinished()
    {
        spawnPointsFinished++;
    }
}
