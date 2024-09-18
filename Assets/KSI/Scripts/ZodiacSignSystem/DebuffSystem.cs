using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

// 디버프 시스템
// 해(돼지) : 최대 체력 20감소
// 술(개) : 이동 속도 20 감소
// 유(닭) : 고정형 장애물 오브젝트 피해 수치 5 증가
// 신(원숭이) : 젤리 코인 점수 5 감소
// 미(양) : 반딧불 회복량 5 감소
// 오(말) : 부스터 아이템 지속시간 1.5초 감소
// 사(뱀) : 날아오는 장애물 오브젝트 피해 수치 5 증가
// 진(용) : 무적 아이템 지속시간 1.5초 감소
// 묘(토끼)) : 츄르의 회복량 5 감소
// 인(호랑이) : 쉴드 효과 무효화
// 축(소) : 활공 1초 감소
// 자(쥐) : 체력이 5초당 1 감소
public class DebuffSystem : MonoBehaviour
{
	public UnityAction OnPigDebuffChanged;
	public UnityAction OnDogDebuffChanged;
	public UnityAction OnChickenDebuffChanged;
	public UnityAction OnMonkeyDebuffChanged;
	public UnityAction OnSheepDebuffChanged;
	public UnityAction OnHorseDebuffChanged;
	public UnityAction OnSnakeDebuffChanged;
	public UnityAction OnDragonDebuffChanged;
	public UnityAction OnRabbitDebuffChanged;
	public UnityAction OnTigerDebuffChanged;
	public UnityAction OnCowDebuffChanged;
	public UnityAction OnMouseDebuffChanged;

	[SerializeField] private TextMeshProUGUI debuffText;

	private Player player;
	private DebuffSystem debuffSystem;
	
	private void Start()
	{
		player = FindObjectOfType<Player>();
		debuffSystem = FindObjectOfType<DebuffSystem>();

		OnPigDebuffChanged += PigDebuff;
		OnDogDebuffChanged += DogDebuff;
		OnChickenDebuffChanged += ChickenDebuff;
		OnMonkeyDebuffChanged += MonkeyDebuff;
		OnSheepDebuffChanged += SheepDebuff;
		OnHorseDebuffChanged += HorseDebuff;
		OnSnakeDebuffChanged += SnakeDebuff;
		OnDragonDebuffChanged += DragonDebuff;
		OnRabbitDebuffChanged += RabbitDebuff;
		OnTigerDebuffChanged += TigerDebuff;
		OnCowDebuffChanged += CowDebuff;
		OnMouseDebuffChanged += MouseDebuff;
	}

	public void PigDebuff()
	{
		FindPlayer();
		player.PigDebuff();
		UpdateDebuffText("해시 디버프");
	}

	public void DogDebuff()
	{
		FindPlayer();
		player.DogDebuff();
		UpdateDebuffText("술시 디버프");
	}

	public void ChickenDebuff()
	{
		FindPlayer();
		player.RoosterDebuff();
		UpdateDebuffText("유시 디버프");
	}

	public void MonkeyDebuff()
	{
		FindPlayer();
		player.MonkeyDebuff();
		UpdateDebuffText("신시 디버프");
	}

	public void SheepDebuff()
	{
		FindPlayer();
		player.LamnDebuff();
		UpdateDebuffText("미시 디버프");
	}

	public void HorseDebuff()
	{
		FindPlayer();
		player.HorseDebuff();
		UpdateDebuffText("오시 디버프");
	}

	public void SnakeDebuff()
	{
		FindPlayer();
		player.SnakeDebuff();
		UpdateDebuffText("사시 디버프");
	}

	public void DragonDebuff()
	{
		FindPlayer();
		player.DragonDebuff();
		UpdateDebuffText("진시 디버프");
	}

	public void RabbitDebuff()
	{
		FindPlayer();
		player.RabbitDebuff();
		UpdateDebuffText("묘시 디버프");
	}

	public void TigerDebuff()
	{
		FindPlayer();
		player.TigerDebuff();
		UpdateDebuffText("인시 디버프");
	}

	public void CowDebuff()
	{
		FindPlayer();
		player.OxDebuff();
		UpdateDebuffText("축시 디버프");
	}

	public void MouseDebuff()
	{
		FindPlayer();
		player.RatDebuff();
		UpdateDebuffText("자시 디버프");
	}

	private void UpdateDebuffText(string message)
	{
		if (debuffText != null)
		{
			debuffText.text = message;
		}
	}

	//public string GetDebuffDescription(BerserkSystemManager.ZodiacSign zodiacSign)
	//{
	//	switch (zodiacSign)
	//	{
	//		case BerserkSystemManager.ZodiacSign.PIG:
	//			return "최대 체력 20 감소";
	//		case BerserkSystemManager.ZodiacSign.DOG:
	//			return "이동 속도 20 감소";
	//		case BerserkSystemManager.ZodiacSign.CHICKEN:
	//			return "고정형 장애물 오브젝트 피해 수치 5 증가";
	//		case BerserkSystemManager.ZodiacSign.MONKEY:
	//			return "젤리 코인 점수 5 감소";
	//		case BerserkSystemManager.ZodiacSign.SHEEP:
	//			return "반딧불 회복량 5 감소";
	//		case BerserkSystemManager.ZodiacSign.HORSE:
	//			return "부스터 아이템 지속시간 1.5초 감소";
	//		case BerserkSystemManager.ZodiacSign.SNAKE:
	//			return "날아오는 장애물 오브젝트 피해 수치 5 증가";
	//		case BerserkSystemManager.ZodiacSign.DRAGON:
	//			return "무적 아이템 지속시간 1.5초 감소";
	//		case BerserkSystemManager.ZodiacSign.RABBIT:
	//			return "츄르의 회복량 5 감소";
	//		case BerserkSystemManager.ZodiacSign.TIGER:
	//			return "쉴드 효과 무효화";
	//		case BerserkSystemManager.ZodiacSign.COW:
	//			return "활공 1초 감소";
	//		case BerserkSystemManager.ZodiacSign.MOUSE:
	//			return "체력이 5초당 1 감소";
	//		default:
	//			return "-";
	//	}
	//}

	public void FindPlayer()
	{
		if (player == null)
			player = FindObjectOfType<Player>();
	}
}
