public class GameModeButtonSceneUI : SceneUI
{
	protected override void Awake()
	{
		base.Awake();

		//buttons["INFINITEButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/InfiniteModeEntranceUI"); });
		buttons["STROYButton"].onClick.AddListener(() => { GameManager.UI.ShowPopUpUI<PopUpUI>("UI/StoryModeEntranceUI"); });	
	}
}