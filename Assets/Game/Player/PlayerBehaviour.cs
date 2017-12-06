using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	[SerializeField]
	float maxLIFE = 100;
	[SerializeField]
	float maxSKILL = 100;

	Gauge life;
	Gauge skill;

	void Start()
	{
		life = new Gauge(maxLIFE, 1);
		skill = new Gauge(maxSKILL, 0);
	}

	void FixedUpdate()
	{
		life.Add(-0.1f);
		skill.Add(0.1f);
	}

	class Gauge
	{
		float max;
		float cur;

		public Gauge(float max, float initRatio)
		{
			this.max = max;
			cur = max * initRatio;
			cur = (cur < 0) ? 0
				: (cur > max) ? max
				: cur;
		}

		public void Add(float value)
		{
			cur += value;
			cur = (cur < 0) ? 0
				: (cur > max) ? max
				: cur;
		}

		public float Ratio { get { return cur / max; } }
	}

	// ダメージを与える処理で呼び出す
	public void Damaged(float value) { life.Add(-value); }

	// 敵を倒したときの処理で呼び出す
	public void AddSkillPoint(float value) { skill.Add(value); }

	// LIFEの割合を得たいときに呼び出す
	public float LifeRatio { get { return life.Ratio; } }
	// SKILLの割合を得たいときに呼び出す
	public float SkillRatio { get { return skill.Ratio; } }
}
