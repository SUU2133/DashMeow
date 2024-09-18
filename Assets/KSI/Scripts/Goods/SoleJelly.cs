using UnityEngine;
using static GameModeSystem;
using static Quest;

// 젤리 발바닥(점수 코인)
// 스토리 및 무한 모드에서 획득한 젤리 코인을 생선으로 환산
// 플레이 점수
// 보상으로 젤리 발바닥 1개당 점수 100점
// 젤리 발바닥 1개당 초밥 5개

public class SoleJelly : Goods
{
	public QuestName questName;

	private void Awake()
	{
		scoreAdapter = new JellyPawAdapter();
		getSound = GameManager.Resource.Load<AudioClip>("Sounds/051_use_item_01");
	}

	public override void Contact()
	{
		gameObject.SetActive(false);

		Debug.Log($"발바닥 젤리 점수 : {scoreValue}");
		
		scoreAdapter.AddScore(scoreValue);

		QuestManager questManager = FindObjectOfType<QuestManager>();
		if (questManager != null)
		{
			if (questName == QuestName.Quest_INFINITE)
			{
				Debug.Log("무한모드에서는 퀘스트를 업데이트하지 않습니다.");
				return;
			}

			Debug.Log($"퀘스트 진행 상황 업데이트 호출 / 퀘스트 이름: {questName}, 전달 점수 : {scoreValue}");
			questManager.UpdateQuestProgress(questName, scoreValue);
		}
		else
		{
			Debug.LogError("QuestManager를 찾을 수 없음");
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Contact();
			GameManager.Sound.SFXPlay("051_use_item_01", getSound);
		}
	}
}
