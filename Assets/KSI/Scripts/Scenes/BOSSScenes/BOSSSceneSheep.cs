using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSSceneSheep : MonoBehaviour
{
	private DebuffSystem debuffSystem;

	private void Start()
    {
		debuffSystem = FindObjectOfType<DebuffSystem>();

		debuffSystem.OnSheepDebuffChanged.Invoke();
		Debug.Log("OnSheepDebuffChanged.");
	}
}
