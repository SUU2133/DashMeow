using UnityEngine;

public class KSJCameraController : MonoBehaviour
{
	public Transform player;


	private Vector3 offset;

	private void Start()
	{
		offset = transform.position - player.position;
	}

	private void LateUpdate()
	{


		Vector3 newPosition = player.position + offset;
		transform.position = new Vector3(newPosition.x, transform.position.y, transform.position.z);
	}
}
