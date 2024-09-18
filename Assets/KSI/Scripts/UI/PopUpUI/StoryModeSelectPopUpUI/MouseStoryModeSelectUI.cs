using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class MouseStoryModeSelectUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["MouseSUBButton"].onClick.AddListener(() => { LoadSUBScene(); });
		buttons["MouseBOSSButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/BossPopUpUI/MouseBossPopUpUI"); });
		//buttons["MouseBERSERKBOSSButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/BERSERKBossPopUpUI/MouseBERSERKBossPopUpUI"); });
		buttons["MouseStoryModeSelectBackButton"].onClick.AddListener(() => { BackButton(); });
	}

	public void LoadSUBScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("SUBSceneMouse");
		GameManager.Scene.LoadSUB();
		Time.timeScale = 1f;	
	}

	public void BackButton()
	{
		GameManager.UI.ClearPopUpUI();
		GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeEntranceUI");
		Time.timeScale = 0f;
	}
}
