using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class MouseBossPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["MouseBOSSEntranceButton"].onClick.AddListener(() => { LoadBOSScene(); });
		buttons["MouseBOSSPopUpBackButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
	}

	public void LoadBOSScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("BOSSSceneMouse");
		GameManager.Scene.LoadBOSS();
		Time.timeScale = 1f;
	}
}
