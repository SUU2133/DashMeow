public class JellyPawAdapter : IScore
{
	public void AddScore(int score)
	{
		GameManager.Score.AddJellyPaw(score);
	}

	//public void AddCount(int count)
	//{
	//	GameManager.Score.AddJellyPawCount(count);
	//}
}