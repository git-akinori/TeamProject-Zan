using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
	[SerializeField]
	GameObject PauseUI;

	// ポーズ画面に入る処理
	public void EnterPause()
	{
		if (Time.timeScale == 1.0f)
		{
			Time.timeScale = 0;
			PauseUI.SetActive(true);
		}
	}

	// ポーズ画面からゲームに戻る処理
	public void ExitPause()
	{
		if (Time.timeScale != 1.0f)
		{
			Time.timeScale = 1.0f;
			PauseUI.SetActive(false);
		}
	}
}
