using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

// 광폭 시스템
// 자축인묘 : 23 ~ 07 (녹)
// 진사오미 : 07 ~ 15 (청)
// 신유술해 : 15 ~ 23 (적)

public class BerserkSystemManager : MonoBehaviour
{
	public enum ZodiacSign
	{
		MOUSE,
		COW,
		TIGER,
		RABBIT,
		DRAGON,
		SNAKE,
		HORSE,
		SHEEP,
		MONKEY,
		CHICKEN,
		DOG, 
		PIG	
	}

	[Serializable]
	public class BerserkTime
	{
		public ZodiacSign zodiacSign;
		public int startHour;
		public int endHour;
	}

	public class ZodiacSignDebuff
	{
		public ZodiacSign zodiacSign;
		public UnityAction applyDebuff;
	}

	public BerserkTime[] berserkTime;
	public ZodiacSignDebuff[] zodiacSignDebuff = new ZodiacSignDebuff[12];

	private DebuffSystem debuffSystem;

	private void Start()
	{
		debuffSystem = FindObjectOfType<DebuffSystem>();
		if (debuffSystem == null)
		{
			Debug.LogError("DebuffSystem 컴포넌트가 존재하지 않습니다!");
			return;
		}
		debuffSystem.FindPlayer();

		List<BerserkTime> list = new List<BerserkTime>();
		var values = Enum.GetValues(typeof(BerserkSystemManager.ZodiacSign)).Cast<BerserkSystemManager.ZodiacSign>().ToList();
		byte time = 0;
		foreach (var item in values)
		{
			list.Add(new BerserkTime { zodiacSign = item, startHour = time, endHour = time + 2 });
			time += 2;
		}
		berserkTime = list.ToArray();

		zodiacSignDebuff = new ZodiacSignDebuff[]
		{
			new ZodiacSignDebuff { zodiacSign = ZodiacSign.MOUSE,applyDebuff = () => debuffSystem.OnMouseDebuffChanged()},
			new ZodiacSignDebuff { zodiacSign = ZodiacSign.COW, applyDebuff = () => debuffSystem.OnCowDebuffChanged()},
			new ZodiacSignDebuff { zodiacSign = ZodiacSign.TIGER, applyDebuff = () => debuffSystem.OnTigerDebuffChanged()},
			new ZodiacSignDebuff { zodiacSign = ZodiacSign.RABBIT, applyDebuff = () => debuffSystem.OnRabbitDebuffChanged()},
			new ZodiacSignDebuff { zodiacSign = ZodiacSign.DRAGON, applyDebuff = () => debuffSystem.OnDragonDebuffChanged()},
			new ZodiacSignDebuff { zodiacSign = ZodiacSign.SNAKE,applyDebuff = () => debuffSystem.OnSnakeDebuffChanged()},
			new ZodiacSignDebuff { zodiacSign = ZodiacSign.HORSE, applyDebuff = () => debuffSystem.OnHorseDebuffChanged()},
			new ZodiacSignDebuff { zodiacSign = ZodiacSign.SHEEP, applyDebuff = () => debuffSystem.OnSheepDebuffChanged()},
			new ZodiacSignDebuff { zodiacSign = ZodiacSign.MONKEY, applyDebuff = () => debuffSystem.OnMonkeyDebuffChanged()},
			new ZodiacSignDebuff { zodiacSign = ZodiacSign.CHICKEN, applyDebuff = () => debuffSystem.OnChickenDebuffChanged()},
			new ZodiacSignDebuff { zodiacSign = ZodiacSign.DOG, applyDebuff = () => debuffSystem.OnDogDebuffChanged()},
			new ZodiacSignDebuff { zodiacSign = ZodiacSign.PIG, applyDebuff = () => debuffSystem.OnPigDebuffChanged()},			
		};
	}

	public ZodiacSign GetCurZodiacSign()
	{
		double unixTimestamp = GetUnixTimestamp();
		DateTimeOffset currentTime = DateTimeOffset.FromUnixTimeSeconds((long)unixTimestamp).ToLocalTime();
		int currentHour = currentTime.Hour;

		foreach (BerserkTime zodiacSignTime in berserkTime)
		{
			if (zodiacSignTime.startHour <= currentHour && currentHour < zodiacSignTime.endHour)
			{
				return zodiacSignTime.zodiacSign;
			}

			if (zodiacSignTime.zodiacSign == ZodiacSign.MOUSE && (currentHour >= 23 || currentHour < 1))
			{
				return ZodiacSign.MOUSE;
			}
		}

		return ZodiacSign.MOUSE;
	}

	public void ApplyDebuff(ZodiacSign zodiacSign)
	{
		foreach (ZodiacSignDebuff debuff in zodiacSignDebuff)
		{
			if (debuff.zodiacSign == zodiacSign)
			{
				debuff.applyDebuff?.Invoke();
				return;
			}
		}
	}

	private double GetUnixTimestamp()
	{
		return (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
	}
}
