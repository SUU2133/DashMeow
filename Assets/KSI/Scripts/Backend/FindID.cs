using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class FindID : BackendLoginBase
{
	[SerializeField] private Image imageEmail; // E-mail 필드 색상 변경
	[SerializeField] private TMP_InputField inputFieldEmail; // E-mail 필드 텍스트 정보 추출
	[SerializeField] private Button btnFindID; // "아이디 찾기" 버튼 (상호작용 가능/불가능)

	public void OnClickFindID()
	{
		ResetUI(imageEmail);

		if (IsFieldDataEmpty(imageEmail, inputFieldEmail.text, "메일 주소"))
			return;

		if (!inputFieldEmail.text.Contains("@"))
		{
			GuideForIncorrectlyEnteredData(imageEmail, "메일 형식이 잘못되었습니다.(ex. address@xx.xx)");
			return;		
		}

		btnFindID.interactable = false;
		SetMessage("메일 발송중입니다.");

		FindCustomID();
	}

	private void FindCustomID()
	{
		Backend.BMember.FindCustomID(inputFieldEmail.text, callback =>
		{
			btnFindID.interactable = true;

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

		btnFindID.interactable = true;
	}
}
