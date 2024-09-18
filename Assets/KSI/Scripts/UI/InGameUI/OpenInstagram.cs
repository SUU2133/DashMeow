using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInstagram : MonoBehaviour
{
	public string instagramUrl = "https://www.instagram.com/studio._.llucky/";

	public void OpenInstagramApp()
	{
		string appUrl = "https://www.instagram.com/studio._.llucky/";

		if (Application.platform == RuntimePlatform.Android)
		{
			Application.OpenURL(appUrl);
		}
		else
		{
			Application.OpenURL(instagramUrl);
		}
	}
}
