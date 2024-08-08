using UnityEngine;

// 쉴드
// 버프형 오브젝트
// 고정형 오브젝트
// 버프 : 장애물 1회 방어(낙사 제외)
// 스토리 모드(서브, 보스, 보스(광폭)), 무한 모드
// 획득 시 사라짐
public class Shield : BuffTypeObject
{
	private void Awake()
	{
		getSound = GameManager.Resource.Load<AudioClip>("Sound/051_use_item_01");
	}

	public override void Buff()
	{
		player.BlockObstacle();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			GameManager.Sound.SFXPlay("Sound/051_use_item_01", getSound);
			Destroy(gameObject);
			Buff();
			Debug.Log("쉴드");
		}
	}
}
