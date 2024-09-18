using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class Player : MonoBehaviour , IDamagable
{

	public float boosterSpeed = 1.2f;
	private Rigidbody2D rb;
	private Animator anim;
	private BoxCollider2D playerCollider;
	private Vector2 originalColliderSize;
	private Vector2 originalColliderOffset;
	private SpriteRenderer spriteRenderer;
	public InfiniteRankRegister weekRank;
	private Color originalColor;
	public Color heatColor;
	public GameObject glideCooltimeUI;
	public TMP_Text glideCooltimeText;
	public GameObject jumpButton;
	public GameObject slideButton;
	public GameObject glideButton;

	public CameraShake cameraShake;
	private Color originalGlideButtonColor;

	public EffectPoolManager effectPool;//이펙트 풀 매니져

	[Header("# Camera Shake Time")]
	public float shakeTime=0.2f;

	[Header("# Player Jump")]
	public float jumpForce = 10f;
	public int jumpCount = 0;
	public int maxJumpCount = 2; // 2단 점프를 위해 최대 점프 횟수를 2로 설정
	public float fallSpeed=5;
	public float fallStart=5;
	public float glideTime = 0;
	public float glideCooldown = 10f;
	private float glideCooldownTimer = 0f;
	[Header("# Player Stat")]
	public float health;
	public float maxHealth = 100f;
	public float speed = 10f;
	public int floorRes = 0; // 발판형 장애물 저항
	public int flyRes = 0; // 날아오는 장애물 저항
	public float allRes = 0; // 모든 피해 수치 감소 
	public float healthRegen = 0;
	
	private bool ratDesire;
	private float healthRegenTimer = 0f;
	private const float healthRegenInterval = 10f;//10초당 healthRegan 만큼 회복

	//쥐의 디버프 관련
	private float healthDamageTimer = 0f;
	private const float healthDamageInterval = 5f;

	//쥐의 염원 회복 관련

	[Header("# Player State")]
	public bool isDead = false;
	public bool isGrounded = false;
	public bool isSlide;
	public bool isJumpButtonHeld = false;
	public bool isGlide = false;
	public bool isInvincible = false;
	public bool isFirstShiled = false;
	public bool isSecondShiled = false;
	public bool isBooster = false;
	private float glideButtonHoldTimer = 0f;
	
	//활주 관련 
	public bool canGlide = true;

	[Header("# Debuff State")]
	public bool isPigDebuff = false;
	public bool isDogDebuff = false;
	public bool isRoosterDebuff = false;
	public bool isMonkeyDebuff = false;
	public bool isLambDebuff = false;
	public bool isHoresDebuff = false;
	public bool isSnakeDebuff = false;
	public bool isDragonDebuff = false;
	public bool isRabbitDebuff = false;
	public bool isTigerDebuff = false;
	public bool isOxDebuff = false;
	public bool isRatDebuff = false;

	[Header("# Effect")]
	public int jumpEffectID;
	public int glideEffectID;

	private GameObject currentGlideEffect;
	private GameObject currentBoosterEffect;
	private GameObject currentShieldEffect;
	private GameObject currentHealingEffect;
	private GameObject currentInvincibleEffect;
	private GameObject currentFireflyEffect;

	public enum EffectType
	{
		JumpEffect,
		GlideEffect,
		DestroyEffect,
		HitEffect,
		ShieldEffect,
		BoosterEffect,
		HealingEffect,
		InvincibleEffect,
		FireflyEffect
	}
	static class Constants
	{
		public const int Pig = 0;
		public const int Dog = 1;
		public const int Rooster = 2;
		public const int Monkey = 3;
		public const int Lamb = 4;
		public const int Horse = 5;
		public const int Snake = 6;
		public const int Dragon = 7;
		public const int Rabbit = 8;
		public const int Tiger = 9;
		public const int Ox = 10;
		public const int Rat = 11;
		public const int Cat = 12;
	}
	
	// Data Manager에서 받아온 염원 활성화 데이터를 저장하는 리스트
	public List<bool> activeDesires = new List<bool>();

	void Start()
	{
		Debug.Log("플레이어 스타트");
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		anim.runtimeAnimatorController = DataManager.Instance.playerSkin[DataManager.Instance.skinID];
		originalGlideButtonColor = glideButton.GetComponent<Image>().color;
		playerCollider = GetComponent<BoxCollider2D>();
		originalColliderOffset = playerCollider.offset;
		originalColliderSize = playerCollider.size;//슬라이드가 끝났을 때 사용
		spriteRenderer = GetComponent<SpriteRenderer>();
		originalColor = spriteRenderer.color;
		heatColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);//히트 시 적용 되는 색
		cameraShake = Camera.main.GetComponent<CameraShake>();
		transform.localScale = new Vector3(9f, 9f, 9f);
		effectPool = EffectPoolManager.Instance;
		// 점프 트리거
		EventTrigger jumpTrigger = jumpButton.GetComponent<EventTrigger>();
		AddEventTrigger(jumpTrigger, EventTriggerType.PointerDown, OnJumpButtonDown);
		

		// 슬라이드 트리거
		EventTrigger slideTrigger = slideButton.GetComponent<EventTrigger>();
		AddEventTrigger(slideTrigger, EventTriggerType.PointerDown, () => Slide(true));
		AddEventTrigger(slideTrigger, EventTriggerType.PointerUp, () => Slide(false));
		
		// 활주 트리거
		EventTrigger glideTrigger = glideButton.GetComponent<EventTrigger>();
		AddEventTrigger(glideTrigger, EventTriggerType.PointerDown, () => Glide(true));
		AddEventTrigger(glideTrigger, EventTriggerType.PointerUp, () => Glide(false)) ;
		
		// DataManager에서 스탯들을 가져옴
		SetStat();

		// 리스트를 12개로 지정
		activeDesires = new List<bool>(new bool[12]);

		// DataManager에 딕셔너리를 순회하면서 bool값 순서를 리스트에 저장
		int i = 0;
		foreach (KeyValuePair<Button, bool> desire in DataManager.Instance.desireStates)
		{
			activeDesires[i] = desire.Value;
			if (activeDesires[i])
				ActiveDesire(i);
			i++;
		}

		// 염원 적용 후 최대 체력에 맞게 조정
		health = maxHealth;
		// 점프 파티클 위치
		foreach (Transform child in effectPool.transform)
		{
			child.gameObject.SetActive(false);
		}
	}

	void Update()
	{
		if (glideTime == 0)
			glideButton.SetActive(false);
		else
			glideButton.SetActive(true);

		if (health <= 0)
		{
			Die();
		}

		/*
		//활공 중 이속 빨라짐
		if(isGlide)
			transform.position += new Vector3(speed*1.2f * Time.deltaTime, 0, 0);
		else
			transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
		*/
		
		if (isSlide)
		{

			rb.AddForce(Vector2.down * 20f, (ForceMode2D)ForceMode.Impulse);
		}
		//하강 가속도
		if (rb.velocity.y < fallStart)
		{
			rb.velocity += Vector2.up * Physics2D.gravity.y * fallSpeed * Time.deltaTime;
		}

		//쥐의 염원이 있을때 : 10초당 체력 1 회복
		if (ratDesire)
		{
			healthRegenTimer += Time.deltaTime;
			if (healthRegenTimer >= healthRegenInterval)
			{
				// 체력 회복
				health = Mathf.Min(health + healthRegen, maxHealth);
				healthRegenTimer = 0f;
				Debug.Log("체력 회복: " + health);
			}
		}
		//활주 중일때
		if (isGlide && canGlide && !isGrounded) {
			rb.velocity = new Vector2(rb.velocity.x, 0.5f);
			
			glideButtonHoldTimer += Time.deltaTime;

			
			if (glideButtonHoldTimer > glideTime)
			{

				Glide(false);
				canGlide = false;
				glideButtonHoldTimer = 0f;
			}
		}
		//활주 쿨타임일때
		if (!canGlide)
		{
			glideCooltimeUI.SetActive(true);

			glideCooldownTimer += Time.deltaTime;
			glideCooltimeText.text = ""+ (glideCooldown - glideCooldownTimer).ToString("F1")+" 초";

			Color glideButtonColor = Color.gray;
			glideButtonColor.a = 101/255f;
			
			glideButton.GetComponent<Image>().color = glideButtonColor;

			if (glideCooldownTimer >= glideCooldown)
			{
				glideCooltimeUI.SetActive(false);
				glideCooltimeText.text = "활공";
				canGlide = true;
				glideCooldownTimer = 0f;
				glideButton.GetComponent<Image>().color = originalGlideButtonColor;
			}
			
		}
		//활주 이펙트
		if (isGlide)
		{
			
			Vector3 offset = new Vector3(1.55f, 0, 0);
			
			if (currentGlideEffect == null || !currentGlideEffect.activeSelf)
			{
				currentGlideEffect = effectPool.Get(glideEffectID, (int)EffectType.GlideEffect);
				currentGlideEffect.transform.position = transform.position+ offset;
				currentGlideEffect.SetActive(true);
			}
			currentGlideEffect.transform.position = transform.position+ offset;
		}
		else
		{
			
			if (currentGlideEffect != null && currentGlideEffect.activeSelf)
			{
				currentGlideEffect.SetActive(false);
			}
		}

		//부스터 이펙트
		if (isBooster)
		{
			Vector3 offset = new Vector3(1.55f, 0,0);
			if (currentBoosterEffect == null || !currentBoosterEffect.activeSelf)
			{
				currentBoosterEffect = effectPool.Get(0, (int)EffectType.BoosterEffect);
				currentBoosterEffect.transform.position = transform.position + offset;
				currentBoosterEffect.SetActive(true);
			}
			currentBoosterEffect.transform.position = transform.position + offset;
		}
		else
		{

			if (currentBoosterEffect != null && currentBoosterEffect.activeSelf)
			{
				currentBoosterEffect.SetActive(false);
			}
		}

		//쉴드 이펙트
		if (isFirstShiled || isSecondShiled)
		{
			Vector3 offset = new Vector3(0, 0, 0);
			if (currentShieldEffect == null || !currentShieldEffect.activeSelf)
			{
				currentShieldEffect = effectPool.Get(0, (int)EffectType.ShieldEffect);
				currentShieldEffect.transform.position = transform.position + offset;
				currentShieldEffect.SetActive(true);
			}
			currentShieldEffect.transform.position = transform.position + offset;
		}
		else
		{

			if (currentShieldEffect != null && currentShieldEffect.activeSelf)
			{
				currentShieldEffect.SetActive(false);
			}
		}
		//무적
		if (isInvincible)
		{
			Vector3 offset = new Vector3(0, 0, 0);
			if (currentInvincibleEffect == null || !currentInvincibleEffect.activeSelf)
			{
				currentInvincibleEffect = effectPool.Get(0, (int)EffectType.InvincibleEffect);
				currentInvincibleEffect.transform.position = transform.position + offset;
				currentInvincibleEffect.SetActive(true);
			}
			currentInvincibleEffect.transform.position = transform.position + offset;
			float rotationSpeed = 50f; // 회전 속도 (초당 50도 회전)
			currentInvincibleEffect.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
		}
		else
		{

			if (currentInvincibleEffect != null && currentInvincibleEffect.activeSelf)
			{
				currentInvincibleEffect.SetActive(false);
			}
		}



		//쥐 디버프일 때 : 5초당 체력 1 감소 
		if (isRatDebuff)
		{
			healthDamageTimer += Time.deltaTime;
			if (healthDamageTimer >= healthDamageInterval)
			{
				// 체력 회복
				health -= 1f;
				healthDamageTimer = 0f;
				Debug.Log("체력 감소: " + health);
			}
		}
	}

	public void OnJumpButtonDown()
	{
		Jump();
		isJumpButtonHeld = true;
		
	}

	

	public void Jump()
	{
		if (jumpCount < maxJumpCount)
		{
			// -1은 현재 애니메이터 레이어, 0f는 애니메이션 시작 부분(0~1)
			Debug.Log("점프");
			anim.Play("Jump", -1, 0f);
			
			rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			jumpCount++;
			// TODO : 사운드 추가
			KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Jump);
			JumpEffect();
		}
	}

	public void JumpEffect()
	{
		Vector3 offset = new Vector3(-0.3f,-0.15f,0);
		Transform effect = effectPool.Get(jumpEffectID, (int)EffectType.JumpEffect).transform;
		effect.position = transform.position + offset;
		StartCoroutine(SetActiveFalseEffect(effect,0.25f));
	}

	IEnumerator SetActiveFalseEffect(Transform effect ,float destroyTime)
	{
		yield return null;

		
		float timer = 0f;
		while (timer < destroyTime)
		{
			
			timer += Time.deltaTime;
			yield return null;
		}
		effect.gameObject.SetActive(false);
	}

	public void Glide(bool _isGlide)
	{
		//땅에서 활공을 누를 경우 얼리 리턴
		if (!canGlide || isGrounded)
		{
			isGlide = false;
			return;
		}

		isGlide = _isGlide;

		//버튼업일 경우
		if (!_isGlide && !isGrounded)
		{
			canGlide = false;
			glideButtonHoldTimer = 0f;
		}

	}

	

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Ground"))
		{
			isGrounded = true;
			anim.SetBool("isGround", true);
			jumpCount = 0;
		}
		

	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Ground"))
		{
			anim.SetBool("isGround", false);
			isGrounded = false;
		}
	}
	
	public void Slide(bool _isSlide)
	{
		isSlide = _isSlide;
		anim.SetBool("isSlide", _isSlide);

		if (_isSlide)
		{
			playerCollider.size = new Vector2(playerCollider.size.x, originalColliderSize.y / 2);
			playerCollider.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - (originalColliderSize.y / 4)+0.05f);
		}
		else
		{
			playerCollider.size = originalColliderSize;
			playerCollider.offset = originalColliderOffset;
		}
	}

	private void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, UnityEngine.Events.UnityAction action)
	{
		EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
		entry.callback.AddListener((eventData) => action());
		trigger.triggers.Add(entry);
	}

	//시작시 DataManager의 스탯을 가져옴
	public void SetStat()
	{
		
		KSJSoundManager.Instance.channels = 10;
		isDead = DataManager.Instance.isDead;
		//이펙트 
		jumpEffectID = DataManager.Instance.effectID;

		health = DataManager.Instance.health;
		maxHealth = DataManager.Instance.maxHealth+DataManager.Instance.redMarbleValue[DataManager.Instance.redMarbleLv];
		if (DataManager.Instance.skinID == 5 || DataManager.Instance.skinID== 6)// 해골냥 : 체력 10 증가 || 마법사냥 : 체력 10 증가
			maxHealth += 10;
		
		jumpForce = DataManager.Instance.jumpForce;
		jumpCount = DataManager.Instance.jumpCount;
		fallSpeed = DataManager.Instance.fallSpeed;
		fallStart = DataManager.Instance.fallStart;

		maxJumpCount = DataManager.Instance.maxJumpCount; // 2단 점프를 위해 최대 점프 횟수를 2로 설정
		floorRes = DataManager.Instance.floorRes; // 발판형 장애물 저항
		flyRes = DataManager.Instance.flyRes; // 날아오는 장애물 저항
		healthRegen = DataManager.Instance.healthRegen;
		glideTime = DataManager.Instance.glideTime+DataManager.Instance.greenMarbleValue[DataManager.Instance.greenMarbleLv];
		if (DataManager.Instance.skinID == 4)//천사냥 : 활공 시간 2초  증가
			glideTime += 2f;
		if(DataManager.Instance.skinID == 6 )//마법사냥 : 활공 시간 3초 증가
			glideTime += 3f;

		if (DataManager.Instance.greenMarbleLv == 10)
			glideCooldown = DataManager.Instance.glideCooldown *(0.8f);
		else
			glideCooldown = DataManager.Instance.glideCooldown;
		allRes = DataManager.Instance.allRes+DataManager.Instance.blueMarbleValue[DataManager.Instance.blueMarbleLv];
		ratDesire = DataManager.Instance.ratDesire;
		healthRegenTimer = DataManager.Instance.healthRegenTimer;
	}

	public void ActiveDesire(int animal)
	{
		switch (animal)
		{
			case Constants.Pig:// 최대 체력 20 증가
				maxHealth += 20;
				health = maxHealth;
				break;
			case Constants.Dog:// 점프 +1회
				maxJumpCount++;
				break;
			case Constants.Rooster:// 물기둥 데미지 10 감소
				//floorRes += 5;
				break;
			case Constants.Monkey:// 젤리코인 점수 5점 증가
				
				break;
			case Constants.Lamb:// 반딧불의 체력 회복량 5 증가
				//완료
				break;
			case Constants.Horse:// 부스터 아이템 지속 시간 1.5초 증가
				//완료		
				break;
			case Constants.Snake:// 날아오는 장애물 오브젝트 피해 수치 2 감소
				flyRes += 2;
				break;
			case Constants.Dragon:// 무적 아이템 지속 시간 증가
				//완료
				break;
			case Constants.Rabbit:// 츄르->회복의 체력 회복량 5 증가
				//완료
				break;
			case Constants.Tiger://  쉴드 효과 횟수 1회 증가
				Debug.Log("호랑이 염원");
				break;
			case Constants.Ox:// 활공 1초 시간 증가
				glideTime += 1f;
				break;
			case Constants.Rat://10초당 체력 1 회복
				ratDesire = true;
				healthRegen += 1f;
				break;
			default:
				break;
		}
	}

	public void TakeDamage(int damage)
	{

		//발판형, 고정형 , 방해물 , 버프

		if (!isInvincible && !isFirstShiled && !isSecondShiled && !isBooster && !isGlide)//무적,첫번째 쉴드, 두번째 쉴드 , 활공 중
		{

			Debug.Log("hit");
			health -= (damage - allRes);
			KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Hit);
			StartCoroutine(HitEffect());
		}
		Debug.Log("Player took damage: " + (damage - allRes) + ", current health: " + health + " allRes : "+allRes);

		
	}

	private IEnumerator HitEffect()
	{
		spriteRenderer.color = heatColor; // 밝게 설정
		// TODO : 사운드
		yield return new WaitForSeconds(0.2f); // 잠시 대기
		spriteRenderer.color = originalColor; // 원래 색으로 복원
		yield return new WaitForSeconds(0.1f);
		
		spriteRenderer.color = heatColor; // 밝게 설정
		yield return new WaitForSeconds(0.2f); // 잠시 대기
		spriteRenderer.color = originalColor; // 원래 색으로 복원
	}

	public int FloorObstacleDamage(int damage)
	{
		return damage -= floorRes;

	}

	public int FlyObstacleDamage(int damage)
	{
		return damage -= flyRes;
	}

	public int WaterPillarDamage(int damage)
	{
		if (activeDesires[2])//닭 염원
			return damage -= 5;
		return damage;
	}
	public void Die()
	{
		if (isDead) return;
		
		isDead = true;
		anim.SetTrigger("isDead");
		Debug.Log("플레이어가 죽었습니다.");
		Time.timeScale = 0f;
		GameManager.UI.ShowPopUpUI<PopUpUI>("UI/DeathPopUpUI");
		
		if (GameManager.GameModeSystem.curGameMode == GameModeSystem.GameMode.INFINITE)
		{
			Debug.Log("무한 모드 점수 : " + GameManager.Score.GetTotalScore());
			weekRank.Process(GameManager.Score.GetTotalScore());
		}
		GameManager.Instance.EndGame();
	}

	// 츄르 : 체력 10회복
	public void HealByFirfly(int value)
	{
		if (isLambDebuff)
			value -= 5;
		if (activeDesires[Constants.Lamb])
			value += 5;

		


		health += value;
		if (health > maxHealth)
			health = maxHealth;
		Vector3 offset = new Vector3(-0.3f, -0.15f, 0);
		Transform effect = effectPool.Get(0, (int)EffectType.FireflyEffect).transform;
		effect.position = transform.position + offset;
		StartCoroutine(SetActiveFalseEffect(effect, 0.5f));
	}
	public void ReduceSpeed(float duration)
	{
		if (activeDesires[Constants.Rooster])
			return;
		StartCoroutine(ReduceSpeedRoutine(duration));
	}
	private IEnumerator ReduceSpeedRoutine(float duration)
	{
		
		speed -= 2;
		yield return new WaitForSeconds(duration);
		speed += 2;
		

		// TODO : 낙사 제외
	}
	public void HealByChur(int value)
	{
		if (isRabbitDebuff)
			value -= 5;

		if (activeDesires[Constants.Rabbit])
			value += 5;


		health += value;
		if (health > maxHealth)
			health = maxHealth;

		Vector3 offset = new Vector3(-0.3f, -0.15f, 0);
		Transform effect = effectPool.Get(0, (int)EffectType.HealingEffect).transform;
		effect.position = transform.position + offset;
		StartCoroutine(SetActiveFalseEffect(effect, 0.5f));
	}

	// 무적 : 3초간 모든 피해 무적(낙사 제외)
	public void BecomeInvincible(float duration)
	{
		if (isDragonDebuff)
			duration -= 1.5f;
		
		if (activeDesires[Constants.Dragon])
			duration += 1.5f;
		StartCoroutine(BecomeInvincibleRoutine(duration));
	}

	private IEnumerator BecomeInvincibleRoutine(float duration)
	{
		isInvincible = true;
		yield return new WaitForSeconds(duration);
		Debug.Log("무적");
		isInvincible = false;

		// TODO : 낙사 제외
	}

	// 부스터 : 3초간 모든 장애물을 파괴하면서 질주(이동 속도 수치가 20증가)
	public void Booster(float duration)
	{
		if (isHoresDebuff)
			duration -= 1.5f;
		if (activeDesires[Constants.Horse])
			duration += 1.5f;
		StartCoroutine(BoostRoutine(duration));
	}

	private IEnumerator BoostRoutine(float duration)
	{
		isBooster = true;
		speed += 20;
		yield return new WaitForSeconds(duration);
		speed -= 20;
		isBooster = false;
	}

	// 쉴드 : 장애물 1회 방어(낙사 제외)
	public void BlockObstacle()
	{
		if (isTigerDebuff)
			return;

		isFirstShiled = true;
		if(activeDesires[Constants.Tiger])
			isSecondShiled = true;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.gameObject.CompareTag("Obstacle"))
		{
			//보호막
			if(!isFirstShiled && isSecondShiled)
			{
				isSecondShiled = false;
			}
			if (isFirstShiled)
			{
				isFirstShiled = false;

			}
			//부스터나 활공 시 장애물 파괴
			if (isBooster)
			{


				Transform effect = effectPool.Get(0, (int)EffectType.DestroyEffect).transform;
				Vector3 effectPosition = other.transform.position;


				float randomZRotation = Random.Range(0f, 360f);
				effect.rotation = Quaternion.Euler(0f, 0f, randomZRotation);

				effect.position = effectPosition;
				KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Destroy);
				StartCoroutine(SetActiveFalseEffect(effect, 0.5f));
				Destroy(other.gameObject);
				//CameraShake.Instance.ShakeCamera(10f, 0.1f);


			}
			if(isGlide && canGlide)
			{
				Transform effect = effectPool.Get(0, (int)EffectType.DestroyEffect).transform;
				Vector3 effectPosition = other.transform.position;

				
				float randomZRotation = Random.Range(0f, 360f);
				effect.rotation = Quaternion.Euler(0f, 0f, randomZRotation);

				effect.position = effectPosition;
				KSJSoundManager.Instance.PlaySfx(KSJSoundManager.Sfx.Destroy);
				StartCoroutine(SetActiveFalseEffect(effect, 0.5f));
				Destroy(other.gameObject);
				//CameraShake.Instance.ShakeCamera(10f, 0.1f);
			}
		}

		if (other.gameObject.CompareTag("FallZone"))
		{
			// 낙사
			isDead = true;
		}
	}

	
	//디버프 메서드
	public void PigDebuff()// 최대체력 20감소
	{
		isPigDebuff = true;
		maxHealth -= 20;
	}

	public void DogDebuff()// 이동속도 20 감소 우선 20프로 감소로 변경
	{
		isDogDebuff = true;
		speed *= 0.80f;
	}

	public void RoosterDebuff()// 고정형 장애물 오브젝트 피해수치 5 증가
	{
		isRoosterDebuff = true;
		floorRes -= 5;
	}

	public void MonkeyDebuff() // 젤리발바닥 점수 5 감소 스코어에서 관리
	{
		isMonkeyDebuff = true;
	}

	public void LamnDebuff() // 반딧불 회복량 5 감소
	{
		isLambDebuff = true;
	}

	public void HorseDebuff() // 부스터 아이템 지속시간 1.5초 감소
	{
		isHoresDebuff = true;
	}

	public void SnakeDebuff() // 날아오는 장애물 오브젝트 피해수치 5 증가
	{
		isSnakeDebuff = true;
		flyRes -=5;
	}

	public void DragonDebuff() // 무적 아이템 지속시간 1.5초 감소
	{
		isDragonDebuff = true;
	}

	public void RabbitDebuff() // 츄르의 회복량 5 감소
	{
		isRabbitDebuff = true;
	}

	public void TigerDebuff() // 쉴드 효과 무효화
	{
		isTigerDebuff = true;
	}

	public void OxDebuff() // 활공 1초 감소
	{
		isOxDebuff = true;
		glideTime -= 1f;
	}

	public void RatDebuff() // 5초당 체력 1 감소
	{
		isRatDebuff = true;

	}
}
