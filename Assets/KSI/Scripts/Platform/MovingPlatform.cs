using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 움직이는 발판
// 위아래로 움직임

public class MovingPlatform : Platform
{
	[SerializeField] private float verticalSpeed = 2f;
	[SerializeField] private float verticalRange = 2f;
	
	private Vector3 startPosition;

	private void Start()
	{
		startPosition = transform.position;
	}

	protected override void Update()
	{
		base.Update();

		PingPongMove();
	}

	private void PingPongMove()
	{
		float newY = Mathf.PingPong(Time.time * verticalSpeed, verticalRange) - (verticalRange / 2);
		transform.position = new Vector3(transform.position.x, startPosition.y + newY, transform.position.z);
	}
}
