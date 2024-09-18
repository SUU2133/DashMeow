using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractor : MonoBehaviour
{
	public int scoreValue = 10;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			GameManager.Score.AddJellyPaw(scoreValue);
			Destroy(gameObject);
		}
	}
}
