using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BERSERKBOSSSceneDragon : MonoBehaviour
{
	private DebuffSystem debuffSystem;

	private void Start()
    {
		debuffSystem = FindObjectOfType<DebuffSystem>();

		debuffSystem.OnDragonDebuffChanged.Invoke();
		Debug.Log("OnDragonDebuffChanged.");
	}
}
