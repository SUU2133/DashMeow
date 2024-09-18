using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenStageSelectorUI : MonoBehaviour
{
	public Button subButton;
	public Button bossButton;
	//public Button berserkbossButton;

	private void Start()
	{
		DisableAllStageButtons();

		subButton.interactable = true;

		StageManager.OnChickenSubComplete.AddListener(() => ActivateButton(bossButton));
		//StageManager.OnChickenSubComplete.AddListener(() => ActivateButton(berserkbossButton));
	}

	private void DisableAllStageButtons()
	{
		subButton.interactable = false;
		bossButton.interactable = false;
		//berserkbossButton.interactable = false;
	}

	private void ActivateButton(Button button)
	{
		if (button != null)
		{
			button.interactable = true;
		}
	}
}
