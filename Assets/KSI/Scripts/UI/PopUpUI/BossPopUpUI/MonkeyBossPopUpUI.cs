using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class MonkeyBossPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["MonkeyBOSSEntranceButton"].onClick.AddListener(() => { LoadBOSScene(); });
		buttons["MonkeyBOSSPopUpBackButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
	}

	public void LoadBOSScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("BOSSSceneMonkey");
		GameManager.Scene.LoadBOSS();
		Time.timeScale = 1f;
	}
}
