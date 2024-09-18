using UnityEngine;
using System.Collections;

// 반딧불
// 버프형 오브젝트
// 날아오는 오브젝트
// 오른쪽에서 왼쪽으로 위아래로 움직이면서 날아옴
// 버프 : 체력 10회복
// 스토리 모드(서브, 보스, 보스(광폭)), 무한 모드
// 획득 시 사라짐
public class Firefly : BuffTypeObject
{
	public float range = 1f;
	public float cycle = 1f;

	private Vector3 startPosition;

	public override void Buff()
	{
		player.HealByFirfly(10);
	}

	private void Awake()
	{
		player = FindObjectOfType<Player>();
		getSound = GameManager.Resource.Load<AudioClip>("Sounds/051_use_item_01");
	}

	private void Start()
	{
		startPosition = transform.position; 
	}

	private void Update()
	{
		float y = Mathf.Sin(Time.time * cycle) * range;
		transform.position = startPosition + Vector3.left * speed * Time.time + Vector3.up * y;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			GameManager.Sound.SFXPlay("051_use_item_01", getSound);
			Buff();
			Destroy(gameObject);
			Debug.Log("반딧불");
		}
	}
}
