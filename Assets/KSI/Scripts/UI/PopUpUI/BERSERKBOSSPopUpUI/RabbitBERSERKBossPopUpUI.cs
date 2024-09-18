using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class RabbitBERSERKBossPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["RabbitBERSERKBossEntranceButton"].onClick.AddListener(() => { LoadBERSERKBOSScene(); });
		buttons["RabbitBERSERKBossPopUpBackButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
	}

	public void LoadBERSERKBOSScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("BERSERKBOSSSceneRabbit");
		GameManager.Scene.LoadBERSERKBOSS();
		Time.timeScale = 1f;
	}
}
