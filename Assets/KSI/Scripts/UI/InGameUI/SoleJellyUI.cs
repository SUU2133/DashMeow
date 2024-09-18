using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameModeSystem;

public class SoleJellyUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI jellyPawText;
	[SerializeField] private TextMeshProUGUI sushiCountText;

	private void OnEnable()
	{
		GameManager.Score.OnJellyPawChanged += UpdateScoreText;
		GameManager.Score.OnJellyPawChanged += UpdateCountText;
	}

	private void UpdateScoreText(int value)
	{
		jellyPawText.text = value.ToString();
	}

	private void UpdateCountText(int value)
	{
		sushiCountText.text = (value / 500 * 25).ToString();
	}
}