using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
	[SerializeField]
	Image lifeGauge;
	[SerializeField]
	Image extraGauge;

	[SerializeField]
	GameObject player;
	PlayerBehaviour playerBehaviour;

    void Start()
    {
		playerBehaviour = player.GetComponent<PlayerBehaviour>();
		lifeGauge.fillAmount = playerBehaviour.LifeRatio;
		extraGauge.fillAmount = playerBehaviour.SkillRatio;
    }
	
    void FixedUpdate()
    {
		lifeGauge.fillAmount = playerBehaviour.LifeRatio;
		extraGauge.fillAmount = playerBehaviour.SkillRatio;
	}
}
