using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : MonoBehaviour
{
	[SerializeField] private UserInfo user;

	private void Awake()
	{
		user.GetUserInfoFromBackend();
	}

	private void Start()
	{
		BackendGameData.Instance.GameDataLoad();
	}
}
