using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
	public static event Action<Scene, LoadSceneMode> OnSceneLoaded;
	// 씬 이름으로 씬을 로드합니다.
	public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    // 현재 씬을 다시 로드합니다.
    public void ReloadCurrentScene()
    {
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        StartCoroutine(LoadSceneAsync(currentSceneName));
    }

    // 씬 인덱스로 씬을 로드합니다.	
    public void LoadSceneByIndex(int sceneIndex)
    {
        StartCoroutine(LoadSceneByIndexAsync(sceneIndex));
    }

    // 다음 씬을 로드합니다.
    public void LoadNextScene()
    {
        int nextSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(LoadSceneByIndexAsync(nextSceneIndex));
        }
        else
        {
            Debug.LogError("다음 씬이 존재하지 않습니다.");
        }
    }

    // 이전 씬을 로드합니다.
    public void LoadPreviousScene()
    {
        int previousSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex - 1;
        if (previousSceneIndex >= 0)
        {
            StartCoroutine(LoadSceneByIndexAsync(previousSceneIndex));
        }
        else
        {
            Debug.LogError("이전 씬이 존재하지 않습니다.");
        }
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        while (!asyncOperation.isDone)
        {
            // 로딩 진행 상황을 출력합니다.
            
            yield return null;
        }
    }

    private IEnumerator LoadSceneByIndexAsync(int sceneIndex)
    {
        AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncOperation.isDone)
        {
            // 로딩 진행 상황을 출력합니다.
            Debug.Log(asyncOperation.progress);
            yield return null;
        }
    }
}