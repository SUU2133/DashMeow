using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class FindPW : BackendLoginBase
{
	[SerializeField] private Image imageID; // ID 필드 색상 변경
	[SerializeField] private TMP_InputField inputFieldID; // ID 필드 텍스트 정보 추출
	[SerializeField] private Image imageEmail; // E-mail 필드 색상 변경
	[SerializeField] private TMP_InputField inputFieldEmail; // E-mail 필드 텍스트 정보 추출
	[SerializeField] private Button btnFindPW; // "비밀번호 찾기" 버튼 (상호작용 가능/불가능)

	public void OnClickFindPW()
	{
		ResetUI(imageID, imageEmail);

		if (IsFieldDataEmpty(imageID, inputFieldID.text, "아이디")) return;
		if (IsFieldDataEmpty(imageEmail, inputFieldEmail.text, "메일 주소")) return;

		if (!inputFieldEmail.text.Contains("@"))
		{
			GuideForIncorrectlyEnteredData(imageEmail, "메일 형식이 잘못되었습니다.(ex. address@xx.xx)");
			return;
		}

		btnFindPW.interactable = false;
		SetMessage("메일 발송중입니다.");

		FindCustomPW();
	}

	private void FindCustomPW()
	{
		Backend.BMember.ResetPassword(inputFieldID.text, inputFieldEmail.text, callback =>
		{
			btnFindPW.interactable = true;

			if (callback.IsSuccess())
			{
				SetMessage($"{inputFieldEmail.text} 주소로 메일을 발송하였습니다.");
			}
			else
			{
				string message = string.Empty;

				switch (int.Parse(callback.GetStatusCode()))
				{
					case 404:
						message = "해당 이메일을 사용하는 사용자가 없습니다.";
						break;
					case 429:
						message = "24시간 이내에 5회 이상 아이디/비밀번호 찾기를 시도했습니다.";
						break;
					default:
						message = callback.GetMessage();
						break;
				}

				if (message.Contains("이메일"))
				{
					GuideForIncorrectlyEnteredData(imageEmail, message);
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

		inputFieldEmail.text = "";
		inputFieldID.text = "";
		imageID.color = Color.white;
		imageEmail.color = Color.white;

		btnFindPW.interactable = true;
	}
}
