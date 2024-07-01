using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    private Rigidbody rb;
    private bool isGrounded = false;
    private int jumpCount = 0;
    public int maxJumpCount = 2; // 2�� ������ ���� �ִ� ���� Ƚ���� 2�� ����

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
    }

    void Jump()
    {
        if (jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            jumpCount++;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
