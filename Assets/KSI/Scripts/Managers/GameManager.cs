using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	static GameManager instance;
	static ResourceManager resourceManager;
	static PoolManager poolManager;
	static UIManager uiManager;
	static SceneManager sceneManager;
	static SoundManager soundManager;
	static ScoreManager scoreManager;
	static BerserkSystemManager berserkSystemManager;
	static GameModeSystem gameModeSystem;
	static BackendManager backend;

	public static GameManager Instance { get { return instance; } }
	public static ResourceManager Resource { get { return resourceManager; } }
	public static PoolManager Pool { get { return poolManager; } }
	public static UIManager UI { get { return uiManager; } }
	public static SceneManager Scene { get { return sceneManager; } }
	public static SoundManager Sound { get { return soundManager; } }
	public static ScoreManager Score { get { return scoreManager; } }
	public static BerserkSystemManager BerserkSystem { get { return berserkSystemManager; } }
	public static GameModeSystem GameModeSystem { get { return gameModeSystem; } }
	public static BackendManager Backend { get { return backend; } }

	public BerserkSystemManager.ZodiacSign BossDebuff { get; set; }
	public BerserkSystemManager.ZodiacSign InfiniteDebuff1 { get; set; }
	public BerserkSystemManager.ZodiacSign InfiniteDebuff2 { get; set; }

	public bool IsGameStarted { get; private set; } = false;

	private void Awake()
	{
		if (instance != null)
		{
			Destroy(this);
			return;
		}

		instance = this;
		DontDestroyOnLoad(this);

		InitManagers();

		//OnApplicationPause();
		//OnApplicationQuit();

		Application.targetFrameRate = 60;
	}

	private void OnDestroy()
	{
		if (instance == this)
			instance = null;
	}

	private void InitManagers()
	{
		GameObject resourceObject = new GameObject();
		resourceObject.name = "ResourceManager";
		resourceObject.transform.parent = transform;
		resourceManager = resourceObject.AddComponent<ResourceManager>();

		GameObject poolObject = new GameObject();
		poolObject.name = "PoolManager";
		poolObject.transform.parent = transform;
		poolManager = poolObject.AddComponent<PoolManager>();

		GameObject uiObject = new GameObject();
		uiObject.name = "UIManager";
		uiObject.transform.parent = transform;
		uiManager = uiObject.AddComponent<UIManager>();

		GameObject sceneObject = new GameObject();
		sceneObject.name = "SceneManager";
		sceneObject.transform.parent = transform;
		sceneManager = sceneObject.AddComponent<SceneManager>();

		GameObject scoreObject = new GameObject();
		scoreObject.name = "ScoreManager";
		scoreObject.transform.parent = transform;
		scoreManager = scoreObject.AddComponent<ScoreManager>();

		GameObject soundObject = new GameObject();
		soundObject.name = "SoundManager"; 
		soundObject.transform.parent = transform;
		soundManager = soundObject.AddComponent<SoundManager>();

		GameObject berserkSystemObject = new GameObject();
		berserkSystemObject.name = "BerserkSystemManager";
		berserkSystemObject.transform.parent = transform;
		berserkSystemManager = berserkSystemObject.AddComponent<BerserkSystemManager>();

		GameObject gameModeSystemObject = new GameObject();
		gameModeSystemObject.name = "GameModeSystem ";
		gameModeSystemObject.transform.parent = transform;
		gameModeSystem = gameModeSystemObject.AddComponent<GameModeSystem>();
	}

	private void OnApplicationPause()
	{
		Screen.orientation = ScreenOrientation.AutoRotation;

		Screen.autorotateToPortrait = false;

		Screen.autorotateToPortraitUpsideDown = false;

		Screen.autorotateToLandscapeLeft = true;

		Screen.autorotateToLandscapeRight = true;
	}

	public void StartGame()
	{
		IsGameStarted = true;
		GameManager.Score.Reset();
	}

	public void StopGame()
	{
		IsGameStarted = false;
	}

	public void EndGame()
	{
		ResetAllDebuffs(); 
	}

	public void InfiniteEndGame()
	{ 
		ResetAllDebuffs();
	}

	public void ResetBossDebuff()
	{
		BossDebuff = default;
	}

	public void ResetInfiniteDebuff()
	{
		InfiniteDebuff1 = default;
		InfiniteDebuff2 = default;
	}

	public void ResetAllDebuffs()
	{
		ResetBossDebuff();
		ResetInfiniteDebuff();
	}

	//private void OnApplicationQuit()
	//{
	//	Account.Inst.UserDataAllSave();
	//}

	//private void OnDisable()
	//{
	//	Account.Inst.UserDataAllSave();
	//}

	//private void OnApplicationFocus(bool hasFocus)
	//{
	//	if (hasFocus)
	//	{
	//		GameManager.Backend.AutoLogin();
	//	}
	//}
}