using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SnakeBossPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["SnakeBOSSEntranceButton"].onClick.AddListener(() => { LoadBOSScene(); });
		buttons["SnakeBOSSPopUpBackButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
	}

	public void LoadBOSScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("BOSSSceneSnake");
		GameManager.Scene.LoadBOSS();
		Time.timeScale = 1f;
	}
}
