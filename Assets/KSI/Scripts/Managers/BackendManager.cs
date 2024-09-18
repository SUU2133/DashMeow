using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System.Threading.Tasks;
using TMPro;
using static BackEnd.Backend;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class BackendManager : MonoBehaviour
{
	//public TextMeshProUGUI uiText;

	private void Start()
	{
		BackendCustomSetting settings = new BackendCustomSetting();
		settings.clientAppID = "0c21fa70-5305-11ef-af9d-db301a4783ec8093";
		settings.signatureKey = "0c222180-5305-11ef-af9d-db301a4783ec8093";
		settings.functionAuthKey = "AQICAHiT9pph8qngOxVNCJXRC0BZjtURNwhhts4F69FL8VO4dgHzijgayqKy88FRGb2AdcJbAAAAYjBgBgkqhkiG9w0BBwagUzBRAgEAMEwGCSqGSIb3DQEHATAeBglghkgBZQMEAS4wEQQMctJLbx8w5C1W4UvjAgEQgB9sAZws0RjaIIFuJcepkSQPjamqIWZ4sICTZwbWqUjr";
		settings.isSendLogReport = true;
		settings.timeOutSec = 100;

		// 뒤끝 초기화 (콜백 함수 풀링을 사용하려면 매개변수를 true로 설정)
		var bro = Backend.Initialize(settings);

		// 뒤끝 초기화에 대한 응답값
		if (bro.IsSuccess())
		{
			// 초기화 성공 시 statusCode 204 Success
			Debug.Log($"초기화 성공 : {bro}");
			//UpdateText($"초기화 성공 : {bro}");
		}
		else
		{
			// 초기화 실패 시 statusCode 400대 에러 발생
			Debug.LogError($"초기화 실패 : {bro}");
			//UpdateText($"초기화 실패 : {bro}");
		}

		//AutoLogin();
	}


	public void AutoLogin()
	{
		BackendReturnObject bro = Backend.BMember.LoginWithTheBackendToken();
		if (bro.IsSuccess())
		{
			Debug.Log("자동 로그인에 성공했습니다");
		}
		else
		{
			Debug.Log("로그인에 실패했습니다");
		}
	}


	// 로그아웃
	public void CustomLogOut()
	{
		Backend.BMember.Logout();
		Debug.Log("로그아웃 성공");
		//KSJSoundManager.Instance.StopBGM();
		UnitySceneManager.LoadScene("LoginScene");
		GameManager.Scene.LoadLOBBY();
		KSJSoundManager.Instance.StopBGM();
	}
	
	// 탈퇴
	public void CustomSignOut()
	{
		Backend.BMember.WithdrawAccount();
		Debug.Log("탈퇴 성공");
		//KSJSoundManager.Instance.StopBGM();
		UnitySceneManager.LoadScene("LoginScene");
		GameManager.Scene.LoadLOBBY();
		KSJSoundManager.Instance.StopBGM();
	}

}


