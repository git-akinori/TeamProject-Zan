using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField]
	GameObject slime;
	[SerializeField]
	GameObject goblin;
	[SerializeField]
	GameObject ork;

	[SerializeField]
	Vector3 offset_slime;
	[SerializeField]
	Vector3 offset_goblin;
	[SerializeField]
	Vector3 offset_ork;

	[SerializeField]
	float respawnTime = 3;
	int cur_waveNum = 0;

	[SerializeField]
	bool debugMode = false;

	// wave毎に敵のスポーンデータリストを用意
	List<EnemySpawnData>[] waves = new List<EnemySpawnData>[3];

	void Start()
	{
		// string[0] : waveNum
		// string[1] : enemyID
		// string[2] : enemyName
		// string[3] : startTime
		// string[4] : respawnTime
		// string[5] : spawnLimit
		var waveDataList = GameManager.Member.WaveDataList;
		//Debug.LogError(waveDataList[0][0]);

		for (int i = 0; i < 3; ++i) { waves[i] = new List<EnemySpawnData>(); }

		foreach (var strings in waveDataList)
		{
			GameObject obj = null;
			Vector3 offset = Vector3.zero;

			// ID : 100 : スライム
			// ID : 200 : ゴブリン
			// ID : 300 : オーク
			if (strings[1] == "1") { obj = slime; offset = offset_slime; }
			else if (strings[1] == "2") { obj = goblin; offset = offset_goblin; }
			else if (strings[1] == "3") { obj = ork; offset = offset_ork; }
			//else Debug.LogError("EnemyObject is null");

			// スポーンデータに各要素を格納
			EnemySpawnData esd = new EnemySpawnData(
				obj,
				offset,
				float.Parse(strings[3]), // startTime
				float.Parse(strings[4]), // respawnTime
				int.Parse(strings[5]), // spawnLimit
				transform);

			// 対応するwaveにスポーンデータ追加
			if (strings[0] == "1") waves[0].Add(esd);
			else if (strings[0] == "2") waves[1].Add(esd);
			else if (strings[0] == "3") waves[2].Add(esd);
		}
	}

	void Update()
	{
		if (debugMode)
		{
			elapsedTime += Time.deltaTime;
			if (elapsedTime > debugSpawnTime && debugSpawnTime != 0)
			{
				elapsedTime = 0;
				SpawnEnemy();
			}
		}
		else
		{
			int endWaveNum = 0;
			foreach (var wave in waves[cur_waveNum])
			{
				wave.Update();
				if(wave.EndWave && wave.EnemyCount == 0)
				{
					endWaveNum += 1;
				}
					//Debug.LogError("endwavenum" + endWaveNum);
			}
			if(endWaveNum == 2)
			{
				cur_waveNum++;
			}
		}
	}

	// for debug -------------------------------------
	enum EnumEnemy
	{
		SLIME,
		GOBLIN,
		ORK,
	}

	[SerializeField]
	EnumEnemy EnemyName;

	float elapsedTime = 0;
	[SerializeField]
	float debugSpawnTime = 1;

	void SpawnEnemy()
	{
		GameObject enemy = null;
		Vector3 offset = Vector3.zero;

		switch (EnemyName)
		{
			case EnumEnemy.SLIME:
				enemy = slime;
				offset = offset_slime;
				break;
			case EnumEnemy.GOBLIN:
				enemy = goblin;
				offset = offset_goblin;
				break;
			case EnumEnemy.ORK:
				enemy = ork;
				offset = offset_ork;
				break;
		}
		if (enemy != null)
			Instantiate(enemy, transform).transform.position += offset;
	}
}
