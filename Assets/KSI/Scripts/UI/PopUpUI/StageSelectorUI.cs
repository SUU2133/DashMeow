using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectorUI : MonoBehaviour
{
	public Button pigButton;
	public Button dogButton;
	public Button chickenButton;
	public Button monkeyButton;
	public Button sheepButton;
	public Button horseButton;
	public Button snakeButton;
	public Button dragonButton;
	public Button rabbitButton;
	public Button tigerButton;
	public Button cowButton;
	public Button mouseButton;

	private void Start()
	{
		DisableAllStageButtons();

		pigButton.interactable = true;

		StageManager.OnPigBossComplete.AddListener(() => ActivateButton(dogButton));
		StageManager.OnChickenBossComplete.AddListener(() => ActivateButton(monkeyButton));
		StageManager.OnMonkeyBossComplete.AddListener(() => ActivateButton(sheepButton));
		StageManager.OnSheepBossComplete.AddListener(() => ActivateButton(horseButton));
		StageManager.OnHorseBossComplete.AddListener(() => ActivateButton(snakeButton));
		StageManager.OnSnakeBossComplete.AddListener(() => ActivateButton(dragonButton));
		StageManager.OnDragonBossComplete.AddListener(() => ActivateButton(rabbitButton));
		StageManager.OnRabbitBossComplete.AddListener(() => ActivateButton(tigerButton));
		StageManager.OnTigerBossComplete.AddListener(() => ActivateButton(cowButton));
		StageManager.OnCowBossComplete.AddListener(() => ActivateButton(mouseButton));
	}

	private void DisableAllStageButtons()
	{
		pigButton.interactable = false;
		dogButton.interactable = false;
		chickenButton.interactable = false;
		monkeyButton.interactable = false;
		sheepButton.interactable = false;
		horseButton.interactable = false;
		snakeButton.interactable = false;
		dragonButton.interactable = false;
		rabbitButton.interactable = false;
		tigerButton.interactable = false;
		cowButton.interactable = false;
		mouseButton.interactable = false;
	}

	private void ActivateButton(Button button)
	{
		if (button != null)
		{
			button.interactable = true;
		}
	}
}
