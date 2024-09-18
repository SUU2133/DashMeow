using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSSceneRabbit : MonoBehaviour
{
	private DebuffSystem debuffSystem;

	private void Start()
    {
		debuffSystem = FindObjectOfType<DebuffSystem>();

		debuffSystem.OnRabbitDebuffChanged.Invoke();
		Debug.Log("OnRabbitDebuffChanged.");
	}
}
