using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class MonkeyStoryModeSelectPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["MonkeySUBButton"].onClick.AddListener(() => { LoadSUBScene(); });
		buttons["MonkeyBOSSButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/BossPopUpUI/MonkeyBossPopUpUI"); });
		//buttons["MonkeyBERSERKBOSSButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/BERSERKBossPopUpUI/MonkeyBERSERKBossPopUpUI"); });
		buttons["MonkeyStoryModeSelectBackButton"].onClick.AddListener(() => { BackButton(); });
	}

	public void LoadSUBScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("SUBSceneMonkey");
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
