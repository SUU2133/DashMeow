using UnityEngine;
using System.Collections;

// 버프형 오브젝트
// 파괴됨
public abstract class BuffTypeObject : MonoBehaviour
{
	[SerializeField] protected float speed = 10f;
	protected AudioClip getSound;
	public Player player;

	public abstract void Buff();
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
}
