using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NickNameUpdate : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI textNickname;
	[SerializeField] private TMP_InputField inputFieldUID;

	public void UpdateNickname()
	{
		textNickname.text = UserInfo.Data.nickname == null ? Backend.UID : UserInfo.Data.nickname;

		inputFieldUID.text = Backend.UID;
		inputFieldUID.readOnly = true;
	}
}
