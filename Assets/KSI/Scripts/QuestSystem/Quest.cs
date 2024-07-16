using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "QuestSystem/Quest")]
public class Quest : ScriptableObject
{
	public string questName;
	public string description;
	public bool isComplete;
	public int scoreTarget;

	public void CheckCompleteQuest(int score)
	{
		Debug.Log($"{questName} 완료 체크, 현재 점수 : {score}, 목표 점수: {scoreTarget}");
		if (score >= scoreTarget)
		{
			isComplete = true;
			Debug.Log(questName + " 완료");
		}
		else
		{
			Debug.Log($"{questName} 완료 못함. 현재 점수 : {score}, 목표 점수: {scoreTarget}");
		}
	}
}
