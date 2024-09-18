using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class CowBossPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["CowBOSSEntranceButton"].onClick.AddListener(() => { LoadBOSScene(); });
		buttons["CowBOSSPopUpBackButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
	}

	public void LoadBOSScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("BOSSSceneCow");
		GameManager.Scene.LoadBOSS();
		Time.timeScale = 1f;
	}
}
