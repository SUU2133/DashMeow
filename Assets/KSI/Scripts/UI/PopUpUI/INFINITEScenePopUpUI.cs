using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class INFINITEScenePopUpUI : MonoBehaviour
{
	[SerializeField] private GameObject popupPanel;

	private void Start()
	{
		ShowPopup();
	}

	public void ShowPopup()
	{
		popupPanel.SetActive(true);
		Time.timeScale = 0f;
	}

	public void ClosePopup()
	{
		popupPanel.SetActive(false);
		GameStart();
		Time.timeScale = 0.1f;

	}

	public void GameStart()
	{
		GameManager.Scene.StartGame();
	}
}
