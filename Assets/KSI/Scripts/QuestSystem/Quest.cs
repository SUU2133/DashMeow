using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Quest;

[CreateAssetMenu(fileName = "New Quest", menuName = "QuestSystem/Quest")]
public class Quest : ScriptableObject
{
	public enum QuestName
	{
		Quest_SUB,
		Quest_BOSS,
		Quest_BERSERKBOSS,
		Quest_INFINITE,
	}
	public QuestName curQuestName;

	public bool isComplete;
	public int targetScore;
	public int rewardSushi;
	public bool rewardDesirePiece;
	public int rewardValue; 
	public int currentScore;

	public void CheckCompleteQuest(int score)
	{
		Debug.Log($"{curQuestName} 완료 체크, 현재 점수 : {score}, 목표 점수: {targetScore}");
		if (score >= targetScore)
		{
			isComplete = true;
			Debug.Log(curQuestName + " 완료");
		}
		else
		{
			Debug.Log($"{curQuestName} 완료 못함. 현재 점수 : {score}, 목표 점수: {targetScore}");
		}
	}

	public int GetReward()
	{
		return currentScore;
	}

	public int GetDesirePiece()
	{
		return rewardValue;
	}
}
