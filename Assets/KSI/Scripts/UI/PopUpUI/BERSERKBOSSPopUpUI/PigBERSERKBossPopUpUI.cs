using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class PigBERSERKBossPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["PigBERSERKBossEntranceButton"].onClick.AddListener(() => { LoadBERSERKBOSScene(); });
		buttons["PigBERSERKBossPopUpBackButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
	}

	public void LoadBERSERKBOSScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("BERSERKBOSSScenePig");
		GameManager.Scene.LoadBERSERKBOSS();
		Time.timeScale = 1f;
	}
}
