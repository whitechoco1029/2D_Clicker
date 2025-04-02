using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEvent : MonoBehaviour
{
    [SerializeField] GameObject eventPanel; // 팝업 UI 패널
    [SerializeField] Button eventButton; // 클릭할 버튼
    [SerializeField] int maxClicks = 10; // 10번 클릭
    [SerializeField] float eventInterval = 60f; // 1분

    public int clickCount = 0;
    public float startTime = 0f;
    public bool isEventActive = false;

    public void Start()
    {
        eventPanel.SetActive(false); // 시작할 때 비활성화
        InvokeRepeating(nameof(TriggerEvent), eventInterval, eventInterval); //60초마다
        eventButton.onClick.AddListener(ClickEnemy);
    }

    public void TriggerEvent()
    {
        clickCount = 0;
        startTime = Time.time; // 이벤트 시작 시간 기록
        eventPanel.SetActive(true); // 팝업창 띄우기
        isEventActive = true; // 이벤트 활성화
    }

    public void ClickEnemy()
    {
        if (!isEventActive) return;

        clickCount++;
        float elapsedTime = Time.time - startTime; // 경과 시간 계산

        int rewardGold = 7; // 기본 보상 (7골드)

        // 클릭 횟수에 따른 보상 설정
        if (elapsedTime <= 5f)
        {
            rewardGold = 10; // 5초 이내 클릭 10번 시 10골드
        }
        else if (elapsedTime <= 6f)
        {
            rewardGold = 9; // 6초 이내 클릭 10번 시 9골드
        }
        else if (elapsedTime <= 7f)
        {
            rewardGold = 8; // 7초 이내 클릭 10번 시 8골드
        }

        if (clickCount >= maxClicks)
        {
            UserData userData = SaveSystem.Load(); // 데이터를 불러오기
            userData.gold += rewardGold; // 골드 추가
            SaveSystem.Save(userData); // 변경된 데이터 저장

            eventPanel.SetActive(false);
            isEventActive = false;
        }
    }
}