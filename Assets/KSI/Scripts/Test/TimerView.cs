using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
	[Header("Timer")]
	public float roundTime;
	public Slider timerSlider;

	//[Header("Time Out")]
	//public GameObject TimeOutUI;

	bool roundEnd = false;

	private void Start()
	{
		//TimeOutUI.SetActive(false);

		timerSlider.maxValue = roundTime;
		timerSlider.value = roundTime;
	}

	void Update()
	{
		Timer();
	}

	void Timer()
	{
		if (roundTime > 0 && !roundEnd)
		{
			roundTime -= Time.deltaTime;

			timerSlider.value = roundTime;

			if (roundTime <= 0)
			{
				roundTime = 0;
				roundEnd = true;
			}
		}

		//if (roundEnd)
		//{
		//	TimeOutUI.SetActive(true); ;

		//	roundEnd = false;
		//}
	}

}
