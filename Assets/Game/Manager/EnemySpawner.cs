using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField]
    GameObject enemy;
    List<GameObject> enemyList = new List<GameObject>();

    [SerializeField]
    Vector3 offsetPos;

    void Start()
    {
        SpawnEnemy();
    }

    void Update()
    {

    }



    // ポンポンスポポーン
    void SpawnEnemy()
    {
        var obj = Instantiate(enemy, transform);
        obj.transform.position += offsetPos;
        enemyList.Add(obj);
    }
}
