using System.Collections;
using UnityEngine;

public class CustomSceneManager : MonoBehaviour
{
    // �� �̸����� ���� �ε��մϴ�.
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    // ���� ���� �ٽ� �ε��մϴ�.
    public void ReloadCurrentScene()
    {
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        StartCoroutine(LoadSceneAsync(currentSceneName));
    }

    // �� �ε����� ���� �ε��մϴ�.
    public void LoadSceneByIndex(int sceneIndex)
    {
        StartCoroutine(LoadSceneByIndexAsync(sceneIndex));
    }

    // ���� ���� �ε��մϴ�.
    public void LoadNextScene()
    {
        int nextSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(LoadSceneByIndexAsync(nextSceneIndex));
        }
        else
        {
            Debug.LogError("���� ���� �������� �ʽ��ϴ�.");
        }
    }

    // ���� ���� �ε��մϴ�.
    public void LoadPreviousScene()
    {
        int previousSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex - 1;
        if (previousSceneIndex >= 0)
        {
            StartCoroutine(LoadSceneByIndexAsync(previousSceneIndex));
        }
        else
        {
            Debug.LogError("���� ���� �������� �ʽ��ϴ�.");
        }
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        while (!asyncOperation.isDone)
        {
            // �ε� ���� ��Ȳ�� ����մϴ�.
            Debug.Log(asyncOperation.progress);
            yield return null;
        }
    }

    private IEnumerator LoadSceneByIndexAsync(int sceneIndex)
    {
        AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncOperation.isDone)
        {
            // �ε� ���� ��Ȳ�� ����մϴ�.
            Debug.Log(asyncOperation.progress);
            yield return null;
        }
    }
}