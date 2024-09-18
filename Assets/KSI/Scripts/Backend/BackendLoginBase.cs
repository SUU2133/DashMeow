using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class BackendLoginBase : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI textMessage;

	protected void ResetUI(params Image[] images)
	{
		textMessage.text = string.Empty;

		for (int i = 0; i < images.Length; ++i)
		{
			images[i].color = Color.white;
		}
	}

	protected void SetMessage(string msg)
	{
		textMessage.text = msg;
	}

	protected void GuideForIncorrectlyEnteredData(Image image, string msg)
	{
		textMessage.text = msg;
		image.color = Color.red;
	}

	protected bool IsFieldDataEmpty(Image image, string field, string result)
	{
		if (field.Trim().Equals(""))
		{
			GuideForIncorrectlyEnteredData(image, $"\"{result}\"를 작성해주세요");
			return true;
		}
		return false;
	}

	protected void ResetImageColor(Image image)
	{
		if (image != null)
		{
			image.color = Color.white;
		}
	}
}
