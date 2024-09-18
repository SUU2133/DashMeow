using UnityEngine;

public class SnakeBERSERKBOSSButton : MonoBehaviour
{
	private const float updateCycle = 1f;
	private float lastUpdate = 0f;
	private GameObject berserkBossButton;

	private void Awake()
	{
		berserkBossButton = GameObject.Find("SnakeBERSERKBOSSButton");
		berserkBossButton.SetActive(false);
		UpdateBERSERKBOSSButton();
	}

	private void Update()
	{
		lastUpdate += Time.deltaTime;

		if (lastUpdate >= updateCycle)
		{
			UpdateBERSERKBOSSButton();
			lastUpdate = 0f;
		}
	}

	private void UpdateBERSERKBOSSButton()
	{
		var currentZodiacSign = GameManager.BerserkSystem.GetCurZodiacSign();
		Debug.Log($"Current Zodiac Sign: {currentZodiacSign}");
		ActivateBERSERKBOSSButton(currentZodiacSign);
	}

	private void ActivateBERSERKBOSSButton(BerserkSystemManager.ZodiacSign zodiacSign)
	{
		switch (zodiacSign)
		{
			case BerserkSystemManager.ZodiacSign.SNAKE:
				berserkBossButton.SetActive(true);
				Debug.Log("SnakeBERSERKBOSSButton activated.");
				break;
			default:
				berserkBossButton.SetActive(false);
				Debug.Log("SnakeBERSERKBOSSButton deactivated.");
				break;
		}
	}
}
