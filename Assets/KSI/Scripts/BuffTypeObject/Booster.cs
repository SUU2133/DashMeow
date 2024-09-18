using UnityEngine;

// 부스터
// 버프형 오브젝트
// 고정형 오브젝트
// 버프 : 3초간 모든 장애물을 파괴하면서 질주(이동 속도 수치가 20증가)
// 스토리 모드(서브, 보스, 보스(광폭)), 무한 모드
// 획득 시 사라짐
public class Booster : BuffTypeObject
{
	private void Awake()
	{
		player = FindObjectOfType<Player>();
		getSound = GameManager.Resource.Load<AudioClip>("Sounds/051_use_item_01");
	}

	public override void Buff()
	{
		player.Booster(3f);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			GameManager.Sound.SFXPlay("051_use_item_01", getSound);
			Buff();
			Destroy(gameObject);
			Debug.Log("부스터");
		}
	}
}
