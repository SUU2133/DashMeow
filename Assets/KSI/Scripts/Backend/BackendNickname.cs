using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class BackendNickname : BackendLoginBase
{
	[System.Serializable]
	public class NicknameEvent : UnityEngine.Events.UnityEvent { }
	public NicknameEvent onNicknameEvent = new NicknameEvent();

	[SerializeField] private Image imageNickname; // 닉네임 필드 색상 변경
	[SerializeField] private TMP_InputField inputFieldNickname; // 닉네임 필드 텍스트 정보 추출
	[SerializeField] private Button btnUpdateNickname; // "닉네임 설정" 버튼 (상호작용 가능/불가능)

	private void OnEnable()
	{
		ResetUI(imageNickname);
		SetMessage("닉네임을 입력하세요");
	}

	public void OnClickUpdateNickname()
	{
		ResetUI(imageNickname);

		if (IsFieldDataEmpty(imageNickname, inputFieldNickname.text, "Nickname")) return;

		btnUpdateNickname.interactable = false;
		SetMessage("닉네임 변경중입니다..");

		UpdateNickname();
	}

	private void UpdateNickname()
	{
		Backend.BMember.UpdateNickname(inputFieldNickname.text, callback =>
		{
			btnUpdateNickname.interactable = true;

			if (callback.IsSuccess())
			{
				SetMessage($"{inputFieldNickname.text}(으)로 닉네임이 변경되었습니다.");

				onNicknameEvent?.Invoke();
			}
			else
			{
				string message = string.Empty;

				switch (int.Parse(callback.GetStatusCode()))
				{
					case 400:
						message = "닉네임이 비어있거나 | 20자 이상 이거나 | 앞/뒤에 공백이 있습니다.";
						break;
					case 409:
						message = "이미 존재하는 닉네임입니다.";
						break;
					default:
						message = callback.GetMessage();
						break;
				}

				GuideForIncorrectlyEnteredData(imageNickname, message);
			}
		});
	}
}
