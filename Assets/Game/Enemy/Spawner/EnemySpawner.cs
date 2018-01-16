using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField]
	GameObject slime;
	[SerializeField]
	GameObject skelton;
	[SerializeField]
	GameObject ork;

	[SerializeField]
	Vector3 offset_slime;
	[SerializeField]
	Vector3 offset_skelton;
	[SerializeField]
	Vector3 offset_ork;

	[SerializeField]
	GameObject boss_slime;
	[SerializeField]
	GameObject boss_skelton;
	[SerializeField]
	GameObject boss_ork;

	[SerializeField]
	Vector3 offset_boss_slime;
	[SerializeField]
	Vector3 offset_boss_skelton;
	[SerializeField]
	Vector3 offset_boss_ork;

	int cur_waveNum = 0;

	int total_spawnLimit = 0;

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

		for (int i = 0; i < 3; ++i) { waves[i] = new List<EnemySpawnData>(); }

		foreach (var strings in waveDataList)
		{
			GameObject obj = null;
			Vector3 offset = Vector3.zero;

			// ザコ
			// ID : 101 : スライム
			// ID : 102 : ゴブリン
			// ID : 103 : オーク
			// ボス
			// ID : 1001 : スライム
			// ID : 1002 : ゴブリン
			// ID : 1003 : オーク
			if (strings[1] == "101") { obj = slime; offset = offset_slime; }
			else if (strings[1] == "102") { obj = skelton; offset = offset_skelton; }
			else if (strings[1] == "103") { obj = ork; offset = offset_ork; }
			else if (strings[1] == "1001") { obj = boss_slime; offset = offset_boss_slime; }
			else if (strings[1] == "1002") { obj = boss_skelton; offset = offset_boss_skelton; }
			else if (strings[1] == "1003") { obj = boss_ork; offset = offset_boss_ork; }

			// スポーンデータに各要素を格納
			EnemySpawnData esd = new EnemySpawnData(
				obj,                        // enemyObj
				offset,                     // position
				float.Parse(strings[3]),    // startTime
				float.Parse(strings[4]),    // respawnTime
				int.Parse(strings[5]),      // spawnLimit
				transform);                 // parentTransform

			// 対応するwaveにスポーンデータ追加
			if (strings[0] == "1") waves[0].Add(esd);
			else if (strings[0] == "2") waves[1].Add(esd);
			else if (strings[0] == "3") waves[2].Add(esd);
		}

		total_spawnLimit = 0;
		foreach (var esd in waves[cur_waveNum])
		{
			total_spawnLimit += esd.SpawnLimit;
		}
	}

	void FixedUpdate()
	{
		// 通常時
		if (!debugMode)
		{
			int total_spawnedNum = 0;
			int total_aliveNum = 0;
			foreach (var esd in waves[cur_waveNum])
			{
				esd.Update();
				total_spawnedNum += esd.SpawnedNum;
				total_aliveNum += esd.AliveNum;
			}

			Debug.Log(total_aliveNum);

			if (total_spawnedNum >= total_spawnLimit && total_aliveNum == 0)
			{
				cur_waveNum++;
				Debug.Log("End Wave " + cur_waveNum + "!");

				if (cur_waveNum < 3)
				{
					total_spawnLimit = 0;
					foreach (var esd in waves[cur_waveNum])
					{
						total_spawnLimit += esd.SpawnLimit;
					}
				}
				else
				{
					GameManager.Member.EnterResult(true); // WIN
				}
			}

		}
		// デバッグモード時
		else
		{
			if (debugSpawnTime > 0)
			{
				elapsedTime += Time.deltaTime;
				if (elapsedTime > debugSpawnTime)
				{
					elapsedTime = 0;
					SpawnEnemy();
				}
			}
			if (spawnOnce)
			{
				spawnOnce = false;
				debugSpawnTime = 0;
				SpawnEnemy();
			}
		}
	}

	// for debug ---------------------------------------------------
	enum EnumEnemy
	{
		SLIME,
		SKELTON,
		ORK,
		BOSS_SLIME,
		BOSS_SKELTON,
		BOSS_ORK,
	}

	[SerializeField]
	EnumEnemy EnemyName;

	float elapsedTime = 0;
	[SerializeField]
	float debugSpawnTime = 1;

	[SerializeField]
	bool spawnOnce = false;

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
			case EnumEnemy.SKELTON:
				enemy = skelton;
				offset = offset_skelton;
				break;
			case EnumEnemy.ORK:
				enemy = ork;
				offset = offset_ork;
				break;

			case EnumEnemy.BOSS_SLIME:
				enemy = boss_slime;
				offset = offset_boss_slime;
				break;
			case EnumEnemy.BOSS_SKELTON:
				enemy = boss_skelton;
				offset = offset_boss_skelton;
				break;
			case EnumEnemy.BOSS_ORK:
				enemy = boss_ork;
				offset = offset_boss_ork;
				break;
		}
		if (enemy != null)
			Instantiate(enemy, transform).transform.position += offset;
	}
}