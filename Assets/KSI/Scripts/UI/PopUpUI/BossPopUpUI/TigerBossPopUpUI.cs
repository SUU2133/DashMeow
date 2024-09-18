using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class TigerBossPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["TigerBOSSEntranceButton"].onClick.AddListener(() => { LoadBOSScene(); });
		buttons["TigerBOSSPopUpBackButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
	}

	public void LoadBOSScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("BOSSSceneTiger");
		GameManager.Scene.LoadBOSS();
		Time.timeScale = 1f;
	}
}
