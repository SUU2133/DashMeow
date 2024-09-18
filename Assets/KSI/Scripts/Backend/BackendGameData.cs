using UnityEngine;
using BackEnd;
using UnityEngine.Events;

public class BackendGameData
{
	[System.Serializable]
	public class GameDataLoadEvent : UnityEvent { }
	public GameDataLoadEvent onGameDataLoadEvent = new GameDataLoadEvent();

	private static BackendGameData instance = null;
	public static BackendGameData Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new BackendGameData();
			}

			return instance;
		}
	}

	private UserGameData userGameData = new UserGameData();
	public UserGameData UserGameData => userGameData;

	private string gameDataRowInDate = string.Empty;

	public void GameDataInsert()
	{
		userGameData.Reset();

		Param param = new Param()
		{
			{"INFINITBestscore", userGameData.INFINITBestscore},
			{"sushi", userGameData.sushi},
			{"cannedFood", userGameData.cannedFood},
			{"silverKey", userGameData.silverKey},
			{"goldKey", userGameData.goldKey},
			{"money", userGameData.money},
			{"brokenBlue", userGameData.brokenBlue},
			{"brokenRed", userGameData.brokenRed},
			{"brokenGreen", userGameData.brokenGreen},

			{"resurrection", userGameData.resurrection},
			{"wizardHuntBackground", userGameData.wizardHuntBackground},
			{"wizardHuntEffect", userGameData.wizardHuntEffect},
			{"wizardHuntSkin", userGameData.wizardHuntSkin},
			{"nabinyangBackground", userGameData.nabinyangBackground},
			{"nabinyangEffect", userGameData.nabinyangEffect},
			{"nabinyangSkin", userGameData.nabinyangSkin}
	};

		Backend.GameData.Insert("USER_DATA", param, callback =>
		{
			if (callback.IsSuccess())
			{
				gameDataRowInDate = callback.GetInDate();

				Debug.Log($"게임 정보 데이터 삽입에 성공했습니다. : {callback}");
			}
			else
			{
				Debug.LogError($"게임 정보 데이터 삽입에 실패했습니다. : {callback}");
			}
		});
	}

	public void GameDataLoad()
	{
		Backend.GameData.GetMyData("USER_DATA", new Where(), callback =>
		{
			if (callback.IsSuccess())
			{
				Debug.Log($"게임 정보 데이터 불러오기에 성공했습니다. : {callback}");

				try
				{
					LitJson.JsonData gameDataJson = callback.FlattenRows();

					if (gameDataJson.Count <= 0)
					{
						Debug.LogWarning("데이터가 존재하지 않습니다.");
					}
					else
					{
						gameDataRowInDate = gameDataJson[0]["inDate"].ToString();

						userGameData.INFINITBestscore = int.Parse(gameDataJson[0]["INFINITBestscore"].ToString());						
						userGameData.cannedFood = int.Parse(gameDataJson[0]["cannedFood"].ToString());
						userGameData.silverKey = int.Parse(gameDataJson[0]["silverKey"].ToString());
						userGameData.goldKey = int.Parse(gameDataJson[0]["goldKey"].ToString());
						userGameData.money = int.Parse(gameDataJson[0]["money"].ToString());
						userGameData.brokenBlue = int.Parse(gameDataJson[0]["brokenBlue"].ToString());
						userGameData.brokenRed = int.Parse(gameDataJson[0]["brokenRed"].ToString());
						userGameData.brokenGreen = int.Parse(gameDataJson[0]["brokenGreen"].ToString());
						userGameData.sushi = int.Parse(gameDataJson[0]["sushi"].ToString());

						userGameData.resurrection = int.Parse(gameDataJson[0]["resurrection"].ToString());
						userGameData.wizardHuntBackground = int.Parse(gameDataJson[0]["wizardHuntBackground"].ToString());					
						userGameData.wizardHuntEffect = int.Parse(gameDataJson[0]["wizardHuntEffect"].ToString());
						userGameData.wizardHuntSkin = int.Parse(gameDataJson[0]["wizardHuntSkin"].ToString());
						userGameData.nabinyangBackground = int.Parse(gameDataJson[0]["nabinyangBackground"].ToString());
						userGameData.nabinyangEffect = int.Parse(gameDataJson[0]["nabinyangEffect"].ToString());
						userGameData.nabinyangSkin = int.Parse(gameDataJson[0]["nabinyangSkin"].ToString());
						SyncLocalAndServerData();
						onGameDataLoadEvent?.Invoke();

						// 각 데이터를 디버그 로그로 출력
						Debug.Log($"INFINITBestscore: {userGameData.INFINITBestscore}");
						Debug.Log($"Canned Food: {userGameData.cannedFood}");
						Debug.Log($"Silver Key: {userGameData.silverKey}");
						Debug.Log($"Gold Key: {userGameData.goldKey}");
						Debug.Log($"Money: {userGameData.money}");
						Debug.Log($"Broken Blue: {userGameData.brokenBlue}");
						Debug.Log($"Broken Red: {userGameData.brokenRed}");
						Debug.Log($"Broken Green: {userGameData.brokenGreen}");
						Debug.Log($"Sushi: {userGameData.sushi}");

						Debug.Log($"Resurrection: {userGameData.resurrection}");
						Debug.Log($"WizardHuntBackground: {userGameData.wizardHuntBackground}");
						Debug.Log($"WizardHuntEffect: {userGameData.wizardHuntEffect}");
						Debug.Log($"WizardHuntSkin: {userGameData.wizardHuntSkin}");
						Debug.Log($"NabinyangBackground: {userGameData.nabinyangBackground}");
						Debug.Log($"nabinyangEffect: {userGameData.nabinyangEffect}");
						Debug.Log($"NabinyangSkin: {userGameData.nabinyangSkin}");					
					}
				}
				catch (System.Exception e)
				{
					userGameData.Reset();
					Debug.LogError(e);
				}
			}
			else
			{
				Debug.LogError($"게임 정보 데이터 불러오기에 실패했습니다. : {callback}");
			}
		});
	}

	public void GameDataUpdate(UnityAction action = null)
	{
		if (userGameData == null)
		{
			Debug.LogError("서버에서 다운받거나 새로 삽입한 데이터가 존재하지 않습니다." +
						   "Insert 혹은 Load를 통해 데이터를 생성해주세요.");
			return;
		}
		SyncLocalAndServerData();
		Param param = new Param()
		{
			{"INFINITBestscore", userGameData.INFINITBestscore},
			{"sushi", userGameData.sushi},
			{"cannedFood", userGameData.cannedFood},
			{"silverKey", userGameData.silverKey},
			{"goldKey", userGameData.goldKey},
			{"money", userGameData.money},
			{"brokenBlue", userGameData.brokenBlue},
			{"brokenRed", userGameData.brokenRed},
			{"brokenGreen", userGameData.brokenGreen},

			{"resurrection", userGameData.resurrection},
			{"wizardHuntBackground", userGameData.wizardHuntBackground},
			{"wizardHuntEffect", userGameData.wizardHuntEffect},
			{"wizardHuntSkin", userGameData.wizardHuntSkin},
			{"nabinyangBackground", userGameData.nabinyangBackground},
			{"nabinyangEffect", userGameData.nabinyangEffect},
			{"nabinyangSkin", userGameData.nabinyangSkin}
		};

		if (string.IsNullOrEmpty(gameDataRowInDate))
		{
			Debug.LogError($"유저의 inDate 정보가 없어 게임 정보 데이터 수정에 실패했습니다.");
		}
		else
		{
			Debug.Log($"{gameDataRowInDate}의 게임 정보 데이터 수정을 요청합니다.");

			Backend.GameData.UpdateV2("USER_DATA", gameDataRowInDate, Backend.UserInDate, param, callback =>
			{
				if (callback.IsSuccess())
				{
					Debug.Log($"게임 정보 데이터 수정에 성공했습니다. : {callback}");

					action?.Invoke();

					onGameDataLoadEvent?.Invoke();
				}
				else
				{
					Debug.LogError($"게임 정보 데이터 수정에 실패했습니다. : {callback}");
				}
			});
		}
	}
	public void SyncLocalAndServerData()
	{
		// 서버 데이터와 로컬 데이터를 비교하여 동기화하는 로직 작성
		// 로컬 데이터가 서버 데이터와 다를 경우 서버 데이터를 덮어씁니다.
		if (userGameData.sushi != DataManager.Instance.sushi)
		{
			DataManager.Instance.sushi = userGameData.sushi;
		}
		if (userGameData.cannedFood != DataManager.Instance.cannedFood)
		{
			DataManager.Instance.cannedFood = userGameData.cannedFood;
		}
		if (userGameData.silverKey != DataManager.Instance.silverKey)
		{
			DataManager.Instance.silverKey = userGameData.silverKey;
		}
		if (userGameData.goldKey != DataManager.Instance.goldKey)
		{
			DataManager.Instance.goldKey = userGameData.goldKey;
		}
		if (userGameData.money != DataManager.Instance.money)
		{
			DataManager.Instance.money = userGameData.money;
		}
		if (userGameData.brokenBlue != DataManager.Instance.brokenBlue)
		{
			DataManager.Instance.brokenBlue = userGameData.brokenBlue;
		}
		if (userGameData.brokenRed != DataManager.Instance.brokenRed)
		{
			DataManager.Instance.brokenRed = userGameData.brokenRed;
		}
		if (userGameData.brokenGreen != DataManager.Instance.brokenGreen)
		{
			DataManager.Instance.brokenGreen = userGameData.brokenGreen;
		}
		if (userGameData.resurrection != DataManager.Instance.resurrection)
		{
			DataManager.Instance.resurrection = userGameData.resurrection;
		}
		//마법냥
		if( userGameData.wizardHuntBackground != DataManager.Instance.magicCatWallpaper)
		{
			DataManager.Instance.magicCatWallpaper = userGameData.wizardHuntBackground;
		}
		if(userGameData.wizardHuntEffect != DataManager.Instance.magicCatEffect)
		{
			DataManager.Instance.magicCatEffect = DataManager.Instance.magicCatEffect;
		}
		if(userGameData.wizardHuntSkin != DataManager.Instance.magicCatSkin)
		{
			DataManager.Instance.magicCatSkin = userGameData.wizardHuntSkin;
		}
		//나비냥
		if(userGameData.nabinyangBackground != DataManager.Instance.butterflyCatWallpaper)
		{
			DataManager.Instance.butterflyCatWallpaper = userGameData.nabinyangBackground;
		}
		if (userGameData.nabinyangEffect != DataManager.Instance.butterflyCatEffect)
		{
			DataManager.Instance.butterflyCatEffect = userGameData.nabinyangEffect;
		}
		if (userGameData.nabinyangSkin != DataManager.Instance.butterflyCatSkin)
		{
			DataManager.Instance.butterflyCatSkin = userGameData.nabinyangSkin;
		}

		// 추가적으로 필요한 데이터 동기화 로직 작성
	}
	public void UpdateServerDataFromLocal()
	{
		// 로컬 데이터를 서버 데이터로 업데이트
		Param param = new Param();

		// 각 데이터 항목을 비교하여 변경된 항목만 업데이트 파라미터에 추가
		if (userGameData.sushi != DataManager.Instance.sushi)
		{
			param.Add("sushi", DataManager.Instance.sushi);
			userGameData.sushi = DataManager.Instance.sushi;
		}
		if (userGameData.cannedFood != DataManager.Instance.cannedFood)
		{
			param.Add("cannedFood", DataManager.Instance.cannedFood);
			userGameData.cannedFood = DataManager.Instance.cannedFood;
		}
		if (userGameData.silverKey != DataManager.Instance.silverKey)
		{
			param.Add("silverKey", DataManager.Instance.silverKey);
			userGameData.silverKey = DataManager.Instance.silverKey;
		}
		if (userGameData.goldKey != DataManager.Instance.goldKey)
		{
			param.Add("goldKey", DataManager.Instance.goldKey);
			userGameData.goldKey = DataManager.Instance.goldKey;
		}
		if (userGameData.money != DataManager.Instance.money)
		{
			param.Add("money", DataManager.Instance.money);
			userGameData.money = DataManager.Instance.money;
		}
		if (userGameData.brokenBlue != DataManager.Instance.brokenBlue)
		{
			param.Add("brokenBlue", DataManager.Instance.brokenBlue);
			userGameData.brokenBlue = DataManager.Instance.brokenBlue;
		}
		if (userGameData.brokenRed != DataManager.Instance.brokenRed)
		{
			param.Add("brokenRed", DataManager.Instance.brokenRed);
			userGameData.brokenRed = DataManager.Instance.brokenRed;
		}
		if (userGameData.brokenGreen != DataManager.Instance.brokenGreen)
		{
			param.Add("brokenGreen", DataManager.Instance.brokenGreen);
			userGameData.brokenGreen = DataManager.Instance.brokenGreen;
		}
		if (userGameData.resurrection != DataManager.Instance.resurrection)
		{
			param.Add("resurrection", DataManager.Instance.resurrection);
			userGameData.resurrection = DataManager.Instance.resurrection;
		}

		// 추가적으로 필요한 데이터 업데이트 로직 작성

		if (param.Count > 0)
		{
			if (string.IsNullOrEmpty(gameDataRowInDate))
			{
				Debug.LogError($"유저의 inDate 정보가 없어 게임 정보 데이터 수정에 실패했습니다.");
			}
			else
			{
				Debug.Log($"{gameDataRowInDate}의 게임 정보 데이터 수정을 요청합니다.");

				Backend.GameData.UpdateV2("USER_DATA", gameDataRowInDate, Backend.UserInDate, param, callback =>
				{
					if (callback.IsSuccess())
					{
						Debug.Log($"게임 정보 데이터 수정에 성공했습니다. : {callback}");
					}
					else
					{
						Debug.LogError($"게임 정보 데이터 수정에 실패했습니다. : {callback}");
					}
				});
			}
		}
	}
}

