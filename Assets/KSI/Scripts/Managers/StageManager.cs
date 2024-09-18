using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageManager : MonoBehaviour
{
	public static UnityEvent OnPigSubComplete = new UnityEvent();
	public static UnityEvent OnPigBossComplete = new UnityEvent();
	public static UnityEvent OnDogSubComplete = new UnityEvent();
	public static UnityEvent OnDogBossComplete = new UnityEvent();
	public static UnityEvent OnChickenSubComplete = new UnityEvent();
	public static UnityEvent OnChickenBossComplete = new UnityEvent();
	public static UnityEvent OnMonkeySubComplete = new UnityEvent();
	public static UnityEvent OnMonkeyBossComplete = new UnityEvent();
	public static UnityEvent OnSheepSubComplete = new UnityEvent();
	public static UnityEvent OnSheepBossComplete = new UnityEvent();
	public static UnityEvent OnHorseSubComplete = new UnityEvent();
	public static UnityEvent OnHorseBossComplete = new UnityEvent();
	public static UnityEvent OnSnakeSubComplete = new UnityEvent();
	public static UnityEvent OnSnakeBossComplete = new UnityEvent();
	public static UnityEvent OnDragonSubComplete = new UnityEvent();
	public static UnityEvent OnDragonBossComplete = new UnityEvent();
	public static UnityEvent OnRabbitSubComplete = new UnityEvent();
	public static UnityEvent OnRabbitBossComplete = new UnityEvent();
	public static UnityEvent OnTigerSubComplete = new UnityEvent();
	public static UnityEvent OnTigerBossComplete = new UnityEvent();
	public static UnityEvent OnCowSubComplete = new UnityEvent();
	public static UnityEvent OnCowBossComplete = new UnityEvent();
	public static UnityEvent OnMouseSubComplete = new UnityEvent();
	public static UnityEvent OnMouseBossComplete = new UnityEvent();
}
