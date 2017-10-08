using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{

    [SerializeField] private EnemyManager enemyManager;

    [SerializeField] private UpdateHPBeads hpBracelet;

    public GameObject temp;
    public float total = 100.0f;
    public float current = 100.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (current > 0)
        {
            current -= 1.0f;
            hpBracelet.UpdateBeads(current, total);

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //enemyManager.SpawnEnemy(temp);
            current -= 5;
            hpBracelet.UpdateBeads(current, total);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            enemyManager.DespawnEnemy(enemyManager.GetLastEnemySpawned());
        }
    }
}
