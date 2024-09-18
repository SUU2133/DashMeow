using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;

public class UserInfo : MonoBehaviour
{
	[System.Serializable]
	public class UserInfoEvent : UnityEngine.Events.UnityEvent { }
	public UserInfoEvent onUserInfoEvent = new UserInfoEvent();

	private static UserInfoData data = new UserInfoData();
	public static UserInfoData Data => data;

	public void GetUserInfoFromBackend()
	{
		Backend.BMember.GetUserInfo(callback =>
		{
			if (callback.IsSuccess())
			{
				try
				{
					JsonData json = callback.GetReturnValuetoJSON()["row"];

					data.gamerId = json["gamerId"].ToString();
					data.countryCode = json["countryCode"]?.ToString();
					data.nickname = json["nickname"]?.ToString();
					data.inDate = json["inDate"].ToString();
					data.emailForFindPassword = json["emailForFindPassword"]?.ToString();
					data.subscriptionType = json["subscriptionType"].ToString();
					data.federationId = json["federationId"]?.ToString();
				}
				catch (System.Exception e)
				{
					data.Reset();

					Debug.LogError(e);
				}
			}
			else
			{
				data.Reset();
				Debug.LogError(callback.GetMessage());
			}

			onUserInfoEvent?.Invoke();
		});
	}
}

public class UserInfoData
{
	public string gamerId;              // 유저의 gamerID
	public string countryCode;          // 국가코드. 설정 안했으면 null
	public string nickname;             // 닉네임. 설정 안했으면 null
	public string inDate;               // 유저의 inDate
	public string emailForFindPassword; // 이메일주소. 설정 안했으면 null
	public string subscriptionType;     // 커스텀, 페더레이션 타입
	public string federationId;         // 구글, 애플, 페이스북 페더레이션 ID. 커스텀 계정은 null

	public void Reset()
	{
		gamerId = "Offline";
		countryCode = "Unknown";
		nickname = "Noname";
		inDate = string.Empty;
		emailForFindPassword = string.Empty;
		subscriptionType = string.Empty;
		federationId = string.Empty;
	}
}
