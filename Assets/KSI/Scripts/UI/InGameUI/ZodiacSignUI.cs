using TMPro;
using UnityEngine;

public class ZodiacSignUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI zodiacSignText;
	[SerializeField] private TextMeshProUGUI zodiacTimeText;

	private const float updateCycle = 1f;
	private float lastUpdate = 0f;

	private void Start()
	{
		if (zodiacSignText == null)
		{
			Debug.LogError("ZodiacSignText가 할당되지 않았습니다.");
			return;
		}

		if (zodiacTimeText == null)
		{
			Debug.LogError("ZodiacTimeText가 할당되지 않았습니다.");
			return;
		}

		if (GameManager.BerserkSystem == null)
		{
			Debug.LogError("BerserkSystemManager가 할당되지 않았습니다.");
			return;
		}

		//UpdateZodiacSignUI();

		//StartCoroutine(UpdateZodiacSignRoutine());
	}

	private void Update()
	{
		lastUpdate += Time.deltaTime;

		if (lastUpdate >= updateCycle)
		{
			UpdateZodiacSignUI();
			lastUpdate = 0f;
		}
	}

	private void UpdateZodiacSignUI()
	{
		var currentZodiacSign = GameManager.BerserkSystem.GetCurZodiacSign();
		(string signText, string timeText) = GetDisplayTextForZodiacSign(currentZodiacSign);
		zodiacSignText.text = signText;
		zodiacTimeText.text = timeText;
	}

	//private IEnumerator UpdateZodiacSignRoutine()
	//{
	//	while (true)
	//	{
	//		UpdateZodiacSignUI();

	//		yield return new WaitForSeconds(1);
	//	}
	//}

	private (string, string) GetDisplayTextForZodiacSign(BerserkSystemManager.ZodiacSign zodiacSign)
	{
		switch (zodiacSign)
		{
			case BerserkSystemManager.ZodiacSign.MOUSE:
			case BerserkSystemManager.ZodiacSign.COW:
			case BerserkSystemManager.ZodiacSign.TIGER:
			case BerserkSystemManager.ZodiacSign.RABBIT:							
				return ("자/축/인/묘\n23~07", "자시/23~01\n축시/01~03\n인시/03~05\n묘시/05~07");
			case BerserkSystemManager.ZodiacSign.DRAGON:
			case BerserkSystemManager.ZodiacSign.SNAKE:
			case BerserkSystemManager.ZodiacSign.HORSE:
			case BerserkSystemManager.ZodiacSign.SHEEP:				
				return ("진/사/오/미\n07~15", "진시/07~09\n사시/09~11\n오시/11~13\n미시/13~15");
			case BerserkSystemManager.ZodiacSign.MONKEY:
			case BerserkSystemManager.ZodiacSign.CHICKEN:
			case BerserkSystemManager.ZodiacSign.DOG:
			case BerserkSystemManager.ZodiacSign.PIG:						
				return ("신/유/술/해\n15~23", "신시/15~17\n유시/17~19\n술시/19~21\n해시/21~23");
			default:
				return ("-", "-");
		}
	}
}
