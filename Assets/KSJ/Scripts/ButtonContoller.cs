using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
	public GameObject backpack;
	public List<Button> desires;

	public Dictionary<Button, bool> desireStates;

	private int maxOnButtons = 2;
	private int currentOnCount = 0;
	private Scrollbar scrollbar;

	void Start()
	{
		scrollbar = GetComponentInChildren<Scrollbar>();
		desireStates = new Dictionary<Button, bool>();

		// 각 버튼의 상태 초기화 및 클릭 이벤트 설정
		for (int i = 0; i < desires.Count; i++)
		{
			Button button = desires[i];
			// Load saved state from PlayerPrefs
			bool isOn = PlayerPrefs.GetInt("DesireState_" + i, 0) == 1;
			desireStates[button] = isOn;

			button.GetComponentInChildren<TMP_Text>().text = isOn ? "해제" : "장착";
			button.onClick.AddListener(() => ToggleButton(button));
			button.GetComponent<Image>().color = isOn ? Color.white : Color.gray;

			if (isOn)
			{
				currentOnCount++;
			}
		}
		DataManager.Instance.InitializeData(desireStates);
	}

	void ToggleButton(Button button)
	{
		// 버튼이 Off 상태일 때
		if (!desireStates[button])
		{
			if (currentOnCount < maxOnButtons)
			{
				SetButtonState(button, true);
				currentOnCount++;
			}
		}
		// 버튼이 On 상태일 때
		else
		{
			SetButtonState(button, false);
			currentOnCount--;
		}
		DataManager.Instance.InitializeData(desireStates);
		DataManager.Instance.SaveDataToJson();
	}

	void SetButtonState(Button button, bool isOn)
	{
		int index = desires.IndexOf(button);
		desireStates[button] = isOn; // 버튼 상태 저장
		button.GetComponentInChildren<TMP_Text>().text = isOn ? "해제" : "장착";
		button.GetComponent<Image>().color = isOn ? Color.white : Color.gray;

		// Save state to PlayerPrefs
		PlayerPrefs.SetInt("DesireState_" + index, isOn ? 1 : 0);
		PlayerPrefs.Save(); // 저장
	}
}