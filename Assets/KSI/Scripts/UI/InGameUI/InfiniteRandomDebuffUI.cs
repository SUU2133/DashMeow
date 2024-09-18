using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfiniteRandomDebuffUI : MonoBehaviour
{
	[SerializeField] private TMP_Text randomDebuffText1;
	[SerializeField] private TMP_Text randomDebuffText2;

	[SerializeField] private TMP_Text randomDebuffText1_1;
	[SerializeField] private TMP_Text randomDebuffText1_2;

	private DebuffSystem debuffSystem;

	private void Start()
	{
		debuffSystem = FindObjectOfType<DebuffSystem>();
		if (debuffSystem == null)
		{
			Debug.LogError("DebuffSystem 컴포넌트를 찾을 수 없음");
		}
	}

	public void DisplayInfiniteRandomDebuff(BerserkSystemManager.ZodiacSign debuff1, BerserkSystemManager.ZodiacSign debuff2)
	{
		randomDebuffText1.text = $"첫 번째\n디버프 {debuff1}";
		randomDebuffText2.text = $"두 번째\n디버프 {debuff2}";
	}

	//public void DisplayInfiniteRandomDebuff1(BerserkSystemManager.ZodiacSign debuff1, BerserkSystemManager.ZodiacSign debuff2)
	//{
	//	randomDebuffText1_1.text = debuffSystem.GetDebuffDescription(debuff1);
	//	randomDebuffText1_2.text = debuffSystem.GetDebuffDescription(debuff2);
	//}
}
