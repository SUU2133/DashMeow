using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSSceneTiger : MonoBehaviour
{
	private DebuffSystem debuffSystem;

	private void Start()
    {
		debuffSystem = FindObjectOfType<DebuffSystem>();

		debuffSystem.OnTigerDebuffChanged.Invoke();
		Debug.Log("OnTigerDebuffChanged.");
	}
}
