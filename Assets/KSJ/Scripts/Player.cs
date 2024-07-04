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

    static class Constants
    {
        public const int Pig = 0;
        public const int Dog = 1;
        public const int Rooster = 2;
        public const int Monket = 3;
        public const int Lamb = 4;
        public const int Horse = 5;
        public const int Snake = 6;
        public const int Dragon = 7;
        public const int Rabbit = 8;
        public const int Tiger = 9;
        public const int Ox = 10;
        public const int Rat = 11;
    }

    //Data Manager���� �޾ƿ� ���� Ȱ��ȭ �����͸� �����ϴ� ����Ʈ
    public List<bool> activeDesires= new List<bool>();

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //���� Ʈ����
        EventTrigger jumpTrigger = jumpButton.GetComponent<EventTrigger>();
        AddEventTrigger(jumpTrigger, EventTriggerType.PointerDown, Jump);
        //�����̵� Ʈ����
        EventTrigger slideTrigger = slideButton.GetComponent<EventTrigger>();
        AddEventTrigger(slideTrigger, EventTriggerType.PointerDown, () => Slide(true));
        AddEventTrigger(slideTrigger, EventTriggerType.PointerUp, () => Slide(false));

        //����Ʈ�� 12���� ����
        activeDesires = new List<bool>(new bool[12]);
        
        //DataManager�� ��ųʸ��� ��ȸ�ϸ鼭 bool�� ������ ����Ʈ�� ����
        int i = 0;
        foreach (KeyValuePair<Button,bool> desire in DataManager.Instance.desireStates)
        {
            activeDesires[i] = desire.Value;
            if(activeDesires[i])
                ActiveDesire(i);
            i++;

        }
        //���� ���� �� �ִ� ü�¿� �°� ����
        health = maxHealth;
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

   public void ActiveDesire(int animal)
    {
        switch (animal)
        {
            case Constants.Pig:
                maxHealth += 5;
                break;
            case Constants.Dog:
                speed += 5f;
                break;
            default:
                break;

        }
    }
}
