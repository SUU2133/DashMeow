using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KSJScoreText : MonoBehaviour
{
	public static KSJScoreText instance;
	public Text scoreText;
	private int score = 0;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		UpdateScoreText();
	}

	public void AddScore(int value)
	{
		score += value;
		UpdateScoreText();
	}

	void UpdateScoreText()
	{
		scoreText.text = "Score: " + score.ToString();
	}
}