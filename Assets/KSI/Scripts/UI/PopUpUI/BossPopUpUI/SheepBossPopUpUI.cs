using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SheepBossPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["SheepBOSSEntranceButton"].onClick.AddListener(() => { LoadBOSScene(); });
		buttons["SheepBOSSPopUpBackButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
	}

	public void LoadBOSScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("BOSSSceneSheep");
		GameManager.Scene.LoadBOSS();
		Time.timeScale = 1f;
	}
}
