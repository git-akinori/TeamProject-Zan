using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumEnemy
{
	SLIME,
	SKELETON,
	ORK,
	BOSS_SLIME,
	BOSS_SKELETON,
	BOSS_ORK,
}

public class EnemyStatus : MonoBehaviour
{
	[SerializeField]
	BaseEnemyStatus slime;
	[SerializeField]
	BaseEnemyStatus skeleton;
	[SerializeField]
	BaseEnemyStatus ork;

	[SerializeField]
	BaseEnemyStatus boss_slime;
	[SerializeField]
	BaseEnemyStatus boss_skeleton;
	[SerializeField]
	BaseEnemyStatus boss_ork;

	public BaseEnemyStatus Slime { get { return slime; } }
	public BaseEnemyStatus Skeleton { get { return skeleton; } }
	public BaseEnemyStatus Ork { get { return ork; } }
	public BaseEnemyStatus Boss_Slime { get { return boss_slime; } }
	public BaseEnemyStatus Boss_Skeleton { get { return boss_skeleton; } }
	public BaseEnemyStatus Boss_Ork { get { return boss_ork; } }
}
