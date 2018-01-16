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
	int spawnedNum = 0;
	int aliveNum = 0;

	// スポーン中の敵リスト
	List<GameObject> enemyList = new List<GameObject>();

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
			start = true;
			elapsedTime = 0;
			Spawn();
		}
		else if (start && elapsedTime > respawnTime && spawnedNum < spawnLimit)
		{
			elapsedTime = 0;
			Spawn();
		}

		int deadNum = 0;
		foreach (var enemy in enemyList)
		{
			if (enemy == null) deadNum++;
		}
		aliveNum = spawnedNum - deadNum;
	}

	private void Spawn()
	{
		if (enemyObj)
		{
			var obj = Object.Instantiate(enemyObj, transform);
			obj.transform.position += offset;
			enemyList.Add(obj);

			spawnedNum++;
		}
		else Debug.LogError("enemyObj is null");
	}

	public int SpawnedNum { get { return spawnedNum; } }
	public int SpawnLimit { get { return spawnLimit; } }
	public int AliveNum { get { return aliveNum; } }
}