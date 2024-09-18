using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
	public GameObject questCompleteUI;
	public TMP_Text questCompleteText;

	private void Start()
    {
		questCompleteUI.SetActive(false);
	}

	public void ShowQuestCompleteUI(Quest.QuestName questName)
	{
		questCompleteText.text = questName + " CLEAR!";
		questCompleteUI.SetActive(true);
	}
}
