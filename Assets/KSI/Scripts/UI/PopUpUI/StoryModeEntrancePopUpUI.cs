using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;


public class StoryModeEntrancePopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["PigButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/PigStoryModeSelectUI"); });
		buttons["ChickenButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/ChickenStoryModeSelectUI"); });
		buttons["SheepButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/SheepStoryModeSelectUI"); });
		buttons["SnakeButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/SnakeStoryModeSelectUI"); });
		buttons["RabbitButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/RabbitStoryModeSelectUI"); });
		buttons["CowButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/CowStoryModeSelectUI"); });
		buttons["DogButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/DogStoryModeSelectUI"); });
		buttons["MonkeyButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/MonkeyStoryModeSelectUI"); });
		buttons["HorseButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/HorseStoryModeSelectUI"); });
		buttons["DragonButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/DragonStoryModeSelectUI"); });
		buttons["TigerButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/TigerStoryModeSelectUI"); });
		buttons["MouseButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeSelectUI/MouseStoryModeSelectUI"); });

		buttons["StoryModeEntranceBackButton"].onClick.AddListener(() => { BackButton(); });
	}

	public void BackButton()
	{	
		Time.timeScale = 0f;
		UnitySceneManager.LoadScene("LobbyScene");
		GameManager.UI.ClearPopUpUI();
		GameManager.Scene.LoadLOBBY();
	}
}
