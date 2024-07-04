using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public CustomSceneManager sceneManager; // SceneManager�� �����մϴ�.
    public string sceneName = "GameScene"; // �⺻�� ����

    public void LoadScene()
    {
        if (sceneManager != null)
        {
            sceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("SceneManager is not set.");
        }
    }
}
