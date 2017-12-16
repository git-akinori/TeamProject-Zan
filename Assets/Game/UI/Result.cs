using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour {

	[SerializeField]
	GameObject ResultUI;

	// 起動時オフ
	void Start () {
		ResultUI.SetActive(false);
	}

	// リザルト画面に入る処理
	public void EnterResult()
	{
		if (Time.timeScale != 0)
		{
			Time.timeScale = 0;
			ResultUI.SetActive(true);
		}
	}

	// リザルト画面からホームに戻る処理
	public void ExitResult()
	{
		if (Time.timeScale != 1.0f)
		{
			Time.timeScale = 1.0f;
			SceneManager.LoadScene("Home");
		}
	}
}
