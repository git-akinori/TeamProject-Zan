using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EnemySpawnData
{
	GameObject enemyObj;
	Vector3 offset;
	float startTime;
	float respawnTime;
	int spawnLimit;
	Transform transform;

	bool start = false;
	float elapsedTime = 0;
	int spawnNum = 0;

	// スポーン中の敵リスト
	static List<GameObject> enemyList = new List<GameObject>();

	public EnemySpawnData(GameObject enemyObj, Vector3 offset, float startTime, float respawnTime, int spawnLimit, Transform transform)
	{
		this.enemyObj = enemyObj;
		this.offset = offset;
		this.startTime = startTime;
		this.respawnTime = respawnTime;
		this.spawnLimit = spawnLimit;
		this.transform = transform;
	}

	public void Update()
	{
		elapsedTime += Time.deltaTime;

		if (!start && elapsedTime > startTime)
		{
			Spawn();
			start = true;
			elapsedTime = 0;
		}

		if (start && elapsedTime > respawnTime && spawnNum < spawnLimit)
		{
			Spawn();
			elapsedTime = 0;
		}
	}

	private void Spawn()
	{
		var obj = Object.Instantiate(enemyObj, transform);
		obj.transform.position += offset;
		enemyList.Add(obj);

		++spawnNum;
	}

	public bool EndWave
	{
		get
		{
			return spawnNum >= spawnLimit;
		}
	}

	public int EnemyCount { get { return enemyList.Count; } }
}