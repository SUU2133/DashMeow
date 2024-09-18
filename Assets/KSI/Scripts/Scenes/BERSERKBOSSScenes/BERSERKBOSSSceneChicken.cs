using UnityEngine;

public class BerserkBOSSSceneChicken : MonoBehaviour
{
	private DebuffSystem debuffSystem;

	private void Start()
    {
		debuffSystem = FindObjectOfType<DebuffSystem>();

		debuffSystem.OnChickenDebuffChanged.Invoke();
		Debug.Log("OnChickenDebuffChanged.");
	}
}
