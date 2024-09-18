using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSSceneChicken : MonoBehaviour
{
	private DebuffSystem debuffSystem;

	private void Start()
    {
		debuffSystem = FindObjectOfType<DebuffSystem>();

		debuffSystem.OnChickenDebuffChanged.Invoke();
		Debug.Log("OnChickenDebuffChanged.");
	}
}
