using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour {

	[SerializeField] private EnemyManager enemyManager;
    //List of enemies avaiable to spawn.
	[SerializeField]List<string> Enemylist;
    Random randspawn = new Random();

    public void SpawnWave(int difficulty)
    {
        for (int i = 0; i < difficulty; i++)
        {
            enemyManager.SpawnEnemy(Enemylist[Random.Range(0, Enemylist.Count)]);
        }
    }

}
