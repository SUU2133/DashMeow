using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class DeathPopUpUI : PopUpUI
{
	[Header("UI")]
	[SerializeField] private TextMeshProUGUI resultTitleText;
	[SerializeField] private TextMeshProUGUI totalScoreText;

	protected override void Awake()
	{
		base.Awake();

		buttons["DeathPopUpUIReplayButton"].onClick.AddListener(() => { RestartButton(); });
		buttons["DeathPopUpUIQuitButton"].onClick.AddListener(() => { QuitButton(); });
	}

	private void Update()
	{
		DisplayDeathUI();
	}

	public void RestartButton()
	{
		Time.timeScale = 1f;
		GameManager.UI.ClearPopUpUI();
		GameManager.Score.Reset();
		GameManager.Instance.ResetAllDebuffs();
		UnitySceneManager.LoadScene(UnitySceneManager.GetActiveScene().buildIndex);
	}

	public void QuitButton()
	{
		Time.timeScale = 1f;
		GameManager.UI.ClearPopUpUI();
		GameManager.Score.Reset();
		GameManager.Instance.ResetAllDebuffs();
		UnitySceneManager.LoadScene("LobbyScene");
	}

	public void DisplayDeathUI()
	{
		int totalScore = GameManager.Score.GetTotalScore();
		totalScoreText.text = totalScore.ToString();

		GameModeSystem gameModeSystem = FindObjectOfType<GameModeSystem>();
		Debug.Log($"Current Game Mode: {GameManager.GameModeSystem.curGameMode}");
		if (GameManager.GameModeSystem.curGameMode == GameModeSystem.GameMode.INFINITE)
		{
			resultTitleText.text = "RESULT";		
		}
		else
		{
			resultTitleText.text = "FAIL";		
		}
	}
}