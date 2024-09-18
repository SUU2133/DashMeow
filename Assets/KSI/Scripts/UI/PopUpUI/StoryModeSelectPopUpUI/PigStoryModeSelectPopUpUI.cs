using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class PigStoryModeSelectPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["PigSUBButton"].onClick.AddListener(() => { LoadSUBScene(); });
		buttons["PigBOSSButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/BossPopUpUI/PigBossPopUpUI"); });
		//buttons["PigBERSERKBOSSButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/BERSERKBossPopUpU/PigBERSERKBossPopUpU"); });
		buttons["PigStoryModeSelectBackButton"].onClick.AddListener(() => { BackButton(); });
	}

	public void LoadSUBScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("SUBScenePig");
		GameManager.Scene.LoadSUB();
		Time.timeScale = 1f;	
	}

	public void BackButton()
	{
		GameManager.UI.ClearPopUpUI();
		GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeEntranceUI");
		Time.timeScale = 0;
	}
}
