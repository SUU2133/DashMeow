using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class PausePopUpUI : PopUpUI
{

	protected override void Awake()
	{
		base.Awake();

		buttons["PausePopUpUIResumeButton"].onClick.AddListener(() => { ResumeGame(); });
		buttons["PausePopUpUIQuitButton"].onClick.AddListener(() => { QuitGame(); });
	}

	public void ResumeGame()
	{
		GameManager.UI.ClosePopUpUI();		
		Time.timeScale = 1f;
	}

	public void QuitGame()
	{
		GameManager.UI.ClearPopUpUI();
		Time.timeScale = 1f;
		UnitySceneManager.LoadScene("LobbyScene");
	}
}
