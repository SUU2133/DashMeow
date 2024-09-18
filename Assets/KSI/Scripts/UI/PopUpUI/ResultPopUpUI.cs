using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class ResultPopUpUI : PopUpUI
{
	[Header("UI")]
	[SerializeField] private TextMeshProUGUI totalScoreText;
	[SerializeField] private TextMeshProUGUI rewardSushiText;
	//[SerializeField] private TextMeshProUGUI desirePieceiText;
	
	public UnityAction OnDisplayDeathUIChanged;

	private void OnEnable()
	{
		OnDisplayDeathUIChanged += DisplayResultUI;
	}

	protected override void Awake()
	{
		base.Awake();

		buttons["ResultPopUpUIReplayButton"].onClick.AddListener(() => { RestartButton(); });
		buttons["ResultPopUpUIQuitButton"].onClick.AddListener(() => { QuitButton(); });
		buttons["ResultPopUpUINextButton"].onClick.AddListener(() => { NextButton(); });
	}

	private void Start()
	{
		totalScoreText = GameObject.Find("TotalScoreText").GetComponent<TextMeshProUGUI>();
		//rewardSushiText = GameObject.Find("JellyPawCountText").GetComponent<TextMeshProUGUI>();
		//desirePieceiText = GameObject.Find("DesireText").GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		DisplayResultUI();
	}

	public void RestartButton()
	{
		GameManager.UI.ClosePopUpUI();
		Time.timeScale = 1f;
		GameManager.Score.Reset();
		GameManager.Instance.ResetAllDebuffs();
		UnitySceneManager.LoadScene(UnitySceneManager.GetActiveScene().buildIndex);
	}

	public void QuitButton()
	{
		GameManager.UI.ClearPopUpUI();
		Time.timeScale = 1f;
		GameManager.Score.Reset();
		GameManager.Instance.ResetAllDebuffs();
		UnitySceneManager.LoadScene("LobbyScene");
	}

	public void NextButton()
	{
		GameManager.UI.ClearPopUpUI();
		Time.timeScale = 1f;
		GameManager.Score.Reset();
		GameManager.Instance.ResetAllDebuffs();

		string currentSceneName = UnitySceneManager.GetActiveScene().name;

		if (currentSceneName == "SUBScenePig")
		{
			StageManager.OnPigSubComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/PigStoryModeSelectUI");
		}
		else if (currentSceneName == "BOSSScenePig" || currentSceneName == "BERSERKBOSSScenePig")
		{
			StageManager.OnPigBossComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeEntranceUI");
		}
		else if(currentSceneName == "SUBSceneDog")
		{
			StageManager.OnPigSubComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/DogStoryModeSelectUI");
		}
		else if (currentSceneName == "BOSSSceneDog" || currentSceneName == "BERSERKBOSSSceneDog")
		{
			StageManager.OnPigBossComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeEntranceUI");
		}
		else if (currentSceneName == "SUBSceneChicken")
		{
			StageManager.OnPigSubComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/ChickenStoryModeSelectUI");
		}
		else if (currentSceneName == "BOSSSceneChicken" || currentSceneName == "BERSERKBOSSSceneChicken")
		{
			StageManager.OnPigBossComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeEntranceUI");
		}
		else if (currentSceneName == "SUBSceneMonkey")
		{
			StageManager.OnPigSubComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/MonkeyStoryModeSelectUI");
		}
		else if (currentSceneName == "BOSSSceneMonkey" || currentSceneName == "BERSERKBOSSSceneMonkey")
		{
			StageManager.OnPigBossComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeEntranceUI");
		}
		else if (currentSceneName == "SUBSceneSheep")
		{
			StageManager.OnPigSubComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/SheepStoryModeSelectUI");
		}
		else if (currentSceneName == "BOSSSceneSheep" || currentSceneName == "BERSERKBOSSSceneSheep")
		{
			StageManager.OnPigBossComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeEntranceUI");
		}
		else if (currentSceneName == "SUBSceneHorse")
		{
			StageManager.OnPigSubComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/HorseStoryModeSelectUI");
		}
		else if (currentSceneName == "BOSSSceneHorse" || currentSceneName == "BERSERKBOSSSceneHorse")
		{
			StageManager.OnPigBossComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeEntranceUI");
		}
		else if (currentSceneName == "SUBSceneSnake")
		{
			StageManager.OnPigSubComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/SnakeStoryModeSelectUI");
		}
		else if (currentSceneName == "BOSSSceneSnake" || currentSceneName == "BERSERKBOSSSceneSnake")
		{
			StageManager.OnPigBossComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeEntranceUI");
		}
		else if (currentSceneName == "SUBSceneDragon")
		{
			StageManager.OnPigSubComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/DragonStoryModeSelectUI");
		}
		else if (currentSceneName == "BOSSSceneDragon" || currentSceneName == "BERSERKBOSSSceneDragon")
		{
			StageManager.OnPigBossComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeEntranceUI");
		}
		else if (currentSceneName == "SUBSceneRabbit")
		{
			StageManager.OnPigSubComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/RabbitStoryModeSelectUI");
		}
		else if (currentSceneName == "BOSSSceneRabbit" || currentSceneName == "BERSERKBOSSSceneRabbit")
		{
			StageManager.OnPigBossComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeEntranceUI");
		}
		else if (currentSceneName == "SUBSceneTiger")
		{
			StageManager.OnPigSubComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/TigerStoryModeSelectUI");
		}
		else if (currentSceneName == "BOSSSceneTiger" || currentSceneName == "BERSERKBOSSSceneTiger")
		{
			StageManager.OnPigBossComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeEntranceUI");
		}
		else if (currentSceneName == "SUBSceneCow")
		{
			StageManager.OnPigSubComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/CowStoryModeSelectUI");
		}
		else if (currentSceneName == "BOSSSceneCow" || currentSceneName == "BERSERKBOSSSceneCow")
		{
			StageManager.OnPigBossComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeEntranceUI");
		}
		else if (currentSceneName == "SUBSceneMouse")
		{
			StageManager.OnPigSubComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/MouseStoryModeSelectUI");
		}
		else if (currentSceneName == "BOSSSceneMouse" || currentSceneName == "BERSERKBOSSSceneMouse")
		{
			StageManager.OnPigBossComplete.Invoke();
			GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeEntranceUI");
		}
	}

	public void DisplayResultUI()
	{
		int totalScore = GameManager.Score.GetTotalScore();
		totalScoreText.text = totalScore.ToString();

		int rewardSushi = totalScore / 500 * 25 ;
		rewardSushiText.text = rewardSushi.ToString();
	}

	public void UpdateRewardSushiText(int reward)
	{
		rewardSushiText.text = reward.ToString();

	}

	//public void UpdateDesirePieceiText(int rewardValue)
	//{
	//	desirePieceiText.text = rewardValue.ToString();
	//}
}
