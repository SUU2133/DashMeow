using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelViewer : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI nicknameText;
	[SerializeField] private TextMeshProUGUI sushiText;
	[SerializeField] private TextMeshProUGUI cannedFoodText;
	[SerializeField] private TextMeshProUGUI siverKeyText;
	[SerializeField] private TextMeshProUGUI goldKeyText;

	private void Awake()
	{
		//BackendGameData.Instance.onGameDataLoadEvent.AddListener(UpdateGameData);
		Debug.Log("AddListener/onGameDataLoadEvent");
	}

	public void UpdateNickname()
	{
		nicknameText.text = UserInfo.Data.nickname == null ? Backend.UID : UserInfo.Data.nickname;
		Debug.Log($"UpdateNickname : {nicknameText.text}");
	}

	public void UpdateGameData()
	{
		sushiText.text = $"{BackendGameData.Instance.UserGameData.sushi}";
		cannedFoodText.text = $"{BackendGameData.Instance.UserGameData.cannedFood}";
		siverKeyText.text = $"{BackendGameData.Instance.UserGameData.silverKey}  / 30";
		goldKeyText.text = $"{BackendGameData.Instance.UserGameData.goldKey}  / 5";

		Debug.Log($"Sushi UpdateGameData : {sushiText.text}");
		Debug.Log($"CannedFood UpdateGameData : {cannedFoodText.text}");
		Debug.Log($"SilverKey UpdateGameData : {siverKeyText.text}");
		Debug.Log($"GoldKeyUpdateGameData : {goldKeyText.text}");
	}
}
