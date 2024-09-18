using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class DogBERSERKBossPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["DogBERSERKBossEntranceButton"].onClick.AddListener(() => { LoadBERSERKBOSScene(); });
		buttons["DogBERSERKBossPopUpBackButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
	}

	public void LoadBERSERKBOSScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("BERSERKBOSSSceneDog");
		GameManager.Scene.LoadBERSERKBOSS();
		Time.timeScale = 1f;
	}
}
