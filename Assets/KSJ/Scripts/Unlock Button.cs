using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockButton : MonoBehaviour
{
	public GameObject isBuyUI;
	public Transform contentTransform;
	public Button OverlayButton;
	public GameObject itemPrefab;
	public int cost;
	public int itemID;

	private string saveKey;

	
	public void Start()
	{
		isBuyUI = GameObject.Find("/Canvas/Is Buy");
		OverlayButton = GameObject.Find("/Canvas/Overlay Button").GetComponent<Button>();

		if (isBuyUI == null)
		{
			Debug.LogError("IsBuy UI를 찾을 수 없습니다. 이름이 정확한지 확인하세요.");
		}

		// isBuyUI 안의 Content를 찾음
		contentTransform = isBuyUI.transform.Find("Contents");
		if (contentTransform == null)
		{
			Debug.LogError("Contents 오브젝트를 찾을 수 없습니다. isBuyUI 안에 Content 오브젝트를 추가하세요.");
		}

		// 고유한 저장 키 설정
		saveKey = "UnlockButton_" + gameObject.name;

		// 상태 로드
		if (PlayerPrefs.GetInt(saveKey, 1) == 0)
		{
			gameObject.SetActive(false);
		}

		//ResetUnlockButton();
	}

	public void OnClick()
	{
		KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Positive);
		Debug.Log("클릭됨");

		// isBuyUI를 활성화하고 위치를 고정된 위치로 설정
		isBuyUI.transform.localScale = new Vector3(1, 1, 1);
		OverlayButton.transform.localScale = new Vector3(1, 1, 1);

		// 기존에 생성된 아이템이 있다면 제거
		foreach (Transform child in contentTransform)
		{
			Destroy(child.gameObject);
		}

		// 클릭된 아이템 프리팹을 contentTransform 안에 생성
		
		GameObject spawnedItem = Instantiate(itemPrefab, contentTransform, transform.parent);
		RectTransform spawnedItemRect = spawnedItem.GetComponent<RectTransform>();
		if (spawnedItemRect != null)
		{
			Vector3 position = spawnedItemRect.localPosition;
			position.x = 0;
			position.y = 40;
			spawnedItemRect.localPosition = position;
		}

		Button[] buttons = spawnedItem.GetComponentsInChildren<Button>();
		foreach (Button button in buttons)
		{
			button.gameObject.SetActive(false);
		}
		
		Button buyButton = isBuyUI.transform.Find("Is Buy Button").GetComponent<Button>();
		buyButton.onClick.RemoveAllListeners();
		buyButton.onClick.AddListener(() => IsBuy(itemID));
	}

	public void IsBuy(int _itemID)
	{
		switch (_itemID)
		{
			case 300: // 스킨들
				if (DataManager.Instance.cannedFood - cost < 0)
				{
					KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Negative);
					break;
				}
				DataManager.Instance.cannedFood -= cost;

				// 오브젝트 비활성화하고 상태 저장
				PlayerPrefs.SetInt(saveKey, 0);
				PlayerPrefs.Save();

				gameObject.SetActive(false);
				break;


			default:
				break;
		}

		isBuyUI.transform.localScale = new Vector3(0, 0, 0);
		OverlayButton.transform.localScale = new Vector3(0, 0, 0);
		DataManager.Instance.SaveDataToJson();
	}

	public void ResetUnlockButton()
	{
		// 모든 UnlockButton 상태를 초기화하여 다시 활성화
		PlayerPrefs.DeleteKey(saveKey);
		gameObject.SetActive(true);
	}
}