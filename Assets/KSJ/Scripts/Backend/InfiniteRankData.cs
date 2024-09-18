using UnityEngine;
using TMPro;

public class InfiniteRankData : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI textRank;
	[SerializeField]
	private TextMeshProUGUI textNickname;
	[SerializeField]
	private TextMeshProUGUI textScore;

	private int rank;
	private string nickname;
	private int score;

	public int Rank
	{
		set
		{
			if(value <= Constants.MAX_RANK_LIST)
			{
				rank = value;
				textRank.text = rank.ToString();
			}
			else
			{
				textRank.text = "-";
			}


		}

		get => rank;
	}

	public string Nickname
	{
		set
		{
			nickname = value;
			textNickname.text = nickname;
		}
		get => nickname;

	}

	public int Score
	{
		set
		{
			score = value;
			textScore.text = score.ToString();
		}
		get => score;
	}
}
