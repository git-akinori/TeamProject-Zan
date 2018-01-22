using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseEnemyStatus
{
	[SerializeField, Range(0f, 100f)]
	float hp;
	[SerializeField, Range(0f, 100f)]
	float speed;
	[SerializeField, Range(0f, 100f)]
	float attack;
	[SerializeField, Range(0f, 100f)]
	float weight;
	[SerializeField, Range(0f, 100f)]
	float exPoint;

	public float HP { get { return hp; } set { hp = value; } }
	public float Speed { get { return speed; } }
	public float Attack { get { return attack; } }
	public float Weight { get { return weight; } }
	public float ExPoint { get { return exPoint; } }
}

