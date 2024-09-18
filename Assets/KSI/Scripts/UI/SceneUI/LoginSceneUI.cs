using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class LoginSceneUI : SceneUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["QuitButton"].onClick.AddListener(() => { QuitButton(); });
	}
	
	public void QuitButton()
	{
		Application.Quit();
		Debug.Log("게임 종료");
	}
}
