using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
	public List<Button> buttons; // 모든 버튼을 리스트로 관리
	public List<GameObject> uiWindows; // 모든 UI 창을 리스트로 관리
	private Dictionary<Button, GameObject> buttonToUIMap = new Dictionary<Button, GameObject>();

	public List<Button> closeButtons;
	private Dictionary<Button, GameObject> closeToUIMap = new Dictionary<Button, GameObject>();

	private void Start()
	{
		Debug.Log("창 매니져");
		
		
		// 버튼과 UI 창을 매핑
		for (int i = 0; i < buttons.Count; i++)
		{
			Button button = buttons[i];
			GameObject uiWindow = uiWindows[i];
			buttonToUIMap.Add(button, uiWindow);
			button.onClick.AddListener(() => OnButtonClick(button));
		}

		// 클로즈 버튼과 UI 창을 매핑
		for (int i = 0; i < closeButtons.Count; i++)
		{
			Button closeButton = closeButtons[i];
			GameObject uiWindow = uiWindows[i];
			closeToUIMap.Add(closeButton, uiWindow);
			closeButton.onClick.AddListener(() => CloseButtonClick(closeButton));
		}
		foreach (var uiWindow in uiWindows)
		{
			uiWindow.transform.localScale = new Vector3(1f, 1f, 1f);
			
			uiWindow.transform.localScale = new Vector3(0, 0, 0);
		}
	}

	private void OnButtonClick(Button clickedButton)
	{
		// 클릭된 버튼과 관련된 UI 창을 가져옴
		GameObject clickedUIWindow = buttonToUIMap[clickedButton];

		// 모든 UI 창을 숨김
		foreach (var uiWindow in uiWindows)
		{
			uiWindow.transform.localScale = new Vector3(0,0,0);
		}

		
		KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Positive);
		// 클릭된 버튼과 관련된 UI 창을 표시
		clickedUIWindow.transform.localScale = new Vector3(1f,1f,1f);
		//열리는 창에 스크롤바가 하나만 있다는 가정하에
		
		

	}

	private void CloseButtonClick(Button closeButton)
	{
		// 클릭된 버튼과 관련된 UI 창을 가져옴
		GameObject closeUIWindow = closeToUIMap[closeButton];
		
		KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Positive);
		// 클릭된 버튼과 관련된 UI 창을 표시
		closeUIWindow.transform.localScale = new Vector3(0f, 0f, 0f);
		//열리는 창에 스크롤바가 하나만 있다는 가정하에
	}

}