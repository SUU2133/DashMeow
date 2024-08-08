using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class BackendLoginBase : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI textMessage;

	/// <summary>
	/// 메시지 내용, InputField 색상 초기화
	/// </summary>
	protected void ResetUI(params Image[] images)
	{
		textMessage.text = string.Empty;

		for (int i = 0; i < images.Length; ++i)
		{
			images[i].color = Color.white;
		}
	}

	/// <summary>
	/// 매개변수에 있는 내용을 출력
	/// </summary>
	protected void SetMessage(string msg)
	{
		textMessage.text = msg;
	}

	/// <summary>
	/// 입력 오류가 있는 Inputfield의 색상 변경
	/// 오류에 대한 메세지 출력
	/// </summary>
	protected void GuideForIncorrectlyEnteredData(Image image, string msg)
	{
		textMessage.text = msg;
		image.color = Color.red;
	}

	/// <summary>
	/// 필드 값이 비어있는지 확인 (image : 필드 , field : 내용 , result : 출력될 내용)
	/// </summary>
	protected bool IsFieldDataEmpty(Image image, string field, string result)
	{
		if (field.Trim().Equals(""))
		{
			GuideForIncorrectlyEnteredData(image, $"\"{result}\"를 작성해주세요");
			return true;
		}
		return false;
	}	
}
