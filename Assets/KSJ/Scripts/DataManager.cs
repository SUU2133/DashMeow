using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static DataManager Instance { get; private set; }

    // �� ���� ������ ������
    public Dictionary<Button, bool> desireStates = new Dictionary<Button, bool>();

    private void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� ��ü ����
        }
        else
        {
            Destroy(gameObject); // �ߺ��� �ν��Ͻ� ����
        }
    }

    // ������ �ʱ�ȭ �޼���
    public void InitializeData(Dictionary<Button, bool> data)
    {
        desireStates = new Dictionary<Button, bool>(data);
    }
}