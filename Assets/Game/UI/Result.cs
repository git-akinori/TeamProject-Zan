using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{

	[SerializeField]
	GameObject ResultUI;

	// 起動時オフ
	void Start()
	{
		ResultUI.SetActive(false);
	}

	// リザルト画面に入る処理
	public void EnterResult(bool win)
	{
		Time.timeScale = 0;
		ResultUI.SetActive(true);

		if (win) { Debug.Log("WIN!"); }
		else { Debug.Log("LOSE!"); }
	}

}
