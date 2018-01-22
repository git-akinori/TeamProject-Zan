using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField]
	GameObject slime;
	[SerializeField]
	GameObject skeleton;
	[SerializeField]
	GameObject ork;

	[SerializeField]
	Vector3 offset_slime;
	[SerializeField]
	Vector3 offset_skeleton;
	[SerializeField]
	Vector3 offset_ork;

	[SerializeField]
	GameObject boss_slime;
	[SerializeField]
	GameObject boss_skeleton;
	[SerializeField]
	GameObject boss_ork;

	[SerializeField]
	Vector3 offset_boss_slime;
	[SerializeField]
	Vector3 offset_boss_skeleton;
	[SerializeField]
	Vector3 offset_boss_ork;

	int cur_waveNum = 0;

	int total_spawnLimit = 0;

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
			else if (strings[1] == "102") { obj = skeleton; offset = offset_skeleton; }
			else if (strings[1] == "103") { obj = ork; offset = offset_ork; }
			else if (strings[1] == "1001") { obj = boss_slime; offset = offset_boss_slime; }
			else if (strings[1] == "1002") { obj = boss_skeleton; offset = offset_boss_skeleton; }
			else if (strings[1] == "1003") { obj = boss_ork; offset = offset_boss_ork; }

			// スポーンデータに各要素を格納
			EnemySpawnData esd = new EnemySpawnData(
				obj,                        // enemyObj
				strings[2],					// enemyName
				offset,                     // position
				float.Parse(strings[3]),    // startTime
				float.Parse(strings[4]),    // respawnTime
				int.Parse(strings[5]),      // spawnLimit
				transform);                 // parentTransform

			// 対応するwaveにスポーンデータ追加
			waves[int.Parse(strings[0]) - 1].Add(esd);
		}

		// 現在のウェーブの最大スポーン数を格納
		total_spawnLimit = 0;
		foreach (var esd in waves[cur_waveNum])
		{
			total_spawnLimit += esd.SpawnLimit;
		}
	}

	bool wave_start = false;

	void FixedUpdate()
	{
		// 通常時
		if (!debugMode)
		{
			// ウェーブ開始までのカウントダウン処理
			if (!wave_start)
			{
				/*
				 
				 */
				wave_start = true;
			}
			// ウェーブ開始
			else
			{
				// 現在のウェーブの敵の種類ごとの処理
				int total_spawnedNum = 0;
				int total_aliveNum = 0;

				if (cur_waveNum < 3)
				{
					foreach (var esd in waves[cur_waveNum])
					{
						esd.Update();
						total_spawnedNum += esd.SpawnedNum;
						total_aliveNum += esd.AliveNum;
					}
				}

				// ウェーブクリア判定
				if (total_spawnedNum >= total_spawnLimit && total_aliveNum == 0)
				{
					cur_waveNum++;
					Debug.Log("End Wave " + cur_waveNum + "!");

					// 最大スポーン数を次ウェーブに切り替え
					if (cur_waveNum < 3)
					{
						total_spawnLimit = 0;
						foreach (var esd in waves[cur_waveNum])
						{
							total_spawnLimit += esd.SpawnLimit;
						}
					}
					// ステージクリア判定と処理
					else
					{
						GameManager.Member.EnterResult(true); // WIN
					}
				}
			}
		}
		// デバッグモード時
		else
		{
			// 常に1体だけ生存する
			if (OnlyOneEnemy)
			{
				if(debugSpawnTime != 0) debugSpawnTime = 0;
				if(!debugEnemyObj) SpawnEnemy();
			}
			// 一定間隔でスポーン
			if (debugSpawnTime > 0)
			{
				elapsedTime += Time.deltaTime;
				if (elapsedTime > debugSpawnTime)
				{
					elapsedTime = 0;
					SpawnEnemy();
				}
			}
		}
	}

	// for debug ---------------------------------------------------

	[SerializeField]
	bool debugMode = false;
	[SerializeField]
	bool OnlyOneEnemy = false;

	[SerializeField]
	EnumEnemy EnemyName;

	float elapsedTime = 0;

	[SerializeField]
	float debugSpawnTime = 1;

	GameObject debugEnemyObj;

	void SpawnEnemy()
	{
		GameObject enemy = null;
		string name = "";
		Vector3 offset = Vector3.zero;

		switch (EnemyName)
		{
			case EnumEnemy.SLIME:
				enemy = slime;
				name = "Slime";
				offset = offset_slime;
				break;
			case EnumEnemy.SKELETON:
				enemy = skeleton;
				name = "Skeleton";
				offset = offset_skeleton;
				break;
			case EnumEnemy.ORK:
				enemy = ork;
				name = "Ork";
				offset = offset_ork;
				break;

			case EnumEnemy.BOSS_SLIME:
				enemy = boss_slime;
				name = "Boss Slime";
				offset = offset_boss_slime;
				break;
			case EnumEnemy.BOSS_SKELETON:
				enemy = boss_skeleton;
				name = "Boss Skeleton";
				offset = offset_boss_skeleton;
				break;
			case EnumEnemy.BOSS_ORK:
				enemy = boss_ork;
				name = "Boss Ork";
				offset = offset_boss_ork;
				break;
		}

		if (enemy != null)
		{
			debugEnemyObj = Instantiate(enemy, transform);
			debugEnemyObj.transform.position += offset;
			debugEnemyObj.transform.name = name;
		}
	}
}
