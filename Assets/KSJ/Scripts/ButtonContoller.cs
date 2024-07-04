using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonController : MonoBehaviour
{

    private bool activeInventory = false;
    public List<Button> desires; 

    public Dictionary<Button, bool> desireStates;
    
    private int maxOnButtons = 2; 
    private int currentOnCount = 0; 

    void Start()
    {
        desireStates = new Dictionary<Button, bool>();

        // �� ��ư�� ���� �ʱ�ȭ �� Ŭ�� �̺�Ʈ ����
        foreach (Button button in desires)
        {
            desireStates[button] = false; // �ʱ� ���´� Off
            button.GetComponentInChildren<Text>().text = "Off";
            button.onClick.AddListener(() => ToggleButton(button));
        }

       
    }

    void ToggleButton(Button button)
    {
        // ��ư�� Off ������ ��
        if (!desireStates[button])
        {
            if (currentOnCount < maxOnButtons)
            {
                desireStates[button] = true; // ��ư�� On���� ����
                button.GetComponentInChildren<Text>().text = "On";
                currentOnCount++;
            }
        }
        // ��ư�� On ������ ��
        else
        {
            desireStates[button] = false; // ��ư�� Off�� ����
            button.GetComponentInChildren<Text>().text = "Off"; // ��ư �ؽ�Ʈ ����
            currentOnCount--;
        }
        DataManager.Instance.InitializeData(desireStates);
    }

    public void OnClick()
    {
        activeInventory = !activeInventory;
        if (activeInventory)
        {
            transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }
        else
            transform.localScale = new Vector3(0f,0f,0f);

    }
}