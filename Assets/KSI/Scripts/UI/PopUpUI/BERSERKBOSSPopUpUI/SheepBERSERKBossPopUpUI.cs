using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SheepBERSERKBossPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["SheepBERSERKBossEntranceButton"].onClick.AddListener(() => { LoadBERSERKBOSScene(); });
		buttons["SheepBERSERKBossPopUpBackButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
	}

	public void LoadBERSERKBOSScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("BERSERKBOSSSceneSheep");
		GameManager.Scene.LoadBERSERKBOSS();
		Time.timeScale = 1f;
	}
}
