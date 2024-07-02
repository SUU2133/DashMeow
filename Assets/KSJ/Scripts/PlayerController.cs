using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private Animator anim;
    //���߿� �÷��̾� ���¿� ���� ����Ǵ� ��ų���� ���� �� �ֱ� ������ public���� ����
    public bool isGrounded = false;
    public bool isSlide;
    private int jumpCount = 0;
    public int maxJumpCount = 2; // 2�� ������ ���� �ִ� ���� Ƚ���� 2�� ����

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // ����� ��ġ �Է� ó��
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Jump();
        }

        // Ű���� �Է� ó�� (������)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Slide(true);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Slide(false);
        }
    }

    void Jump()
    {
        if (jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
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

    void Slide(bool _isSlide)
    {
        isSlide = _isSlide;
        anim.SetBool("isSlide", _isSlide);
    }
}
