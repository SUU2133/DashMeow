using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
	public int jellyPawText;

	public UnityAction<int> OnJellyPawChanged;

	public void AddJellyPaw(int score)
	{
		jellyPawText += score;
		OnJellyPawChanged?.Invoke(jellyPawText);
	}

	public void AddJellyPawCount(int score)
	{
		jellyPawText += score;
		OnJellyPawChanged?.Invoke(jellyPawText);
	}

	public int GetTotalScore()
	{
		return jellyPawText;
	}

	public void Reset()
	{
		jellyPawText = 0;
	}
}