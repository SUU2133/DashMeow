using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class PigBossPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["PigBOSSEntranceButton"].onClick.AddListener(() => { LoadBOSScene(); });
		buttons["PigBOSSPopUpBackButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
	}

	public void LoadBOSScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("BOSSScenePig");
		GameManager.Scene.LoadBOSS();
		Time.timeScale = 1f;
	}
}
