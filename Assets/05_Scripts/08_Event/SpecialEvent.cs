using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEvent : MonoBehaviour
{
    [SerializeField] GameObject eventPanel; // �˾� UI �г�
    [SerializeField] Button eventButton; // Ŭ���� ��ư
    [SerializeField] int maxClicks = 10; // 10�� Ŭ��
    [SerializeField] float eventInterval = 60f; // 1��

    public int clickCount = 0;
    public float startTime = 0f;
    public bool isEventActive = false;

    public void Start()
    {
        eventPanel.SetActive(false); // ������ �� ��Ȱ��ȭ
        InvokeRepeating(nameof(TriggerEvent), eventInterval, eventInterval); //60�ʸ���
        eventButton.onClick.AddListener(ClickEnemy);
    }

    public void TriggerEvent()
    {
        clickCount = 0;
        startTime = Time.time; // �̺�Ʈ ���� �ð� ���
        eventPanel.SetActive(true); // �˾�â ����
        isEventActive = true; // �̺�Ʈ Ȱ��ȭ
    }

    public void ClickEnemy()
    {
        if (!isEventActive) return;

        clickCount++;
        float elapsedTime = Time.time - startTime; // ��� �ð� ���

        int rewardGold = 7; // �⺻ ���� (7���)

        // Ŭ�� Ƚ���� ���� ���� ����
        if (elapsedTime <= 5f)
        {
            rewardGold = 10; // 5�� �̳� Ŭ�� 10�� �� 10���
        }
        else if (elapsedTime <= 6f)
        {
            rewardGold = 9; // 6�� �̳� Ŭ�� 10�� �� 9���
        }
        else if (elapsedTime <= 7f)
        {
            rewardGold = 8; // 7�� �̳� Ŭ�� 10�� �� 8���
        }

        if (clickCount >= maxClicks)
        {
            UserData userData = SaveSystem.Load(); // �����͸� �ҷ�����
            userData.gold += rewardGold; // ��� �߰�
            SaveSystem.Save(userData); // ����� ������ ����

            eventPanel.SetActive(false);
            isEventActive = false;
        }
    }
}