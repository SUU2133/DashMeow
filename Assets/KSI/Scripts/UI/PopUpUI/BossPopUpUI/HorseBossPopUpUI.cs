using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class HorseBossPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["HorseBOSSEntranceButton"].onClick.AddListener(() => { LoadBOSScene(); });
		buttons["HorseBOSSPopUpBackButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
	}

	public void LoadBOSScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("BOSSSceneHorse");
		GameManager.Scene.LoadBOSS();
		Time.timeScale = 1f;
	}
}
