using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class BackendLogin : BackendLoginBase
{
	[SerializeField] private Image imageID; // ID 필드 색상 변경
	[SerializeField] private TMP_InputField inputFieldID; // ID 필드 텍스트 정보 추출
	[SerializeField] private Image imagePW; // PW 필드 색상 변경
	[SerializeField] private TMP_InputField inputFieldPW; // PW 필드 텍스트 정보 추출
	[SerializeField] private Button btnLogin; // 로그인 버튼 (상호작용 가능/불가능)

	public void OnClickLogin()
	{
		ResetUI(imageID, imagePW);

		if (IsFieldDataEmpty(imageID, inputFieldID.text, "ID")) return;
		if (IsFieldDataEmpty(imagePW, inputFieldPW.text, "PW")) return;

		btnLogin.interactable = false;

		StartCoroutine(nameof(LoginProcessRoutine));

		ResponseToLogin(inputFieldID.text, inputFieldPW.text);

		KSJSoundManager.Instance.Init();
	}

	private void ResponseToLogin(string ID, string PW)
	{
		Backend.BMember.CustomLogin(ID, PW, callback =>
		{
			StopCoroutine(nameof(LoginProcessRoutine));

			if (callback.IsSuccess())
			{
				SetMessage($"환영합니다. \"{inputFieldID.text}\".");
				Debug.Log($"유저 닉네임 : " + Backend.UserNickName);
				Debug.Log($"유저 인데이트 : " + Backend.UserInDate);
				Debug.Log($"유저 UID(쿠폰용) : " + Backend.UID);
				//Account.Inst.UserDataGet();
				SceneManager.LoadScene("LobbyScene");
				GameManager.Scene.LoadLOBBY();
			}
			else
			{
				btnLogin.interactable = true;

				string message = string.Empty;

				switch (int.Parse(callback.GetStatusCode()))
				{
					case 401:
						message = callback.GetMessage().Contains("customId") ? "ID가 존재하지 않습니다." : "잘못된 PW입니다.";
						break;
					case 403:
						message = callback.GetMessage().Contains("user") ? "차단된 계정입니다." : "차단된 기기입니다.";
						break;
					case 410:
						message = "탈퇴 진행 중입니다.";
						break;
					default:
						message = callback.GetMessage();
						break;
				}

				if (message.Contains("password"))
				{
					GuideForIncorrectlyEnteredData(imagePW, message);
				}
				else
				{
					GuideForIncorrectlyEnteredData(imageID, message);
				}
			}
		});
	}

	private IEnumerator LoginProcessRoutine()
	{
		float time = 0;

		while (true)
		{
			time += Time.deltaTime;

			SetMessage($"로그인 중... {time:F1}초");
			yield return null;
		}
	}

	public void ClearUI()
	{
		SetMessage("");

		inputFieldID.text = "";
		inputFieldPW.text = "";
	}
}