using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseKey : MonoBehaviour
{
	public enum KeyType
	{
		Silver,
		Gold
	}

	public KeyType keyType; // 이 버튼이 SilverKey인지 GoldKey인지 지정
	private Button button;

	private void Awake()
	{
		button = GetComponent<Button>();
		if (button == null)
		{
			Debug.LogError("use key 스크립트 버튼 미싱");
		}
		InvokeRepeating("CheckButtonInteractable", 0f, 1f);
	}



	private void CheckButtonInteractable()
	{
		switch (keyType)
		{
			case KeyType.Silver:
				// 은열쇠가 5개 미만이면 버튼을 비활성화
				button.interactable = DataManager.Instance.silverKey >= 5;
				break;
			case KeyType.Gold:
				// 금열쇠가 1개 미만이면 버튼을 비활성화
				button.interactable = DataManager.Instance.goldKey >= 1;
				break;
		}
	}

	public void OnButtonClick()
	{
		switch (keyType)
		{
			case KeyType.Silver:
				if (DataManager.Instance.silverKey >= 5)
				{
					DataManager.Instance.silverKey -= 5;
					Debug.Log("은열쇠 5개를 사용했습니다.");
				}
				else
				{
					Debug.LogError("은열쇠가 부족합니다.");
				}
				break;
			case KeyType.Gold:
				if (DataManager.Instance.goldKey >= 1)
				{
					DataManager.Instance.goldKey -= 1;
					Debug.Log("금열쇠 1개를 사용했습니다.");
				}
				else
				{
					Debug.LogError("금열쇠가 부족합니다.");
				}
				break;
		}

		// 데이터 저장 (필요에 따라 추가)
		DataManager.Instance.SaveDataToJson();

	}
}