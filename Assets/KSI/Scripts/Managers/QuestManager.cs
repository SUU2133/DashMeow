using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
	//[Header("UI")]
	//[SerializeField] private TextMeshProUGUI scoreText;

	public List<Quest> quests;
	private ResultPopUpUI resultPopUpUI;

	private void Start()
	{
		InitializeQuests();
		resultPopUpUI = FindObjectOfType<ResultPopUpUI>();
	}

	private void InitializeQuests()
	{

		foreach (var quest in quests)
		{
			quest.isComplete = false;
			quest.currentScore = 0;
			Debug.Log("퀘스트 시작 : " + quest.curQuestName);
		}
	}

	public void UpdateQuestProgress(Quest.QuestName questName, int score)
	{
		Debug.Log($"퀘스트 진행 상황 업데이트 / 퀘스트 이름: {questName}, 점수 : {score}");
		bool questFound = false;

		foreach (var quest in quests)
		{
			Debug.Log($"확인 중인 퀘스트: {quest.curQuestName}");
			if (quest.curQuestName == questName)
			{
				questFound = true;

				if (!quest.isComplete)
				{
					int previousScore = quest.currentScore;
					quest.currentScore += score;
					Debug.Log($"퀘스트: {quest.curQuestName}, 이전 점수: {previousScore}, 현재 점수: {quest.currentScore}");

					quest.CheckCompleteQuest(quest.currentScore);

					//if (scoreText != null)
					//{
					//	scoreText.text = $"{quest.currentScore} / {quest.targetScore}";
					//}

					if (quest.isComplete)
					{
						Time.timeScale = 0f;
						GameManager.UI.ShowPopUpUI<PopUpUI>("UI/ResultPopUpUI");
						GiveReward(quest);
					}	
				}
				else
				{
					Debug.Log($"퀘스트 {quest.curQuestName}는 이미 완료됨");
				}
			}
		}
		if (!questFound)
		{
			Debug.LogError($"퀘스트 이름 '{questName}'을 찾을 수 없음");
		}
	}

	public void CompleteQuest(Quest.QuestName questName)
	{
		foreach (var quest in quests)
		{
			if (quest.curQuestName == questName)
			{
				quest.isComplete = true;
				Debug.Log(questName + " 완료");
			}
		}
	}

	private void GiveReward(Quest quest)
	{
		GiveSushi(quest);

		if (quest.rewardDesirePiece)
		{
			GiveDesirePiece(quest);
		}
	}

	private void GiveSushi(Quest quest)
	{
		int reward = quest.GetReward() * 5;
		Debug.Log($"퀘스트 보상 : '{quest.curQuestName}' / 초밥 {reward} 개 ");
		DataManager.Instance.sushi += reward;
	}

	private void GiveDesirePiece(Quest quest)
	{
		int rewardValue = quest.GetDesirePiece();
		Debug.Log($"퀘스트 보상 : '{quest.curQuestName}' / 깨진 염원 조각 {rewardValue} 개");
		string[] colors = { "blue", "red", "green" };

		System.Random random = new System.Random();
		List<int> selectedIndexes = new List<int>();

		while (selectedIndexes.Count < 1)
		{
			int index = random.Next(colors.Length);
			if (!selectedIndexes.Contains(index))
			{
				selectedIndexes.Add(index);
			}
		}

		foreach (int index in selectedIndexes)
		{
			switch (colors[index])
			{
				case "blue":
					DataManager.Instance.brokenBlue += rewardValue;
					break;
				case "red":
					DataManager.Instance.brokenRed += rewardValue;
					break;
				case "green":
					DataManager.Instance.brokenGreen += rewardValue;
					break;
			}
		}
	}

	public void OnGameEnd()
	{
		foreach (var quest in quests)
		{
			if (quest.curQuestName == Quest.QuestName.Quest_INFINITE)
			{
				GameManager.Instance.InfiniteEndGame();
				resultPopUpUI.DisplayResultUI();
				GiveReward(quest);
			}
		}
	}
}