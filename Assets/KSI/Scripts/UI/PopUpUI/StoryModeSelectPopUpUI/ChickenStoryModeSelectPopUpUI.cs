using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class ChickenStoryModeSelectPopUpUI : PopUpUI
{
	protected override void Awake()
	{
		base.Awake();

		buttons["ChickenSUBButton"].onClick.AddListener(() => { LoadSUBScene(); });
		buttons["ChickenBOSSButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/BossPopUpUI/ChickenBossPopUpUI"); });
		//buttons["ChickenBERSERKBOSSButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/BERSERKBossPopUpUI/ChickenBERSERKBossPopUpUI"); });
		buttons["ChickenStoryModeSelectBackButton"].onClick.AddListener(() => { GameManager.UI.ClosePopUpUI(); });
	}

	public void LoadSUBScene()
	{
		GameManager.UI.ClearPopUpUI();
		UnitySceneManager.LoadScene("SUBSceneChicken");
		GameManager.Scene.LoadSUB();
		Time.timeScale = 1f;
	}
}
