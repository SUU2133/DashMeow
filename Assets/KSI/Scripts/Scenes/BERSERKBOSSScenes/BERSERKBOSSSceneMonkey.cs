using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BERSERKBOSSSceneMonkeyn : MonoBehaviour
{
	private DebuffSystem debuffSystem;

	private void Start()
    {
		debuffSystem = FindObjectOfType<DebuffSystem>();

		debuffSystem.OnMonkeyDebuffChanged.Invoke();
		Debug.Log("OnMonkeyDebuffChanged.");
	}
}
