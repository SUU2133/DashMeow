using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class StageUnlockData : BackendStoreManager
{
	private const string UnlockedStages = "UnlockedStages"; 

	public HashSet<int> unlockedStages = new HashSet<int>();


	public StageUnlockData(string key) : base(key)
	{
		unlockedStages = new HashSet<int>();
	}

	// JSON 데이터로부터 해금된 스테이지 정보를 초기화하는 생성자
	public StageUnlockData(string key, LitJson.JsonData userData) : base(key, userData)
	{
		if (userData.Count <= 0)
		{
			Debug.LogWarning("스테이지 데이터가 존재하지 않습니다.");
		}
		else
		{
			unlockedStages = new HashSet<int>();
			foreach (var stage in userData[0][UnlockedStages])
			{
				unlockedStages.Add(int.Parse(stage.ToString()));
			}
			Debug.Log(userData.ToString());
		}
	}	

	public void UnlockStage(int stageIdx)
	{
		if (!unlockedStages.Contains(stageIdx))
		{
			unlockedStages.Add(stageIdx);
			UpdateFlagSet();
		}
	}

	protected override Param GetAllData()
	{
		Param param = new Param();
		param.Add(UnlockedStages, unlockedStages);
		return param;
	}
}

