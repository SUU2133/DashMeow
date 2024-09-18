using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class LoadingScene : MonoBehaviour
{
	[SerializeField] private LoadingUI progress;
	[SerializeField] private SceneNames nextScene;

	private void Awake()
	{
		SystemSetup();
	}

	private void SystemSetup()
	{
		Application.runInBackground = true;

		int width = Screen.width;
		int height = (int)(Screen.width * 9 / 16);
		Screen.SetResolution(width, height, true);

		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		progress.Play(OnAfterProgress);
	}

	private void OnAfterProgress()
	{
		SceneManager.LoadScene(nextScene);
	}
}
