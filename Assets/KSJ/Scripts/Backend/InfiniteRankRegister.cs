using UnityEngine;
using BackEnd;

public class InfiniteRankRegister : MonoBehaviour
{
	public void Process(int newScore)
	{
		//UpdateMyRankData(newScore);
		UpdateMyBestRankData(newScore);
	}

	private void UpdateMyRankData(int newScore)
	{
		string rowInDate = string.Empty;

		Backend.GameData.GetMyData(Constants.USER_DATA_TABLE, new Where(), callback =>
		{
			if (!callback.IsSuccess())
			{
				Debug.LogError($"[랭킹]데이터 조회 중 문제가 발생 : {callback}");
				return;
			}

			Debug.Log($"[랭킹]데이터 조회에 성공 : {callback}");

			if (callback.FlattenRows().Count > 0)
			{
				rowInDate = callback.FlattenRows()[0]["inDate"].ToString();
			}
			else
			{
				Debug.LogError("[랭킹]데이터가 존재하지 않습니다.");
				return;
			}

			Param param = new Param()
			  {
				  {"INFINITBestscore",newScore }
			  };

			Backend.URank.User.UpdateUserScore(Constants.INFINITE_RANK_UUID, Constants.USER_DATA_TABLE, rowInDate, param, callback =>
			{
				if (callback.IsSuccess())
				{
					Debug.Log($"[랭킹]랭킹 등록에 성공 : {callback}");
				}
				else
				{
					Debug.LogError($"[랭킹]랭킹 등록에 실패 : {callback}");
				}

			});
		});

	}

	private void UpdateMyBestRankData(int newScore)
	{
		Backend.URank.User.GetMyRank(Constants.INFINITE_RANK_UUID, callback =>
		{
			if (callback.IsSuccess())
			{
				// json 데이터 파싱 성공
				try
				{
					LitJson.JsonData rankDataJson = callback.FlattenRows();

					//받아온 데이터의 개수가 0이면 데이터가 없는 것

					if (rankDataJson.Count <= 0)
					{
						Debug.LogWarning("[랭킹]데이터가 존재하지 않습니다.");
					}
					else
					{
						//랭킹을 등록할 때는 컬럼명을 "weeklyBewstScore" 라고 하지만
						//랭킹을 불러올 때는 컬럼명을 "score"로 한다

						//추가로 등록한 항목은 컬럼명을 그대로 사용
						int bestScore = int.Parse(rankDataJson[0]["score"].ToString());

						if (newScore > bestScore)
						{
							UpdateMyRankData(newScore);

							Debug.Log($"[랭킹]최고 점수 갱신{ bestScore} -> { newScore}");
						}
					}
				}
				catch (System.Exception e)
				{
					Debug.LogError(e);
				}
			}
			else
			{
				if (callback.GetMessage().Contains("userRank"))
				{
					UpdateMyRankData(newScore);
					Debug.Log($"[랭킹]새로운 랭킹 데이터 생성 및 등록 : {callback}");
				}
			}

		});
	}
}
