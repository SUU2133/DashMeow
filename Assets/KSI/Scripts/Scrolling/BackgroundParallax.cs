using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
	private Material mat;
	private float distance;
	public Player player;

	[Range(0f, 0.5f)]
	public float speed = 0.2f;

	private void Start()
    {
		player = FindObjectOfType<Player>();
		mat = GetComponent<Renderer>().material;

	}

	private void Update()
    {
		if (player.isBooster || player.isGlide)

			distance += Time.deltaTime * speed * player.boosterSpeed;
		else
			distance += Time.deltaTime * speed;
		mat.SetTextureOffset("_MainTex", Vector2.right * distance);
    }
}
