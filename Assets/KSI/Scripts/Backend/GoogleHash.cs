using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoogleHash : MonoBehaviour
{
	[SerializeField] private TMP_InputField googleHash;

	public void GetGoogleHash()
	{
		string googleHashKey = Backend.Utils.GetGoogleHash();

		if (!string.IsNullOrEmpty(googleHashKey))
		{
			Debug.Log(googleHashKey);
			if (googleHash != null)
			{
				googleHash.text = googleHashKey;
			}
		}
	}
}
