using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SheepStoryModeSelectPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["SheepSUBButton"].onClick.AddListener(() => { LoadSUBScene(); });
		buttons["SheepBOSSButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/BossPopUpUI/SheepBossPopUpUI"); });
		buttons["SheepBERSERKBOSSButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/BERSERKBossPopUpUI/SheepBERSERKBossPopUpUI"); });
		buttons["SheepStoryModeSelectBackButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
	}

	public void LoadSUBScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("SUBSceneSheep");
		GameManager.Scene.LoadSUB();
		Time.timeScale = 1f;	
	}
}
