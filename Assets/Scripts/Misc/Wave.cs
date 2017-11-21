using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : MonoBehaviour
{
    public List<string> Monsters;
    private int currentIndex = 0;

    public IEnumerator SpawnWave(int threshold, float waitBeforeSpawningNextThreshold)
    {
        if (threshold == 0 || Monsters.Count == 0)
            yield break;
        for (int i = currentIndex; i < Monsters.Count; i += threshold)
        {
            for (int j = 0; j < threshold; j++)
            {
                if (j + currentIndex < Monsters.Count)
                {
                    Vector3 position;
                    Transform spwnPoint = transform.parent;
                    position = new Vector3(Random.Range(spwnPoint.transform.position.x - (spwnPoint.lossyScale.x / 2), spwnPoint.transform.position.x + (spwnPoint.lossyScale.x / 2)),  // Randomize spawnpoint within spawnpoint
                    spwnPoint.transform.position.y,
                    Random.Range(spwnPoint.transform.position.z - (spwnPoint.lossyScale.z / 2), spwnPoint.transform.position.z + (spwnPoint.lossyScale.z / 2)));
                    GamManager.singleton.GetEnemyManager().SpawnEnemy(Monsters[j + currentIndex], position);
                }
            }
            currentIndex += threshold;
            if (currentIndex < Monsters.Count)
            {
                yield return new WaitForSeconds(waitBeforeSpawningNextThreshold);
            }
            else
            {
                GamManager.singleton.GetRoundManager().IncrementSpawnFinished();
            }
        }
    }
}
