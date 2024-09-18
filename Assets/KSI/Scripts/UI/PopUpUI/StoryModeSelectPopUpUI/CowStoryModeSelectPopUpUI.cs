using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class CowStoryModeSelectPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["CowSUBButton"].onClick.AddListener(() => { LoadSUBScene(); });
		buttons["CowBOSSButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/BossPopUpUI/CowBossPopUpUI"); });
		//buttons["CowBERSERKBOSSButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/BERSERKBossPopUpUI/CowBERSERKBossPopUpUI"); });
		buttons["CowStoryModeSelectBackButton"].onClick.AddListener(() => { BackButton(); });
	}

	public void LoadSUBScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("SUBSceneCow");
		GameManager.Scene.LoadSUB();
		Time.timeScale = 1f;	
	}

	public void BackButton()
	{
		GameManager.UI.ClearPopUpUI();
		GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeEntranceUI");
	}
}
