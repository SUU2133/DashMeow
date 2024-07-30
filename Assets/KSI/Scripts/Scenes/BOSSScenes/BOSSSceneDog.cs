using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSSceneDog : MonoBehaviour
{
	private DebuffSystem debuffSystem;

	private void Start()
    {
		debuffSystem = FindObjectOfType<DebuffSystem>();

		debuffSystem.OnDogDebuffChanged.Invoke();
		Debug.Log("OnDogDebuffChanged.");
	}
}
