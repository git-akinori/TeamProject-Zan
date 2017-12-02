using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    GameObject enemy;

    List<GameObject> enemyList = new List<GameObject>();

	void Start () {
		
	}
	
	void Update () {
		
	}

    // Spawn Enemy Function
    void SpawnEnemy()
    {
        var obj = Instantiate(enemy, transform);
        enemyList.Add(obj);
    }
}
