using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class GoogleManager : MonoBehaviour
{
	public TextMeshProUGUI logText;

	private void Start()
	{
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();
		SignIn();
	}

	public void SignIn()
	{
		PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
	}

	internal void ProcessAuthentication(SignInStatus status)
	{
		if (status == SignInStatus.Success)
		{
			string name = PlayGamesPlatform.Instance.GetUserDisplayName();
			string id = PlayGamesPlatform.Instance.GetUserId();
			string ImgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();

			logText.text = "Success \n" + name;
		}
		else
		{
			logText.text = "Sign In Failed";
		}
	}

}

