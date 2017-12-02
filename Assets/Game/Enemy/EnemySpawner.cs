using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    GameObject enemy;
    List<GameObject> enemyList = new List<GameObject>();

    CsvReader csvReader = new CsvReader();
    const string stageCsvPath = "stage";

    void Start()
    {
        var stageData = csvReader.ReadCSV(stageCsvPath);
        SpawnEnemy();
    }

    void Update()
    {

    }

    // Spawn Enemy Function
    void SpawnEnemy()
    {
        var obj = Instantiate(enemy, transform);
        enemyList.Add(obj);
    }
}
