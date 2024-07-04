using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private Animator anim;
    //���߿� �÷��̾� ���¿� ���� ����Ǵ� ��ų���� ���� �� �ֱ� ������ public���� ����
    public GameObject jumpButton;
    public GameObject slideButton;

    [Header("# Player Stat")]
    public int health;
    public int maxHealth=100;
    public float speed = 10f;
    public float jumpForce = 10f;
    public int jumpCount = 0;
    public int maxJumpCount = 2; // 2�� ������ ���� �ִ� ���� Ƚ���� 2�� ����

    [Header("# Player State")]
    public bool isGrounded = false;
    public bool isSlide;
    
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        EventTrigger jumpTrigger = jumpButton.GetComponent<EventTrigger>();
        AddEventTrigger(jumpTrigger, EventTriggerType.PointerDown, Jump);

        EventTrigger slideTrigger = slideButton.GetComponent<EventTrigger>();
        AddEventTrigger(slideTrigger, EventTriggerType.PointerDown, () => Slide(true));
        AddEventTrigger(slideTrigger, EventTriggerType.PointerUp, () => Slide(false));
    }

    void Update()
    {
        if (isSlide)
        {
            rb.AddForce(Vector2.down, (ForceMode2D)ForceMode.Acceleration);
        }
        
        
        
        
    }

    public void Jump()
    {
        
        
        if (jumpCount < maxJumpCount)
        {
            // -1�� ���� �ִϸ����� ���̾� , 0f�� �ִϸ��̼� ���� �κ�(0~1)
            Debug.Log("����");
            anim.Play("Jump", -1, 0f);
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

    public void Slide(bool _isSlide)
    {
        isSlide = _isSlide;
        anim.SetBool("isSlide", _isSlide);
        
    }

    private void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, UnityEngine.Events.UnityAction action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((eventData) => action());
        trigger.triggers.Add(entry);
    }
}
