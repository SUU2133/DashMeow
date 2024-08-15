using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIMapping : MonoBehaviour
{
	// DataManager의 인스턴스를 캐싱
	private DataManager dataManager;
	//앱 종료 시간
	private const string LastExitTimeKey = "LastExitTime";

	[Header("# UI Mapping")]
	// 인게임 재화
	public TMP_Text sushiText;
	public TMP_Text silverKeyText;
	public TMP_Text goldKeyText;

	public TMP_Text silverTimeKeyText;
	public TMP_Text goldKeyTimeText;
	public TMP_Text cannedFoodText;
	// 스탯창에 표시되는 텍스트
	public TMP_Text maxHealthText;
	public TMP_Text speedText;
	public TMP_Text jumpForceText;
	public TMP_Text glideTimeText;
	public TMP_Text jumpCountText;

	// 염원 Lv, 이름, 성능과 잔여개수
	public TMP_Text redMarble;
	public TMP_Text brokenRedText;

	public TMP_Text blueMarble;
	public TMP_Text brokenBlueText;

	public TMP_Text greenMarble;
	public TMP_Text brokenGreenText;

	// 각각의 강화버튼과 텍스트
	public Button upgradeRedGlide;
	public TMP_Text needRedSushi;

	public Button upgradeBlueHP;
	public TMP_Text needBlueSushi;

	public Button upgradeGreenCount;
	public TMP_Text needGreenSushi;

	// 슬라이더


	public Slider brokenRedSlider;
	public Slider brokenBlueSlider;
	public Slider brokenGreenSlider;

	// 일일 상점 초기화 시간 텍스트
	public TMP_Text dayTimeText;
	public TMP_Text silverkeyTimeText;
	public TMP_Text goldkeyTimeText;

	// 일일 상점 리셋 횟수
	public TMP_Text IngameResetText;
	public TMP_Text AdvResetText;
	public ShopList shopList;

	private const string LastSilverKeyTimeKey = "LastSilverKeyTime";
	private const string LastGoldKeyTimeKey = "LastGoldKeyTime";

	private const int SilverKeyInterval = 600; // 10분 (600초)
	private const int GoldKeyInterval = 1800;  // 30분 (1800초)

	void Start()
	{
		shopList = FindObjectOfType<ShopList>();
		// DataManager 인스턴스 캐싱
		dataManager = DataManager.Instance;
		CheckDayPassed();
		InvokeRepeating("UpdateDayTime", 0f, 1f);
		InvokeRepeating("UpdateKeyGeneration", 0f, 1f);
		//InitializeUI();
	}

	void Update()
	{
		// 최대치 관리
		if (dataManager.sushi > dataManager.sushiMax)
			dataManager.sushi = dataManager.sushiMax + 1;
		if (dataManager.silverKey > dataManager.maxSilverKey)
			dataManager.silverKey = dataManager.maxSilverKey;
		
		UpdateCatsDesire();
		UpdateCharacterUI();
		UpdateLobbyUI();
		UpdateUpgrade();
		UpdateSliderValue();

		UpdateResetText();
	}

	public void UpdateCharacterUI()
	{
		// 스탯창
		maxHealthText.text = "체력 : " + (dataManager.maxHealth + dataManager.redMarbleValue[dataManager.redMarbleLv]);
		glideTimeText.text = "활공 시간 : " + (dataManager.glideTime + dataManager.greenMarbleValue[dataManager.greenMarbleLv]);
		speedText.text = "속도 : " + dataManager.speed;
		jumpForceText.text = "점프력 : " + dataManager.jumpForce;
		jumpCountText.text = "점프 횟수 : " + dataManager.maxJumpCount;

		// 염원 잔여/필요
		brokenBlueText.text = "" + dataManager.brokenBlue + "/" + dataManager.nextExp[dataManager.blueMarbleLv];
		brokenRedText.text = "" + dataManager.brokenRed + "/" + dataManager.nextExp[dataManager.redMarbleLv];
		brokenGreenText.text = "" + dataManager.brokenGreen + "/" + dataManager.nextExp[dataManager.greenMarbleLv];

		// 생선 잔여/필요
		if (dataManager.redMarbleLv == 10)
			needRedSushi.text = "강화 완료";
		else
			needRedSushi.text = "강화    " + dataManager.nextSushi[dataManager.redMarbleLv];

		if (dataManager.blueMarbleLv == 10)
			needBlueSushi.text = "강화 완료";
		else
			needBlueSushi.text = "강화    " + dataManager.nextSushi[dataManager.blueMarbleLv];

		if (dataManager.greenMarbleLv == 10)
			needGreenSushi.text = "강화 완료";
		else
			needGreenSushi.text = "강화    " + dataManager.nextSushi[dataManager.greenMarbleLv];
	}

	public void UpdateCatsDesire()
	{
		redMarble.text = "Lv." + dataManager.redMarbleLv + " 최대 체력 " + dataManager.redMarbleValue[dataManager.redMarbleLv] + " 증가";
		blueMarble.text = "Lv." + dataManager.blueMarbleLv + " 모든 피해 " + dataManager.blueMarbleValue[dataManager.blueMarbleLv] + " 감소";
		greenMarble.text = "Lv. " + dataManager.greenMarbleLv + " 활공  " + dataManager.greenMarbleValue[dataManager.greenMarbleLv] + "초 증가";
	}

	public void UpdateUpgrade()
	{
		if (dataManager.brokenBlue < dataManager.nextExp[dataManager.blueMarbleLv] || dataManager.sushi < dataManager.nextSushi[dataManager.blueMarbleLv] || dataManager.blueMarbleLv == 11)
		{
			upgradeBlueHP.GetComponent<Image>().color = Color.gray;
			upgradeBlueHP.interactable = false; // 버튼 비활성화
		}
		else
		{
			upgradeBlueHP.GetComponent<Image>().color = Color.white;
			upgradeBlueHP.interactable = true; // 버튼 활성화
		}

		if (dataManager.brokenRed < dataManager.nextExp[dataManager.redMarbleLv] || dataManager.sushi < dataManager.nextSushi[dataManager.redMarbleLv] || dataManager.redMarbleLv == 11)
		{
			upgradeRedGlide.GetComponent<Image>().color = Color.gray;
			upgradeRedGlide.interactable = false; // 버튼 비활성화
		}
		else
		{
			upgradeRedGlide.GetComponent<Image>().color = Color.white;
			upgradeRedGlide.interactable = true; // 버튼 활성화
		}

		if (dataManager.brokenGreen < dataManager.nextExp[dataManager.greenMarbleLv] || dataManager.sushi < dataManager.nextSushi[dataManager.greenMarbleLv] || dataManager.greenMarbleLv == 11)
		{
			upgradeGreenCount.GetComponent<Image>().color = Color.gray;
			upgradeGreenCount.interactable = false; // 버튼 비활성화
		}
		else
		{
			upgradeGreenCount.GetComponent<Image>().color = Color.white;
			upgradeGreenCount.interactable = true; // 버튼 활성화
		}
	}

	public void UpgradeButton(int color)
	{
		if (color == 1) // 적
		{
			if (dataManager.brokenRed >= dataManager.nextExp[dataManager.redMarbleLv] && dataManager.sushi >= dataManager.nextSushi[dataManager.redMarbleLv] && dataManager.redMarbleLv < 10)
			{
				dataManager.brokenRed -= dataManager.nextExp[dataManager.redMarbleLv];
				dataManager.sushi -= dataManager.nextSushi[dataManager.redMarbleLv];
				dataManager.redMarbleLv++;
			}
		}
		if (color == 2) // 청
		{
			if (dataManager.brokenBlue >= dataManager.nextExp[dataManager.blueMarbleLv] && dataManager.sushi >= dataManager.nextSushi[dataManager.blueMarbleLv] && dataManager.blueMarbleLv < 10)
			{
				dataManager.brokenBlue -= dataManager.nextExp[dataManager.blueMarbleLv];
				dataManager.sushi -= dataManager.nextSushi[dataManager.blueMarbleLv];
				dataManager.blueMarbleLv++;
			}
		}
		if (color == 3) // 녹
		{
			if (dataManager.brokenGreen >= dataManager.nextExp[dataManager.greenMarbleLv] && dataManager.sushi >= dataManager.nextSushi[dataManager.greenMarbleLv] && dataManager.greenMarbleLv < 10)
			{
				dataManager.brokenGreen -= dataManager.nextExp[dataManager.greenMarbleLv];
				dataManager.sushi -= dataManager.nextSushi[dataManager.greenMarbleLv];
				dataManager.greenMarbleLv++;
			}
		}
		DataManager.Instance.SaveDataToJson();
	}

	public void UpdateSliderValue()
	{


		brokenBlueSlider.value = (float)dataManager.brokenBlue / dataManager.nextExp[dataManager.blueMarbleLv];
		brokenRedSlider.value = (float)dataManager.brokenRed / dataManager.nextExp[dataManager.redMarbleLv];
		brokenGreenSlider.value = (float)dataManager.brokenGreen / dataManager.nextExp[dataManager.greenMarbleLv];
	}

	public void UpdateLobbyUI()
	{
		sushiText.text = "" + dataManager.sushi;
		silverKeyText.text = "" + dataManager.silverKey + "/30";
		goldKeyText.text = "" + dataManager.goldKey +"/5";
		cannedFoodText.text = "" + dataManager.cannedFood;
	}

	public void UpdateDayTime()
	{
		DateTime now = DateTime.Now;
		DateTime midnight = now.Date.AddDays(1); // 자정의 DateTime 객체 생성
		TimeSpan timeRemaining = midnight - now; // 남은 시간 계산
												 //Debug.Log((int)timeRemaining.TotalSeconds);

		if ((int)timeRemaining.TotalSeconds == 0) // 
		{
			DataManager.Instance.resetCannedNum = 0;
			DataManager.Instance.resetNum = 0;
			DataManager.Instance.advResetNum = 0;
			DataManager.Instance.freeSushi = 0;
			shopList.PickRandomItems(1);
			shopList.DisplayRandomItems();
			DataManager.Instance.resetCannedNum = 0;
			DataManager.Instance.resetNum = 0;
			DataManager.Instance.advResetNum = 0;
		}

		// 시간, 분, 초 형식으로 표시
		dayTimeText.text = string.Format("초기화 시간 : {0:D2}:{1:D2}:{2:D2}",
												timeRemaining.Hours,
												timeRemaining.Minutes,
												timeRemaining.Seconds);


	}

	public void UpdateResetText()
	{
		IngameResetText.text = "10\n" + dataManager.resetNum + "/" + dataManager.resetMaxNum;
		AdvResetText.text = "500\n" + dataManager.resetCannedNum + "/" + dataManager.resetCannedNumMax;
	}
	void CheckDayPassed()
	{
		// 마지막 종료 시간을 불러오기
		string lastExitTimeStr = PlayerPrefs.GetString(LastExitTimeKey, string.Empty);

		if (!string.IsNullOrEmpty(lastExitTimeStr))
		{
			DateTime lastExitTime = DateTime.Parse(lastExitTimeStr);

			// 현재 시간과 마지막 종료 시간을 비교
			if (DateTime.Now.Date > lastExitTime.Date)
			{
				// 하루가 지났다면 데이터 초기화
				DataManager.Instance.resetCannedNum = 0;
				DataManager.Instance.resetNum = 0;
				DataManager.Instance.advResetNum = 0;
				DataManager.Instance.freeSushi = 0;
				shopList.PickRandomItems(1);
				shopList.DisplayRandomItems();
				DataManager.Instance.resetCannedNum = 0;
				DataManager.Instance.resetNum = 0;
				DataManager.Instance.advResetNum = 0;
				DataManager.Instance.freeSushi = 0;
				
				Debug.Log("데이터가 하루 지남으로 초기화되었습니다.");
			}
		}
	}
	void OnApplicationQuit()
	{
		PlayerPrefs.SetString(LastExitTimeKey, DateTime.Now.ToString());
		PlayerPrefs.SetString(LastSilverKeyTimeKey, DateTime.Now.ToString());
		PlayerPrefs.SetString(LastGoldKeyTimeKey, DateTime.Now.ToString());

		PlayerPrefs.Save();
	}
	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			PlayerPrefs.SetString(LastExitTimeKey, DateTime.Now.ToString());
			PlayerPrefs.SetString(LastSilverKeyTimeKey, DateTime.Now.ToString());
			PlayerPrefs.SetString(LastGoldKeyTimeKey, DateTime.Now.ToString());
			PlayerPrefs.Save();
		}
		else
		{
			CheckDayPassed();
		}

		
	}

	void UpdateKeyGeneration()
	{
		DateTime now = DateTime.Now;

		// 실버 키 처리

		if (dataManager.silverKey < dataManager.maxSilverKey)
		{
			DateTime lastSilverKeyTime = DateTime.Parse(PlayerPrefs.GetString(LastSilverKeyTimeKey, now.ToString()));
			TimeSpan silverKeyTimeSpan = now - lastSilverKeyTime;

			if (silverKeyTimeSpan.TotalSeconds >= SilverKeyInterval)
			{
				int keysToAdd = (int)(silverKeyTimeSpan.TotalSeconds / SilverKeyInterval);
				dataManager.silverKey = Mathf.Min(dataManager.silverKey + keysToAdd, dataManager.maxSilverKey);
				PlayerPrefs.SetString(LastSilverKeyTimeKey, now.ToString());
				DataManager.Instance.SaveDataToJson();
				silverKeyTimeSpan = TimeSpan.Zero; // 실버 키가 생성되었으므로 남은 시간을 0으로 초기화
			}

			UpdateSilverKeyTimerUI(silverKeyTimeSpan); // 실버 키 타이머 UI 갱신
		}
		else
		{
			silverTimeKeyText.text = "실버 키: 최대치 도달";
		}

		// 골드 키 처리
		if (dataManager.goldKey < dataManager.maxGoldKey)
		{
			DateTime lastGoldKeyTime = DateTime.Parse(PlayerPrefs.GetString(LastGoldKeyTimeKey, now.ToString()));
			TimeSpan goldKeyTimeSpan = now - lastGoldKeyTime;

			if (goldKeyTimeSpan.TotalSeconds >= GoldKeyInterval)
			{
				int keysToAdd = (int)(goldKeyTimeSpan.TotalSeconds / GoldKeyInterval);
				dataManager.goldKey = Mathf.Min(dataManager.goldKey + keysToAdd, dataManager.maxGoldKey);
				PlayerPrefs.SetString(LastGoldKeyTimeKey, now.ToString());
				DataManager.Instance.SaveDataToJson();
				goldKeyTimeSpan = TimeSpan.Zero; // 골드 키가 생성되었으므로 남은 시간을 0으로 초기화
			}

			UpdateGoldKeyTimerUI(goldKeyTimeSpan); // 골드 키 타이머 UI 갱신
		}
		else
		{
			goldKeyTimeText.text = "골드 키: 최대치 도달";
		}

		UpdateLobbyUI(); // 열쇠 갱신을 UI에 반영
	}
	void UpdateSilverKeyTimerUI(TimeSpan silverKeyTimeSpan)

	{
		// 실버 키 타이머 업데이트
		double silverTimeLeft = SilverKeyInterval - silverKeyTimeSpan.TotalSeconds;
		if (silverTimeLeft > 0)
		{
			TimeSpan silverTimeRemaining = TimeSpan.FromSeconds(silverTimeLeft);

			silverTimeKeyText.text = $"실버 키: {silverTimeRemaining.Minutes:D2}:{silverTimeRemaining.Seconds:D2} 후 생성";
		}
		else
		{
			silverTimeKeyText.text = "실버 키: 생성됨";
		}
	}

	void UpdateGoldKeyTimerUI(TimeSpan goldKeyTimeSpan)
	{

	
		// 골드 키 타이머 업데이트
		double goldTimeLeft = GoldKeyInterval - goldKeyTimeSpan.TotalSeconds;
		if (goldTimeLeft > 0)
		{
			TimeSpan goldTimeRemaining = TimeSpan.FromSeconds(goldTimeLeft);

			goldKeyTimeText.text = $"골드 키: {goldTimeRemaining.Minutes:D2}:{goldTimeRemaining.Seconds:D2} 후 생성";
		}
		else
		{
			goldKeyTimeText.text = "골드 키: 생성됨";

		}
	}

}