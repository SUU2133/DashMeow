using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class DogBossPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["DogBOSSEntranceButton"].onClick.AddListener(() => { LoadBOSScene(); });
		buttons["DogBOSSPopUpBackButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
	}

	public void LoadBOSScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("BOSSSceneDog");
		GameManager.Scene.LoadBOSS();
		Time.timeScale = 1f;
	}
}
