using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class TigerStoryModeSelectUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["TigerSUBButton"].onClick.AddListener(() => { LoadSUBScene(); });
		buttons["TigerBOSSButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/BossPopUpUI/TigerBossPopUpUI"); });
		buttons["TigerBERSERKBOSSButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/BERSERKBossPopUpUI/TigerBERSERKBossPopUpUI"); });
		buttons["TigerStoryModeSelectBackButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
	}

	public void LoadSUBScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("SUBSceneTiger");
		GameManager.Scene.LoadSUB();
		Time.timeScale = 1f;	
	}
}
