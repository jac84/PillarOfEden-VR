using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Photon.MonoBehaviour
{

    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private GameObject lastEnemyToBeSpawned = null;
    //[SerializeField] private List<string> Enemies;
    // Use this for initialization
    void Start()
    {

    }
    /*
    *   @brief Used to call all enemies 'update' functions that handle rest of logic
     */
    void Update()
    {

    }
    /**
    *   @brief Used to call all enemies 'Fixed Update' functions that handle physics
    */
    void FixedUpdate()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.UpdateEnemyMovement();
        }
    }
    /**
    * @brief Spawn Specified Enemy
     */
    public void SpawnEnemy(string enemy,Vector3 position)
    {
        Enemy eCom = null;
        GameObject e = null;
        //Remember to replace Instatiate with Photons Instatiate method!!!!!
        e = PhotonNetwork.Instantiate(enemy, position, Quaternion.identity,0);
        if (e == null)
        {
            Debug.Log("Failed to initiate enemy");
            return;
        }
        eCom = e.GetComponent<Enemy>();
        enemies.Add(eCom);
        lastEnemyToBeSpawned = e;
    }
    /**
    * @brief Despawn Specified Enemy
     */
    public void DespawnEnemy(GameObject enemy)
    {
        Enemy foundEnemy = null;
        if (enemy != null)
        {
            foundEnemy = enemies.Find(e => e == enemy.GetComponent<Enemy>());
            if (foundEnemy != null)
            {
               enemies.Remove(foundEnemy);
               PhotonNetwork.Destroy(foundEnemy.gameObject);
               if (enemies.Count > 0)
                   lastEnemyToBeSpawned = enemies[enemies.Count - 1].gameObject;
            }
            else
            {
                Debug.Log("Did not find enemy to despawn");
            }
        }else{
            Debug.Log("Can not delete enemy specified as NULL");
            Debug.Log("Despawn Enemy Failed: Could not find enemy or list is empty");
        }
    }
    /*
    *  @Brief Get last enemy spawned
     */
    public GameObject GetLastEnemySpawned()
    {
        if(lastEnemyToBeSpawned)
            return lastEnemyToBeSpawned;
        return null;
    }
    public void EnemyCleanup()
    {
        Debug.Log("Cleaned Up " + enemies.Count + " Enemies...");
        for (int i= enemies.Count-1; i >= 0; i--)
        {
            DespawnEnemy(enemies[i].gameObject);
        }
    }    
    public int GetEnemyCount()
    {
        return enemies.Count;
    }

}
