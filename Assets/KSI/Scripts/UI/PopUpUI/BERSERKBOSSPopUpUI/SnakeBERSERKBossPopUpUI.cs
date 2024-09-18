using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SnakeBERSERKBossPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["SnakeBERSERKBossEntranceButton"].onClick.AddListener(() => { LoadBERSERKBOSScene(); });
		buttons["SnakeBERSERKBossPopUpBackButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
	}

	public void LoadBERSERKBOSScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("BERSERKBOSSSceneSnake");
		GameManager.Scene.LoadBERSERKBOSS();
		Time.timeScale = 1f;
	}
}
