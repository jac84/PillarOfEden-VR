using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour {

	[SerializeField] private EnemyManager enemyManager;
    //List of enemies avaiable to spawn.
	[SerializeField]List<string> Enemylist;
    Random randspawn = new Random();
   

    public IEnumerator SpawnWave(int difficulty, float spawnWait, float startWait, float waveWait, bool Network_Status)
    {
        int waveamount=0;
        yield return new WaitForSeconds(startWait);
        while (Network_Status)
        {
            for (int i = 0; i < difficulty; i++)
            {
                enemyManager.SpawnEnemy(Enemylist[Random.Range(0, Enemylist.Count)]);
                yield return new WaitForSeconds(spawnWait);
            }
            if (difficulty <= waveamount)
            {
                yield break;
            }
            else
            {
                yield return new WaitForSeconds(waveWait);
                waveamount++;
            }
        }
    }


}
