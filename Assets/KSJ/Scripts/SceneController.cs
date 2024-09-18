using UnityEngine;
public class SceneController : MonoBehaviour
{
    public CustomSceneManager sceneManager; // SceneManager를 참조합니다.
    public string sceneName = "GameScene"; // 기본값 설정

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
