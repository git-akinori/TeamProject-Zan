using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class WaveData
{
	int waveNum;
	[SerializeField]
	int enemyID;
	[SerializeField]
	string enemyName;
	[SerializeField]
	float startTime;
	[SerializeField]
	float respawnTime;
	[SerializeField]
	int spawnLimit;

	public int WaveNum { get { return waveNum; } set { waveNum = value; } }
	public int EnemyID { get { return enemyID; } }
	public string EnemyName { get { return enemyName; } }
	public float StartTime { get { return startTime; } }
	public float RespawnTime { get { return respawnTime; } }
	public int SpawnLimit { get { return spawnLimit; } }
}

[CustomEditor(typeof(WaveData))]
class WaveInspector : Editor
{
	WaveData waveData = null;

	void OnEnable()
	{
		
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.LabelField("WaveNum", waveData.WaveNum.ToString());

		base.OnInspectorGUI();
	}
}
