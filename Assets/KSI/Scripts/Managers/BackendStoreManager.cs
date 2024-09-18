using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackendStoreManager : MonoBehaviour
{
	protected string Key { get; private set; }
	protected string gameDataRowInDate { get; private set; }

	public BackendStoreManager(string key)
	{
		Key = key;
		gameDataRowInDate = string.Empty;
	}

	public BackendStoreManager(string key, LitJson.JsonData userData)
	{
		Key = key;
		gameDataRowInDate = userData[0]["inDate"].ToString();
	}

	protected virtual Param GetAllData()
	{
		return null;
	}

	public void UpdateFlagSet()
	{
		Debug.Log("Updating flag set for data with key: " + Key);

		BackendReturnObject bro = null;

		if (string.IsNullOrEmpty(gameDataRowInDate))
		{
			Debug.Log("내 제일 최신 게임 정보 데이터 수정을 요청합니다.");

			bro = Backend.GameData.Update(Key, new Where(), GetAllData());

			if (!bro.IsSuccess())
			{
				bro = Backend.GameData.Insert(Key, GetAllData());

				if (bro.IsSuccess())
				{
					Debug.Log("Data saved successfully to Backend server");
				}
				else
				{
					Debug.LogError("Failed to save data to Backend server: " + bro.ToString());
				}
			}
		}
		else
		{
			Debug.Log($"{gameDataRowInDate}의 게임 정보 데이터 수정을 요청합니다.");

			bro = Backend.GameData.UpdateV2(Key, gameDataRowInDate, Backend.UserInDate, GetAllData());
		}
	}
}
