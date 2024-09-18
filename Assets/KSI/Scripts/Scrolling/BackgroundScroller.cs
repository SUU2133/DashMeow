using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
	[SerializeField] public float speed;
	public Player player;

	private MeshRenderer render;
	private float x = 0;
	private float y = 0;

	private void Awake()
	{
		player = FindObjectOfType<Player>();
		render = GetComponent<MeshRenderer>();
	}

	private void Update()

	{
		render.material.mainTextureOffset = new Vector2(x, y);

		if (player.isBooster || player.isGlide)

			x = x + Time.deltaTime * speed * player.boosterSpeed;
		else

			x = x + Time.deltaTime * speed;
	}
}


