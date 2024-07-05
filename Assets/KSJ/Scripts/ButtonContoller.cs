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

        // 각 버튼의 상태 초기화 및 클릭 이벤트 설정
        foreach (Button button in desires)
        {
            desireStates[button] = false; // 초기 상태는 Off
            
            button.onClick.AddListener(() => ToggleButton(button));
            button.GetComponent<Image>().color = Color.gray;
        }

       
    }

    void ToggleButton(Button button)
    {
        // 버튼이 Off 상태일 때
        if (!desireStates[button])
        {
            if (currentOnCount < maxOnButtons)
            {
                desireStates[button] = true; // 버튼을 On으로 설정
                button.GetComponent<Image>().color = Color.white;
                currentOnCount++;
            }
        }
        // 버튼이 On 상태일 때
        else
        {
            desireStates[button] = false; // 버튼을 Off로 설정
            button.GetComponent<Image>().color = Color.gray;
            currentOnCount--;
        }
        DataManager.Instance.InitializeData(desireStates);
    }

    public void OnClick()
    {
        activeInventory = !activeInventory;
        if (activeInventory)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
            transform.localScale = new Vector3(0f,0f,0f);

    }
}