using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour {

	[SerializeField] private EnemyManager enemyManager;

	public GameObject temp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			enemyManager.SpawnEnemy(temp);
		}
		if(Input.GetKeyDown(KeyCode.D))
		{
			enemyManager.DespawnEnemy(enemyManager.GetLastEnemySpawned());
		}	
	}
}
