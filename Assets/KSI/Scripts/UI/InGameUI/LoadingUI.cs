using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
	[SerializeField] private Slider sliderProgress;
	[SerializeField] private TextMeshProUGUI textProgressData;
	[SerializeField] private float progressTime;

	public void Play(UnityAction action = null)
	{
		StartCoroutine(OnProgressRoutine(action));
	}

	private IEnumerator OnProgressRoutine(UnityAction action)
	{
		float current = 0;
		float percent = 0;

		while (percent < 1)
		{
			current += Time.deltaTime;
			percent = current / progressTime;

			textProgressData.text = $"로딩중... {sliderProgress.value * 100:F0}%";
			sliderProgress.value = Mathf.Lerp(0, 1, percent);

			yield return null;
		}

		action?.Invoke();
	}

	//public Slider slider;

	//private Animator animater;

	//void Awake()
	//{
	//	animater = GetComponent<Animator>();
	//}

	//public void FadeIn()
	//{
	//	animater.SetBool("Active", true);
	//	Debug.Log("Fade In");
	//}

	//public void FadeOut()
	//{
	//	animater.SetBool("Active", false);
	//	Debug.Log("Fade Out");
	//}

	//public void SetProgress(float progress)
	//{
	//	slider.value = progress;
	//}
}
