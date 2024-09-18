using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SignUp : BackendLoginBase
{
	[SerializeField] private Image imageID; // ID 필드 색상 변경
	[SerializeField] private TMP_InputField inputFieldID; // ID 필드 텍스트 정보 추출
	[SerializeField] private Image imagePW; // PW 필드 색상 변경
	[SerializeField] private TMP_InputField inputFieldPW; // PW 필드 텍스트 정보 추출
	[SerializeField] private Image imageConfirmPW; // Confirm PW 필드 색상 변경
	[SerializeField] private TMP_InputField inputFieldConfirmPW; // Confirm PW 필드 텍스트 정보 추출
	[SerializeField] private Image imageEmail; // E-mail 필드 색상 변경
	[SerializeField] private TMP_InputField inputFieldEmail;  // E-mail 필드 텍스트 정보 추출
	[SerializeField] private Image imageNickname; // Nickname 필드 색상 변경
	[SerializeField] private TMP_InputField inputFieldNickname; // Nickname 필드 텍스트 정보 추출
	[SerializeField] private Button signUpButton; // "계정 생성" 버튼 (상호작용 가능/불가능)

	public void OnClickSignUp()
	{
		ResetUI(imageID, imagePW, imageConfirmPW, imageEmail, imageNickname);

		if (IsFieldDataEmpty(imageID, inputFieldID.text, "아이디")) return;
		if (IsFieldDataEmpty(imagePW, inputFieldPW.text, "비밀번호")) return;
		if (IsFieldDataEmpty(imageConfirmPW, inputFieldConfirmPW.text, "비밀번호 확인")) return;
		if (IsFieldDataEmpty(imageEmail, inputFieldEmail.text, "메일 주소")) return;
		if (IsFieldDataEmpty(imageNickname, inputFieldNickname.text, "닉네임")) return;

		if (!inputFieldPW.text.Equals(inputFieldConfirmPW.text))
		{
			GuideForIncorrectlyEnteredData(imageConfirmPW, "비밀번호가 일치하지 않습니다.");
			return;
		}

		if (!inputFieldEmail.text.Contains("@"))
		{
			GuideForIncorrectlyEnteredData(imageEmail, "메일 형식이 잘못되었습니다.");
			return;
		}

		signUpButton.interactable = false;
		SetMessage("계정 생성중입니다.");

		CustomSignUp();
	}

	// 커스텀 회원 가입
	private void CustomSignUp()
	{
		Backend.BMember.CustomSignUp(inputFieldID.text, inputFieldPW.text, callback =>
		{
			signUpButton.interactable = true;
			
			// 계정 생성 성공
			if (callback.IsSuccess())
			{
				Backend.BMember.UpdateCustomEmail(inputFieldEmail.text, callback =>
				{
					if (callback.IsSuccess())
					{
						Backend.BMember.CreateNickname(inputFieldNickname.text, callback =>
						{
							if (callback.IsSuccess())
							{
								SetMessage($"계정 생성 성공. {inputFieldID.text}님 환영합니다.");
								ClearUI();

								// 계정 생성에 성공했을 때 해당 계정의 게임 정보 생성
								BackendGameData.Instance.GameDataInsert();
		
								UnitySceneManager.LoadScene("LobbyScene");
								GameManager.Scene.LoadLOBBY();
								Time.timeScale = 1f;
							}
						});
					}
				});
			}
			// 계정 생성 실패
			else
			{
				string message = string.Empty;

				switch (int.Parse(callback.GetStatusCode()))
				{
					case 409:
						message = "이미 존재하는 아이디입니다.";
						break;
					case 403:
						message = callback.GetMessage();
						break;
					case 401: 
					case 400:
					default:
						message = callback.GetMessage();
						break;
				}

				if (message.Contains("아이디"))
				{
					GuideForIncorrectlyEnteredData(imageID, message);
				}
				else
				{
					SetMessage(message);
				}
			}
		});
	}

	public void ClearUI()
	{
		SetMessage("");

		inputFieldID.text = "";
		inputFieldNickname.text = "";
		inputFieldPW.text = "";
		inputFieldConfirmPW.text = "";
		inputFieldEmail.text = "";

		signUpButton.interactable = true;
	}
}
