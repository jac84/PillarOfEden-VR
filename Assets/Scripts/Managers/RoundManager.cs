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
    private List<IEnumerator> spawnCoroutines = new List<IEnumerator>();
    private IEnumerator roundStartCoroutine;

    public int spawnPointsFinished = 0;
    int currentRound = 0;
    int currentWave = 0;
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
        currentRound = 1;
    }
    private void Update()
    {
        if(roundStart)
        {
            //Condition to Spawn next wave
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
                        roundStartCoroutine = StartRound(currentRound);
                        StartCoroutine(StartRound(currentRound));
                    }
                    else
                        roundStart = false;
                }
            }
        }
    }
    public void StartSpawn()
    {
        if (!roundStart)
        {
            roundStartCoroutine = StartRound(1);
            StartCoroutine(StartRound(1));
        }
    }
    public void RestartRoundManager()
    {
        StopCoroutine(roundStartCoroutine);
        foreach(IEnumerator e in spawnCoroutines)
        {
            StopCoroutine(e);
        }
        spawnCoroutines.Clear();
        roundStart = false;
        currentRound = 1;
        spawnPointsFinished = 0;
        currentRound = 0;
        currentWave = 0;
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
        spawnCoroutines.Clear();
        int i = 0;
        GamManager.singleton.GetEnemyManager().EnemyCleanup();
        Debug.Log("Wave " + currentWave + ":  Spawned!");
        foreach (Transform t in spawnPoints)
        {
            spawnCoroutines.Add(t.GetChild(currentRound - 1).GetChild(currentWave - 1).GetComponent<Wave>().SpawnWave(groupThreshold, waitBeforeSpawningNextThreshold));
            StartCoroutine(spawnCoroutines[i]);
            i++;
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
