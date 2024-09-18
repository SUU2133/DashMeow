using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public enum SceneNames
{ 
	LoadingScene,
	LoginScene,
	LobbyScene
}

public class SceneManager : MonoBehaviour
{
	public static string GetActiveScene()
	{ 
		return UnitySceneManager.GetActiveScene().name;
	}

	public static void LoadScene(string sceneName = "")
	{
		if (sceneName == "")
		{
			UnitySceneManager.LoadScene(GetActiveScene());
		}
		else
		{
			UnitySceneManager.LoadScene(sceneName);
		}
	}

	public static void LoadScene(SceneNames sceneName)
	{
		UnitySceneManager.LoadScene(sceneName.ToString());
	}

	public void StartGame()
	{
		UnitySceneManager.LoadScene("INFINITEScene");
		Time.timeScale = 0;
		Time.timeScale = 1;
	}

	public void LoadLOBBY()
	{
		GameManager.GameModeSystem.curGameMode = GameModeSystem.GameMode.LOBBY;
	}

	public void LoadSUB()
	{
		GameManager.GameModeSystem.curGameMode = GameModeSystem.GameMode.SUB;
	}

	public void LoadBOSS()
	{
		GameManager.GameModeSystem.curGameMode = GameModeSystem.GameMode.BOSS;
	}

	public void LoadBERSERKBOSS()
	{
		GameManager.GameModeSystem.curGameMode = GameModeSystem.GameMode.BERSERKBOSS;
	}

	public void LoadINFINITE()
	{
		GameManager.GameModeSystem.curGameMode = GameModeSystem.GameMode.INFINITE;
	}

	//private LoadingUI loadingUI;

	//BaseScene curScene;
	//public BaseScene CurScene
	//{
	//	get
	//	{
	//		// FindObjectOfType을 자주 쓰면 부담이 되니까 Null로 확인함
	//		if (curScene == null)
	//			curScene = GameObject.FindObjectOfType<BaseScene>();

	//		return curScene;
	//	}
	//}

	//void Awake()
	//{
	//	//LoadingUI ui = GameManager.Resource.Load<LoadingUI>("UI/LoadingUI");
	//	//loadingUI = Instantiate(ui);
	//	//loadingUI.transform.SetParent(transform, false);
	//}

	//public void LoadScene(string sceneName)
	//{
	//	StartCoroutine(LoadingRoutine(sceneName));
	//}

	//IEnumerator LoadingRoutine(string sceneName)
	//{
	//	loadingUI.FadeOut();
	//	yield return new WaitForSeconds(0.5f);
	//	//로딩 중에는 게임의 시간을 멈춰줌
	//	Time.timeScale = 0f;

	//	// 비동기식 로딩
	//	AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
	//	while (!oper.isDone)
	//	{
	//		loadingUI.SetProgress(Mathf.Lerp(0.0f, 0.5f, oper.progress));
	//		yield return null;
	//	}

	//	if (CurScene != null)
	//	{
	//		CurScene.LoadAsync();
	//		while (CurScene.progress < 1f)
	//		{
	//			loadingUI.SetProgress(Mathf.Lerp(0.5f, 1.0f, CurScene.progress));
	//			yield return null;
	//		}
	//	}

	//	loadingUI.SetProgress(1.0f);
	//	//로딩 중에는 게임의 시간을 멈춘 것 해제
	//	Time.timeScale = 1f;
	//	loadingUI.FadeIn();
	//	yield return new WaitForSeconds(1f);
	//}
}