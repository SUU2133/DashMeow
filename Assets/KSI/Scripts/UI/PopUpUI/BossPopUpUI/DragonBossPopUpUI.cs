using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class DragonBossPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["DragonBOSSEntranceButton"].onClick.AddListener(() => { LoadBOSScene(); });
		buttons["DragonBOSSPopUpBackButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
	}

	public void LoadBOSScene()
	{
		GameManager.UI.ClosePopUpUI();
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("BOSSSceneDragon");
		Time.timeScale = 1f;
	}
}
