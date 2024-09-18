using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossDebuffUI : MonoBehaviour
{
	[Header("UI")]
	[SerializeField] private GameObject bossDebuffUI;
	//[SerializeField] private TMP_Text debuffText;

	private void Start()
	{
		bossDebuffUI.SetActive(false);
		//DisplayBossDebuff(GameManager.Instance.BossDebuff);
	}

	//public void DisplayBossDebuff(BerserkSystemManager.ZodiacSign debuff)
	//{
	//	debuffText.text = $"현재 보스 디버프: {debuff}";
	//}	
}
