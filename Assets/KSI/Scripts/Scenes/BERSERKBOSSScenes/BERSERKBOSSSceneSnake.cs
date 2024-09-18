using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BERSERKBOSSSceneSnake : MonoBehaviour
{
	private DebuffSystem debuffSystem;

	private void Start()
    {
		debuffSystem = FindObjectOfType<DebuffSystem>();

		debuffSystem.OnSnakeDebuffChanged.Invoke();
		Debug.Log("OnSnakeDebuffChanged.");
	}
}
