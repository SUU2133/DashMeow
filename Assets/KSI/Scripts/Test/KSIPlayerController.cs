using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KSIPlayerController : MonoBehaviour, IDamagable
{
	public float jumpForce = 2f;
	private Rigidbody2D rb;
	private Animator anim;
	public bool isGrounded = false;
	public int health = 100;
	private bool isInvincible = false;
	private bool isShiled = false;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		Jump();
	}

	public void Jump()
	{

		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Ground"))
		{
			isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Ground"))
		{
			isGrounded = false;
		}
	}

	/* 강수인 추가 */
	public void TakeDamage(int damage)
	{
		health -= damage;
		//Debug.Log("플레이어가 데미지를 입었습니다. 현재 체력: " + health);
		if (health <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		// TODO : 플레이어 사망 추가
		Debug.Log("플레이어가 죽었습니다.");
	}

	// 츄르 : 체력 10회복
	public void Heal(int value)
	{
		health += value;
	}

	// 무적 : 3초간 모든 피해 무적(낙사 제외)
	public void BecomeInvincible(float duration)
	{
		StartCoroutine(BecomeInvincibleRoutine(duration));
	}

	private IEnumerator BecomeInvincibleRoutine(float duration)
	{
		isInvincible = true;
		yield return new WaitForSeconds(duration);
		isInvincible = false;

		// TODO : 낙사 제외
	}

	// 부스터 : 3초간 모든 장애물을 파괴하면서 질주(이동 속도 수치가 20증가)
	public void Booster(float duration)
	{
		StartCoroutine(BoostRoutine(duration));
	}

	private IEnumerator BoostRoutine(float duration)
	{
		// TODO : 이동 속도 수치가 20증가
		yield return new WaitForSeconds(duration);
	}

	// 쉴드 : 장애물 1회 방어(낙사 제외)
	public void BlockObstacle()
	{
		isShiled = true;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Obstacle"))
		{
			if (isShiled)
			{
				isShiled = false;
				// TODO :  장애물 방어
			}
			else if (!isInvincible)
			{
				// TODO : 플레이어가 무적인지 확인
			}
			else if (other.gameObject.CompareTag("FallZone"))
			{
				// TODO : 낙사 확인
				Die();
			}
		}
	}
}
