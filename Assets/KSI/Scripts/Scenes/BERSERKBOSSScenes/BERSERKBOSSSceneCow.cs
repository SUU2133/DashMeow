using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BERSERKBOSSSceneCow : MonoBehaviour
{
	private DebuffSystem debuffSystem;

	private void Start()
    {
		debuffSystem = FindObjectOfType<DebuffSystem>();

		debuffSystem.OnCowDebuffChanged.Invoke();
		Debug.Log("OnCowDebuffChanged.");
	}
}
