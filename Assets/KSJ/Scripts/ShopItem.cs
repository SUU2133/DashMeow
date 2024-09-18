using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
	public int moneyCost; // 유료 재화
	public int sushiCost;
	public int cannedFoodCost;
	public int quantity;
	public int itemID;
	public bool alreadyBuy;
	public GameObject isBuyUI;
	public GameObject itemPrefab; // 아이템 프리팹을 저장할 변수
	public Transform contentTransform; // 프리팹 아이템을 넣을 부모 Transform
	public Button OverlayButton; // 다른 곳을 클릭하면 isBuyUI가 꺼지게 하는 버튼
	private const string ItemPurchasedKey = "ItemPurchased_";//냥냥게이지 구매 상태를 저장
	public TMP_Text costText;
	public TMP_Text quantityText;

	// 한번 샀을 때 색을 바꿈
	public Button button;
	public Image icon;
	public Image background;

	public static object Instance { get; internal set; }

	private void Start()
	{
		costText = transform.Find("Button/Sushi Cost").GetComponent<TMP_Text>();
		quantityText = transform.Find("Button/Quantity").GetComponent<TMP_Text>();
		OverlayButton = GameObject.Find("/Canvas/Overlay Button").GetComponent<Button>();
		button = GetComponentInChildren<Button>();
		icon = transform.Find("Button/Icon").GetComponent<Image>();
		background = transform.Find("Button/Background").GetComponent<Image>();

		itemPrefab = transform.gameObject;
		// isBuyUI를 찾음
		isBuyUI = GameObject.Find("/Canvas/Is Buy");
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
		// 냥냥 게이지 구매 상태 저장
		if (PlayerPrefs.GetInt(ItemPurchasedKey + itemID, 0) == 1)
		{
			gameObject.SetActive(false);
			return;
		}
		UpdateText();

	}

	private void Update() { }

	public void OnClick()
	{
		if (DataManager.Instance.sushi - sushiCost < 0)
		{
			KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Negative);
			return;
		}
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
		Button buyButton = isBuyUI.transform.Find("Is Buy Button").GetComponent<Button>();
		buyButton.onClick.RemoveAllListeners();
		buyButton.onClick.AddListener(() => IsBuy(itemID));
	}

	public void IsBuy(int _itemID)
	{
		switch (_itemID)
		{

			case 0: // 은방울
				if (DataManager.Instance.sushi - sushiCost < 0)
				{
					KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Negative);
					break;
				}
				DataManager.Instance.sushi -= sushiCost;
				DataManager.Instance.silverKey += quantity;
				gameObject.SetActive(false);
				break;
			case 1: // 금방울
				if (DataManager.Instance.sushi - sushiCost < 0)
				{
					KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Negative);
					break;
				}
				DataManager.Instance.sushi -= sushiCost;
				DataManager.Instance.goldKey += quantity;
				gameObject.SetActive(false);
				break;
			case 2: // 부활권
				if (DataManager.Instance.sushi - sushiCost < 0)
				{
					KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Negative);
					break;
				}
				DataManager.Instance.sushi -= sushiCost;
				DataManager.Instance.resurrection += quantity;
				gameObject.SetActive(false);
				break;
			case 3: // 적 깨진 염원 조각
				if (DataManager.Instance.sushi - sushiCost < 0)
				{
					KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Negative);
					break;
				}
				DataManager.Instance.sushi -= sushiCost;
				DataManager.Instance.brokenRed += quantity;
				gameObject.SetActive(false);
				break;
			case 4: // 청 깨진 염원 조각
				if (DataManager.Instance.sushi - sushiCost < 0)
				{
					KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Negative);
					break;
				}
				DataManager.Instance.sushi -= sushiCost;
				DataManager.Instance.brokenBlue += quantity;
				gameObject.SetActive(false);
				break;
			case 5: // 녹 깨진 염원 조각
				if (DataManager.Instance.sushi - sushiCost < 0)
				{
					KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Negative);
					break;
				}
				DataManager.Instance.sushi -= sushiCost;
				DataManager.Instance.brokenGreen += quantity;
				gameObject.SetActive(false);
				break;

			case 20: // 일일 상점 무료 초밥
				if (DataManager.Instance.freeSushi >= DataManager.Instance.freeSushiMax)
				{
					KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Negative);
					break;
				}
				DataManager.Instance.freeSushi++;
				DataManager.Instance.sushi += quantity;
				break;
			case 100: // 통조림으로 은열쇠 구입 - 은열쇠 상점
				if (DataManager.Instance.cannedFood - cannedFoodCost < 0 || DataManager.Instance.silverKey > 999)
				{
					KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Negative);
					break;
				}
				DataManager.Instance.cannedFood -= cannedFoodCost;
				DataManager.Instance.silverKey += quantity;
				break;

			case 101:// 초밥으로 은열쇠 구입 - 은열쇠 상점
				if (DataManager.Instance.sushi - sushiCost < 0 || DataManager.Instance.silverKey > 999)
				{
					KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Negative);
					break;
				}
				DataManager.Instance.sushi -= sushiCost;
				DataManager.Instance.silverKey += quantity;
				break;


			case 200: //통조림으로 초밥 구입 - 일일 상점 초밥들
				if (DataManager.Instance.cannedFood - cannedFoodCost < 0 || DataManager.Instance.sushi > DataManager.Instance.sushiMax)
				{
					KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Negative);
					break;
				}
				DataManager.Instance.cannedFood -= cannedFoodCost;
				DataManager.Instance.sushi += quantity;
				break;

			case 300: // 스킨들
				if (DataManager.Instance.cannedFood - cannedFoodCost < 0)
				{
					KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Negative);
					break;
				}
				DataManager.Instance.cannedFood -= cannedFoodCost;
				transform.gameObject.SetActive(false);
				break;

			case 500:// 냥냥 게이지 - 통조림 1,3번째

				DataManager.Instance.cannedFood += quantity;
				transform.gameObject.SetActive(false);
				PlayerPrefs.SetInt(ItemPurchasedKey + _itemID, 1); // 상태 저장
				PlayerPrefs.Save();
				// 상태 저장
				break;

			case 501:// 냥냥 게이지 - 염원 조각 10개씩

				DataManager.Instance.brokenBlue += 10;
				DataManager.Instance.brokenRed += 10;
				DataManager.Instance.brokenGreen += 10;
				transform.gameObject.SetActive(false);
				PlayerPrefs.SetInt(ItemPurchasedKey + _itemID, 1); // 상태 저장
				PlayerPrefs.Save();
				// 상태 저장
				break;

			case 502: //냥냥 게이지 - 초롱냥과 배경
				DataManager.Instance.gageCatWallpaper+=1;
				DataManager.Instance.gageCatSkin += 1;
				DataManager.Instance.gageCatEffect += 1;
				transform.gameObject.SetActive(false);
				PlayerPrefs.SetInt(ItemPurchasedKey + _itemID, 1); // 상태 저장
				PlayerPrefs.Save();
				// 상태 저장
				break;
			case 503: //냥냥 게이지 - 특별 염원
				DataManager.Instance.glideTime += 3;
				DataManager.Instance.allRes += 1;
				DataManager.Instance.maxHealth += 5;
				transform.gameObject.SetActive(false);
				PlayerPrefs.SetInt(ItemPurchasedKey + _itemID, 1); // 상태 저장
				PlayerPrefs.Save();
				// 상태 저장
				break;

		}



		List<int> itemsToRemove = new List<int>();
		foreach (int item in DataManager.Instance.resetItemID)
		{
			if (_itemID == item)
			{
				itemsToRemove.Add(item);
			}
		}

		// 반복이 끝난 후에 컬렉션을 수정
		foreach (int item in itemsToRemove)
		{
			DataManager.Instance.resetItemID.Remove(item);
		}

		isBuyUI.transform.localScale = new Vector3(0, 0, 0);
		OverlayButton.transform.localScale = new Vector3(0, 0, 0);
		DataManager.Instance.SaveDataToJson();
	}





	public void UpdateText()
	{
		if (itemID == 100)
		{
			quantityText.text = "" + quantity;
			costText.text = "통조림 : " + cannedFoodCost;
		}
		else if (itemID == 101)
		{
			quantityText.text = "" + quantity;
			costText.text = "초밥 : " + sushiCost;
		}
		else if (itemID == 20)
		{



			costText.text = "일일 500개 \n무료 초밥";


		}
		else if (itemID == 200)
		{
			quantityText.text = "" + quantity;
			costText.text = "통조림:" + cannedFoodCost;
		}




		else if (itemID == 500)//냥냥게이지 1,3 통조림
		{
			quantityText.text = "" + quantity;
			costText.text = "무료";
		}
		else if (itemID == 501)//냥냥게이지
		{
			quantityText.text = "깨진 염원 \n10개씩";
			costText.text = "무료";
		}
		else if (itemID == 502)
		{
			quantityText.text = "초롱냥과 배경";
			costText.text = "무료";
		}






		else
		{
			quantityText.text = "" + quantity;
			costText.text = "초밥 : " + sushiCost;

		}
		



	}


}