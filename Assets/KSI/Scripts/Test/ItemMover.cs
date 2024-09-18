using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMover : MonoBehaviour
{
	[SerializeField] private float speed = 1f;

	private void Update()
	{
		transform.Translate(Vector3.left * speed * Time.deltaTime);

		if (transform.position.x < -10f)
		{
			Destroy(gameObject);
		}
	}
}
