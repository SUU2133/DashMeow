using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 발판
// 파괴 안됨
// 통과 못함
// 스토리모드, 무한모드
public abstract class Platform : MonoBehaviour
{
	public float speed = 10f;
	public Player player;
	//public abstract void Pass();
	private void Awake()
	{
		player = FindObjectOfType<Player>();

	}
	protected virtual void Update()
	{
		Move();
	}

	protected virtual void Move()
	{
		if (player.isBooster || player.isGlide)
			transform.Translate(Vector3.left * speed * Time.deltaTime * player.boosterSpeed);
		else
			transform.Translate(Vector3.left * speed * Time.deltaTime);
		

		Destroy(gameObject, 10f);
	}
}
