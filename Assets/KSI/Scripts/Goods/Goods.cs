using UnityEngine;

public abstract class Goods : MonoBehaviour
{
	[SerializeField] private float speed = 10f;
	protected int scoreValue = 500;
	protected IScore scoreAdapter;
	protected AudioClip getSound;
	public Player player;

	public abstract void Contact();
	private void Start()
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