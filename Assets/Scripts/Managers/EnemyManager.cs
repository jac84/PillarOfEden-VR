using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private List<IEnemy> enemies;
    [SerializeField] private GameObject lastEnemyToBeSpawned = null;
    public List<BoxCollider> enemySpawnPoints;

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
        foreach (IEnemy enemy in enemies)
        {
            enemy.UpdateEnemyMovement();
        }
    }
    /**
    * @brief Spawn Specified Enemy
     */
    public void SpawnEnemy(GameObject enemy)
    {
        IEnemy eCom = null;
        GameObject e = null;
        Vector3 position;
        BoxCollider spwnPoint = null;

        int spwnIndex = Random.Range(0, enemySpawnPoints.Count);         // Pick Random Spawn Point
        spwnPoint = enemySpawnPoints[spwnIndex];
        position = new Vector3(Random.Range(-spwnPoint.transform.position.x - (spwnPoint.size.x / 2), spwnPoint.transform.position.x + (spwnPoint.size.x / 2)),  // Randomize spawnpoint within spawnpoint
        spwnPoint.transform.position.y,
        Random.Range(spwnPoint.transform.position.z - (spwnPoint.size.z / 2), spwnPoint.transform.position.z + (spwnPoint.size.z / 2)));
        Debug.Log(position);
        //Remember to replace Instatiate with Photons Instatiate method!!!!!
        e = Instantiate(enemy, position, Quaternion.identity);
        if (e == null)
        {
            Debug.Log("Failed to initiate enemy");
            return;
        }
        eCom = e.GetComponent<IEnemy>();
        enemies.Add(eCom);
        lastEnemyToBeSpawned = e;
    }
    /**
    * @brief Despawn Specified Enemy
     */
    public void DespawnEnemy(GameObject enemy)
    {
        IEnemy foundEnemy = null;
        if (enemy != null)
        {
            foundEnemy = enemies.Find(e => e == enemy.GetComponent<IEnemy>());
            if (foundEnemy != null)
            {
                Debug.Log("Despawn Enemy Failed: Could not find enemy or list is empty");
                enemies.Remove(foundEnemy);
                Destroy(foundEnemy.gameObject);
                if (enemies.Count > 0)
                    lastEnemyToBeSpawned = enemies[enemies.Count - 1].gameObject;
            }
        }else{
            Debug.Log("Can not delete enemy specified as NULL");
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
}
