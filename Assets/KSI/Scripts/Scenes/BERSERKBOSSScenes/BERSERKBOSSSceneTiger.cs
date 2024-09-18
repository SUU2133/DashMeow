using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BERSERKBOSSSceneTiger : MonoBehaviour
{
	private DebuffSystem debuffSystem;

	private void Start()
    {
		debuffSystem = FindObjectOfType<DebuffSystem>();

		debuffSystem.OnTigerDebuffChanged.Invoke();
		Debug.Log("OnTigerDebuffChanged.");
	}
}
