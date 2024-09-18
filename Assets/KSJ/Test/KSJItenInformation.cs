using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KSJItemInformation : MonoBehaviour
{
	public int ksjscoreValue = 10;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			KSJScoreText.instance.AddScore(ksjscoreValue);
			Destroy(gameObject);
		}
	}
}
