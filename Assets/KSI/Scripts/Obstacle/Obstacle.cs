using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 장애물
// 파괴 안됨
public abstract class Obstacle : MonoBehaviour, IDamagable
{
	[SerializeField] private float speed = 10f;
	[SerializeField] protected int damage = 10;
	public Player player;
	public void TakeDamage(int damage) { }
	private void Awake()
	{
		player = FindObjectOfType<Player>();

	}
	private void Update()
	{
		Move();
	}

	protected virtual void Move()
	{ 
		if (player.isBooster || player.isGlide)
			transform.Translate(Vector3.left * speed * Time.deltaTime * player.boosterSpeed);
		else
			transform.Translate(Vector3.left * speed * Time.deltaTime);

		//Destroy(gameObject, 20f);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		IDamagable damagable = other.GetComponent<IDamagable>();
		if (damagable != null)
		{
			damagable.TakeDamage(damage);
		}
	}
}
